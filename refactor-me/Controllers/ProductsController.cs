using System;
using System.Net;
using System.Web.Http;
using refactor_me.Models;
using refactor_me.Business.Services.Interfaces;

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
            var product = _productService.GetProductById(id);

            // TODO: Test this
            if (product.Id == Guid.Empty)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return product;
        }

        [Route]
        [HttpPost]
        public void Create(Product product)
        {
            // Validate data
            // Set Id
            _productService.CreateProduct(product);
            // Set the http status
            // Set the id in th elocation header
        }

        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, Product product)
        {
            // investigate model binding
            // validate the id
            var existingProduct = _productService.GetProductById(id);

            if (existingProduct.Id == Guid.Empty)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.DeliveryPrice = product.DeliveryPrice;

            _productService.UpdateProduct(existingProduct);

            // Set the http status
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            _productService.DeleteProduct(id);
        }

        [Route("{productId}/options")]
        [HttpGet]
        public ProductOptions GetOptions(Guid productId)
        {
            //TODO: Validate product Id is valid
            return _productOptionService.GetProductOptionsForProduct(productId);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            // Validate Product Id
            var productOption = _productOptionService.GetProductOption(id);

            if (productOption.Id == Guid.Empty)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return productOption;
        }

        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOption productOption)
        {
            // validate product id is valid
            productOption.ProductId = productId;
            _productOptionService.CreateProductOptionForProduct(productOption);
            
            // Set the location header
            // Return status code for created
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid id, ProductOption option)
        {
            // Validate option and product id exist and they are related

            var existingProductOption = _productOptionService.GetProductOption(id);

            existingProductOption.Name = option.Name;
            existingProductOption.Description = option.Description;

            _productOptionService.UpdateProductOption(existingProductOption);
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            // Validate the product id and id and if they are related
            _productOptionService.DeleteProductOption(id);
        }
    }
}
