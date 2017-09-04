using System;
using refactor_me.Business.Mappers.Interfaces;

namespace refactor_me.Business.Mappers
{
    public class ProductMapper : IMapper<Models.Product, Data.Entities.Product>
    {
        public Models.Product ToModel(Data.Entities.Product productEntity)
        {
            if(productEntity == null)
            {
                return default(Models.Product);
            }

            return new Models.Product
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                DeliveryPrice = productEntity.DeliveryPrice,
                Description = productEntity.Description,
                Price = productEntity.Price
            };
        }

        public Data.Entities.Product ToEntity(Models.Product productModel)
        {
            return new Data.Entities.Product
            {
                Id = productModel.Id,
                Name = productModel.Name,
                DeliveryPrice = productModel.DeliveryPrice,
                Description = productModel.Description,
                Price = productModel.Price
            };
        }
    }
}
