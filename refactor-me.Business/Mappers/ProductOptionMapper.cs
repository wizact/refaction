using System;
using refactor_me.Business.Mappers.Interfaces;

namespace refactor_me.Business.Mappers
{
    public class ProductOptionMapper: IMapper<Models.ProductOption, Data.Entities.ProductOption>
    {
        public Models.ProductOption ToModel(Data.Entities.ProductOption productOptionEntity)
        {
            if (productOptionEntity == null)
            {
                return default(Models.ProductOption);
            }

            return new Models.ProductOption
            {
                Id = productOptionEntity.Id,
                ProductId = productOptionEntity.ProductId,
                Name = productOptionEntity.Name,
                Description = productOptionEntity.Description
            };
        }

        public Data.Entities.ProductOption ToEntity(Models.ProductOption productOptionModel)
        {
            return new Data.Entities.ProductOption
            {
                Id = productOptionModel.Id,
                ProductId = productOptionModel.ProductId,
                Name = productOptionModel.Name,
                Description = productOptionModel.Description
            };
        }
    }
}
