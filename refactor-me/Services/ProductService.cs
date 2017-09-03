using System;
using refactor_me.Models;
using refactor_me.Repositories.Interfaces;
using refactor_me.Services.Interfaces;

namespace refactor_me.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Guid CreateProduct(Product product)
        {
            product.Id = Guid.NewGuid();
            return _productRepository.CreateProduct(product);
        }

        public void DeleteProduct(Guid id)
        {
            // TODO: validate id but should stay idempotent, should delete product options, should implement UOW
            _productRepository.DeleteProduct(id);
        }

        public Products GetAllProducts()
        {
            // TODO: add pagination
            return _productRepository.GetAllProducts();
        }

        public Product GetProductById(Guid id)
        {
            // TODO: validate id
            return _productRepository.GetProductById(id);
        }

        public Products SearchByProductName(string productName)
        {
            // TODO: Chaange to a more generic search
            return _productRepository.SearchByProductName(productName);
        }

        public void UpdateProduct(Product product)
        {
            // TODO: validate id, Id should stay the same
            _productRepository.UpdateProduct(product);
        }
    }
}