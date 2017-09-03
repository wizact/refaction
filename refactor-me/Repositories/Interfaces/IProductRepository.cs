using System;
using refactor_me.Models;

namespace refactor_me.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Guid CreateProduct(Product product);
        void DeleteProduct(Guid id);
        Products GetAllProducts();
        Product GetProductById(Guid id);
        Products SearchByProductName();
        void UpdateProduct(Product product);
    }
}