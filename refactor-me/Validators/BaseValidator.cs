using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Linq;
using System.Web.Http;
using System.Net.Http.Formatting;
using refactor_me.Models;

namespace refactor_me.Validators
{
    public abstract class BaseValidator<TModelType>
    {
        readonly List<ValidationResult> _results = new List<ValidationResult>();
        private const string RequestBodyIsRequiredErrorMessage = "Request body is required";

        protected void Validate(TModelType model)
        {
            if (model == null)
            {
                _results.Add(new ValidationResult(RequestBodyIsRequiredErrorMessage));
                return;
            }

            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, _results, true);
        }

        protected void ThrowIfInvalid()
        {
            if (_results.Count > 0)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

                var errorMessage = new ValidationResponse { ErrorMessage = _results.Select(result => result.ErrorMessage).ToList() };
                message.Content = new ObjectContent(errorMessage.GetType(), errorMessage, new JsonMediaTypeFormatter());
                throw new HttpResponseException(message);
            }
        }
    }
}