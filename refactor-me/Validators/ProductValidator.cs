using refactor_me.Models;

namespace refactor_me.Validators
{
    public class ProductValidator: BaseValidator<Product>
    {
        public void ValidateProduct(Product model)
        {
            Validate(model);
            ThrowIfInvalid();
        }
    }
}