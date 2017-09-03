using System;
using refactor_me.Models;
using refactor_me.Data.Repositories.Interfaces;
using refactor_me.Business.Services.Interfaces;
using System.Collections.Generic;
using refactor_me.Business.Mappers.Interfaces;
using System.Linq;

namespace refactor_me.Business.Services
{
    public class ProductService: BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, IEnumerable<IMapper> mappers)
        {
            _productRepository = productRepository;
            _mappers = mappers.ToList();
        }

        public Guid CreateProduct(Product product)
        {
            product.Id = Guid.NewGuid();

            var productEntity = GetMapper<Product, Data.Entities.Product>().ToEntity(product);

            return _productRepository.CreateProduct(productEntity);
        }

        public void DeleteProduct(Guid id)
        {
            // TODO: validate id but should stay idempotent, should delete product options, should implement UOW
            _productRepository.DeleteProduct(id);
        }

        public Products GetAllProducts()
        {
            // TODO: add pagination
            var entityProducts = _productRepository.GetAllProducts();
            var modelProducts = new Products();
            var productMapper = GetMapper<Product, Data.Entities.Product>();

            entityProducts.ForEach(entityProduct => modelProducts.Items.Add(productMapper.ToModel(entityProduct)));

            return modelProducts;
        }

        public Product GetProductById(Guid id)
        {
            // TODO: validate id
            var productMapper = GetMapper<Product, Data.Entities.Product>();
            return productMapper.ToModel(_productRepository.GetProductById(id));
        }

        public Products SearchByProductName(string productName)
        {
            // TODO: Chaange to a more generic search
            var modelProducts = new Products();
            var productMapper = GetMapper<Product, Data.Entities.Product>();
            var entityProducts = _productRepository.SearchByProductName(productName);

            entityProducts.ForEach(entityProduct => modelProducts.Items.Add(productMapper.ToModel(entityProduct)));

            return modelProducts;
        }

        public void UpdateProduct(Product product)
        {
            // TODO: validate id, Id should stay the same
            var productMapper = GetMapper<Product, Data.Entities.Product>();
            _productRepository.UpdateProduct(productMapper.ToEntity(product));
        }
    }
}