using refactor_me.Services.Interfaces;
using System;
using refactor_me.Models;
using refactor_me.Repositories.Interfaces;

namespace refactor_me.Services
{
    public class ProductOptionService : IProductOptionService
    {
        private readonly IProductOptionRepository _productOptionRepository;

        public ProductOptionService(IProductOptionRepository productOptionRepository)
        {
            _productOptionRepository = productOptionRepository;
        }

        public void CreateProductOptionForProduct(ProductOption productOption)
        {
            productOption.Id = Guid.NewGuid();
            _productOptionRepository.CreateProductOptionForProduct(productOption);
        }

        public void DeleteProductOption(Guid id)
        {
            _productOptionRepository.DeleteProductOption(id);
        }

        public ProductOption GetProductOption(Guid productOptionId)
        {
            return _productOptionRepository.GetProductOption(productOptionId);
        }

        public ProductOptions GetProductOptionsForProduct(Guid productId)
        {
            return _productOptionRepository.GetProductOptionsForProduct(productId);
        }

        public void UpdateProductOption(ProductOption productOption)
        {
            _productOptionRepository.UpdateProductOption(productOption);
        }
    }
}