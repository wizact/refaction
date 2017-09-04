using System;
using System.Net;
using System.Web.Http;
using refactor_me.Models;
using refactor_me.Business.Services.Interfaces;
using refactor_me.Validators;
using System.Net.Http;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;
        private readonly IProductOptionService _productOptionService;

        public ProductsController(IProductService productService, IProductOptionService productOptionService)
        {
            _productService = productService;
            _productOptionService = productOptionService;
        }

        [Route]
        [HttpGet]
        public Products GetAll()
        {
            return _productService.GetAllProducts();
        }

        [Route]
        [HttpGet]
        public Products SearchByName(string name)
        {
            return _productService.SearchByProductName(name);
        }

        [Route("{id}")]
        [HttpGet]
        public Product GetProduct(Guid id)
        {
            new GuidValidator().Validate(id);

            var product = _productService.GetProductById(id);

            if (product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return product;
        }

        [Route]
        [HttpPost]
        public HttpResponseMessage Create(Product product)
        {
            product.Id = Guid.NewGuid();
            new ProductValidator().Validate(product);

            _productService.CreateProduct(product);

            var response = new HttpResponseMessage(HttpStatusCode.Created);
            response.Headers.Location = new Uri($"/Products/{product.Id}", UriKind.Relative);
            return response;
        }

        [Route("{id}")]
        [HttpPut]
        public HttpResponseMessage Update(Guid id, Product product)
        {
            new ProductValidator().Validate(product);

            var existingProduct = _productService.GetProductById(id);

            if (existingProduct == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.DeliveryPrice = product.DeliveryPrice;

            _productService.UpdateProduct(existingProduct);

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            _productOptionService.DeleteProductOptionByProductId(id);
            _productService.DeleteProduct(id);
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

            new ProductOptionValidator().Validate(productOption);

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
            new ProductOptionValidator().Validate(productOption);

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
