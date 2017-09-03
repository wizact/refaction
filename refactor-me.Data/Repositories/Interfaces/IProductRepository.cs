using System;
using refactor_me.Data.Entities;
using System.Collections.Generic;

namespace refactor_me.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Guid CreateProduct(Product product);
        void DeleteProduct(Guid id);
        List<Product> GetAllProducts();
        Product GetProductById(Guid id);
        List<Product> SearchByProductName(string productName);
        void UpdateProduct(Product product);
    }
}