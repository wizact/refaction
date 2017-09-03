using System;
using refactor_me.Data.Entities;

namespace refactor_me.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Guid CreateProduct(Product product);
        void DeleteProduct(Guid id);
        Products GetAllProducts();
        Product GetProductById(Guid id);
        Products SearchByProductName(string productName);
        void UpdateProduct(Product product);
    }
}