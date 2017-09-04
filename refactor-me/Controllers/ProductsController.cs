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
            new ProductValidator().ValidateProduct(product);

            _productService.CreateProduct(product);

            var response = new HttpResponseMessage(HttpStatusCode.Created);
            response.Headers.Location = new Uri($"/Products/{product.Id}", UriKind.Relative);
            return response;
        }

        [Route("{id}")]
        [HttpPut]
        public HttpResponseMessage Update(Guid id, Product product)
        {
            new ProductValidator().ValidateProduct(product);

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
    }
}
