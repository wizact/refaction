using refactor_me.Models;

namespace refactor_me.Validators
{
    public class ProductValidator: BaseValidator<Product>
    {
        public new void Validate(Product model)
        {
            base.Validate(model);
            ThrowIfInvalid();
        }
    }
}