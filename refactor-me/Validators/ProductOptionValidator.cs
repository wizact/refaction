using refactor_me.Models;

namespace refactor_me.Validators
{
    public class ProductOptionValidator: BaseValidator<ProductOption>
    {
        public void ValidateProductOption(ProductOption model)
        {
            Validate(model);
            ThrowIfInvalid();
        }
    }
}