using refactor_me.Models;
using System;

namespace refactor_me.Services.Interfaces
{
    public interface IProductService
    {
        Guid CreateProduct(Product product);

        void DeleteProduct(Guid id);

        Products GetAllProducts();

        Product GetProductById(Guid id);

        Products SearchByProductName(string productName);

        void UpdateProduct(Product product);
    }
}
