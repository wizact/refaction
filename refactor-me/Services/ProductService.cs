using System;
using refactor_me.Models;
using refactor_me.Repositories.Interfaces;

namespace refactor_me.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        Guid CreateProduct(Product product)
        {
            this._productRepository.CreateProduct(product);
        }

        void DeleteProduct(Guid id)
        {

        }

        Products GetAllProducts()
        {
            
        }

        Product GetProductById(Guid id)
        {
            
        }

        Products SearchByProductName()
        {
            
        }

        void UpdateProduct(Product product)
        {
            
        }
    }
}