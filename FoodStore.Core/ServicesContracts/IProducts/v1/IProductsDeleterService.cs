namespace FoodStore.Core.ServicesContracts.IProducts.v1
{
    /// <summary>
    /// Represents business logic (delete) for manipulating product entity
    /// </summary>
    public interface IProductsDeleterService
    {    /// <summary>
         /// Deletes a product based on the given product id
         /// </summary>
         /// <param name="productID">Product ID to be deleted</param>
         /// <returns>Returns true, if the deletion is successful; otherwise false</returns>
        public Task<bool> DeleteProduct(Guid? productID);
    }
}

