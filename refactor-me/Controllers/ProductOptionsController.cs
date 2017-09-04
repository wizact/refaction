using refactor_me.Business.Services.Interfaces;
using refactor_me.Models;
using refactor_me.Validators;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductOptionsController : ApiController
    {
        private readonly IProductService _productService;
        private readonly IProductOptionService _productOptionService;

        public ProductOptionsController(IProductService productService, IProductOptionService productOptionService)
        {
            _productService = productService;
            _productOptionService = productOptionService;
        }

        [Route("{productId}/options")]
        [HttpGet]
        public ProductOptions GetOptions(Guid productId)
        {
            ValidateProductExists(productId);

            return _productOptionService.GetProductOptionsForProduct(productId);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            ValidateProductExists(productId);

            var productOption = _productOptionService.GetProductOption(id);

            if (productOption == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return productOption;
        }

        [Route("{productId}/options")]
        [HttpPost]
        public HttpResponseMessage CreateOption(Guid productId, ProductOption productOption)
        {
            ValidateProductExists(productId);

            new ProductOptionValidator().ValidateProductOption(productOption);

            productOption.Id = Guid.NewGuid();
            productOption.ProductId = productId;
            _productOptionService.CreateProductOptionForProduct(productOption);

            var response = new HttpResponseMessage(HttpStatusCode.Created);
            response.Headers.Location = new Uri($"/Products/{productId}/Options/{productOption.Id}", UriKind.Relative);
            return response;
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid productId, Guid id, ProductOption productOption)
        {
            ValidateProductExists(productId);

            var existingProductOption = _productOptionService.GetProductOption(id);
            new ProductOptionValidator().ValidateProductOption(productOption);

            if (existingProductOption == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            existingProductOption.Name = productOption.Name;
            existingProductOption.Description = productOption.Description;

            _productOptionService.UpdateProductOption(existingProductOption);
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid productId, Guid id)
        {
            ValidateProductExists(productId);
            _productOptionService.DeleteProductOption(id);
        }

        private void ValidateProductExists(Guid productId)
        {
            var product = _productService.GetProductById(productId);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}
