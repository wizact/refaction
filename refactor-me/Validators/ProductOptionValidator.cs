using refactor_me.Models;

namespace refactor_me.Validators
{
    public class ProductOptionValidator: BaseValidator<ProductOption>
    {
        public new void Validate(ProductOption model)
        {
            base.Validate(model);
            ThrowIfInvalid();
        }
    }
}