using System;
using System.Net;
using System.Web.Http;

namespace refactor_me.Validators
{
    public class GuidValidator
    {
        public void Validate(Guid guid)
        {
            if (guid == Guid.Empty || guid == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}