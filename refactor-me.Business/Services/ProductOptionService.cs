using System;
using refactor_me.Models;
using refactor_me.Data.Repositories.Interfaces;
using refactor_me.Business.Services.Interfaces;
using refactor_me.Business.Mappers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace refactor_me.Business.Services
{
    public class ProductOptionService : BaseService, IProductOptionService
    {
        private readonly IProductOptionRepository _productOptionRepository;

        public ProductOptionService(IProductOptionRepository productOptionRepository, IEnumerable<IMapper> mappers)
        {
            _productOptionRepository = productOptionRepository;
            _mappers = mappers.ToList();
        }

        public void CreateProductOptionForProduct(ProductOption productOption)
        {
            productOption.Id = Guid.NewGuid();

            var productOptionEntity = GetMapper<ProductOption, Data.Entities.ProductOption>().ToEntity(productOption);

            _productOptionRepository.CreateProductOptionForProduct(productOptionEntity);
        }

        public void DeleteProductOption(Guid id)
        {
            _productOptionRepository.DeleteProductOption(id);
        }

        public ProductOption GetProductOption(Guid productOptionId)
        {
            var producEntity = _productOptionRepository.GetProductOption(productOptionId);
            var mapper = GetMapper<ProductOption, Data.Entities.ProductOption>();
            return mapper.ToModel(producEntity);
        }

        public ProductOptions GetProductOptionsForProduct(Guid productId)
        {
            var productOptionsEntity = _productOptionRepository.GetProductOptionsForProduct(productId);
            var mapper = GetMapper<ProductOption, Data.Entities.ProductOption>();

            var productOptionsModel = new ProductOptions();
            productOptionsEntity.Items.ForEach(poe => { productOptionsModel.Items.Add(mapper.ToModel(poe)); });

            return productOptionsModel;
        }

        public void UpdateProductOption(ProductOption productOption)
        {
            var mapper = GetMapper<ProductOption, Data.Entities.ProductOption>();
            _productOptionRepository.UpdateProductOption(mapper.ToEntity(productOption));
        }
    }
}