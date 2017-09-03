using refactor_me.Data.Entities;
using System;

namespace refactor_me.Data.Repositories.Interfaces
{
    public interface IProductOptionRepository
    {
        ProductOptions GetProductOptionsForProduct(Guid productId);

        ProductOption GetProductOption(Guid productOptionId);

        void CreateProductOptionForProduct(ProductOption productOption);

        void UpdateProductOption(ProductOption productOption);

        void DeleteProductOption(Guid id);
    }
}
