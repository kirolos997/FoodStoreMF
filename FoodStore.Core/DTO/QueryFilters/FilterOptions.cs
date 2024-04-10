using FoodStore.Core.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FoodStore.Core.DTO.QueryFilters
{

    public class FilterOptions<T> : IValidatableObject
    {
        public string[]? Search { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Search != null && Search.Length != 0)
            {
                var validTerms = GetValidTerms().Select(item => item.Name);

                var inValidTerms = GetPassedQueryTerms().Select(item => item.Name).Except(validTerms, StringComparer.OrdinalIgnoreCase);

                foreach (var item in inValidTerms)
                {
                    yield return new ValidationResult($"Invalid search term'{item}'", [nameof(Search)]);
                }
            }

        }
        public IEnumerable<FilterTerm> GetPassedQueryTerms()
        {
            if (Search == null) yield break;

            foreach (var expression in Search)
            {
                if (string.IsNullOrEmpty(expression)) continue;

                var tokens = expression.Split(' ');

                if (tokens.Length == 0)
                {
                    yield return new FilterTerm
                    {
                        ValidSyntax = false,
                        Name = expression
                    };

                    continue;
                }

                if (tokens.Length < 3)
                {
                    yield return new FilterTerm
                    {
                        ValidSyntax = false,
                        Name = tokens[0]
                    };

                    continue;
                }

                yield return new FilterTerm
                {
                    ValidSyntax = true,
                    Name = tokens[0],
                    Operator = tokens[1],
                    Value = string.Join(" ", tokens.Skip(2))
                };
            }
        }

        public IEnumerable<FilterTerm> GetValidTerms()
        {
            var queryTerms = GetPassedQueryTerms().Where(x => x.ValidSyntax).ToArray();

            if (!queryTerms.Any()) yield break;

            var declaredTerms = typeof(T)
                .GetTypeInfo()
                .DeclaredProperties
                .Where(p => p.GetCustomAttributes<SearchableAttribute>().Any())
                .Select(p => new FilterTerm { Name = p.Name });

            foreach (var term in queryTerms)
            {
                var declaredTerm = declaredTerms
                    .SingleOrDefault(x => x.Name.Equals(term.Name, StringComparison.OrdinalIgnoreCase));
                if (declaredTerm == null) continue;

                yield return new FilterTerm
                {
                    ValidSyntax = term.ValidSyntax,
                    Name = declaredTerm.Name,
                    Operator = term.Operator,
                    Value = term.Value
                };
            }
        }

    }



}
