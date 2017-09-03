using refactor_me.Models;
using System;

namespace refactor_me.Services.Interfaces
{
    public interface IProductOptionService
    {
        void CreateProductOptionForProduct(ProductOption productOption);

        void DeleteProductOption(Guid id);

        ProductOption GetProductOption(Guid productOptionId);

        ProductOptions GetProductOptionsForProduct(Guid productId);

        void UpdateProductOption(ProductOption productOption);
    }
}
