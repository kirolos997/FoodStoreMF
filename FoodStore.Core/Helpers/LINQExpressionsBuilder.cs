using FoodStore.Core.DTO.QueryFilters;
using FoodStore.Core.Enums;
using FoodStore.Core.Exceptions.Generic;
using System.Linq.Expressions;

namespace FoodStore.Core.Helpers
{
    /// <summary>
    /// LINQ dynamic expression builder class to build expression tree for filtering the response and generating lambda function
    /// </summary>
    public class LINQExpressionsBuilder
    {
        public static Expression<Func<T, bool>>? GetAndFilterExpression<T>(List<FilterTerm> searchTerm)
        {
            if (searchTerm.Count == 0)
            {
                return null;
            }
            Expression? lambdaBody = null;

            Expression? comparisonExpression = null;

            Expression? property = null;

            Expression? constant = null;

            //  Expression.Parameter(): Creating a 'parameter expression' for the delegate having type to be of Product and name item
            var lambdaParam = Expression.Parameter(typeof(T), "item");

            foreach (FilterTerm term in searchTerm)
            {
                // Expression.Property(): Accessing a value of a specific property from an object at runtime
                // Expression.Constant(): Creating a constant node inside expression tree

                property = Expression.Property(lambdaParam, term.Name);

                if (bool.TryParse(term.Value, out bool boolValue))
                {
                    constant = Expression.Constant(boolValue);
                }
                else if (decimal.TryParse(term.Value, out decimal decimalValue))
                {
                    constant = Expression.Constant(decimalValue);
                }
                else
                {
                    // Normal string
                    constant = Expression.Constant(term.Value);
                }

                if (property.Type != constant.Type)
                {
                    throw new InvalidOperationException($"Invalid data type passed for {term.Name}. Expected type is {property.Type}");
                }
                switch (term.Operator.ToLower())
                {
                    case nameof(Operators.eq):
                        comparisonExpression = Expression.Equal(property, constant);
                        break;
                    case nameof(Operators.neq):
                        comparisonExpression = Expression.NotEqual(property, constant);
                        break;
                    case nameof(Operators.lt):
                        comparisonExpression = Expression.LessThan(property, constant);
                        break;
                    case nameof(Operators.gt):
                        comparisonExpression = Expression.GreaterThan(property, constant);
                        break;
                }

                if (comparisonExpression is null)
                {
                    throw new InvalidOperatorException($"Operator{term.Operator} is not valid. Valid operators are 'eq', 'neq', 'lt', 'gt'");
                }
                else if (lambdaBody == null)
                {
                    lambdaBody = comparisonExpression;
                }
                else
                {
                    lambdaBody = Expression.AndAlso(lambdaBody, comparisonExpression);
                }

                comparisonExpression = null;

            }

            return Expression.Lambda<Func<T, bool>>(lambdaBody, lambdaParam);

        }
    }
}
