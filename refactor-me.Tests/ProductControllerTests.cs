using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using refactor_me.Business.Services.Interfaces;
using refactor_me.Models;
using refactor_me.Controllers;
using MSTestExtensions;
using System.Web.Http;
using System.Net;

namespace refactor_me.Tests
{
    [TestClass]
    public class ProductControllerTests: BaseTest
    {
        [TestMethod]
        public void when_product_does_not_exist_should_return_not_found()
        {
            // Arrange:
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(m => m.GetProductById(It.IsAny<Guid>())).Returns(default(Product));
            var productOptionServiceMock = new Mock<IProductOptionService>();
            var productsController = new ProductsController(productServiceMock.Object, productOptionServiceMock.Object);

            // Act:
            Action getProduct = () => productsController.GetProduct(Guid.NewGuid());

            // Assert:
            Assert.Throws<HttpResponseException>(getProduct);
        }

        [TestMethod]
        public void when_product_exists_then_it_should_be_returned()
        {
            // Arrange:
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(m => m.GetProductById(It.IsAny<Guid>())).Returns(new Product { Name = "iPhone" });
            var productOptionServiceMock = new Mock<IProductOptionService>();
            var productsController = new ProductsController(productServiceMock.Object, productOptionServiceMock.Object);

            // Act:
            var product = productsController.GetProduct(Guid.NewGuid());

            // Assert:
            Assert.AreEqual(product.Name, "iPhone");
        }

        [TestMethod]
        public void when_product_is_getting_created_then_it_should_get_validated()
        {
            // Arrange:
            var productServiceMock = new Mock<IProductService>();
            var productOptionServiceMock = new Mock<IProductOptionService>();
            var productsController = new ProductsController(productServiceMock.Object, productOptionServiceMock.Object);
            var productToCreate = new Product { Name = string.Empty, Description = string.Empty };

            // Act:
            Action createProduct = () => productsController.Create(productToCreate);

            // Assert:
            Assert.Throws<HttpResponseException>(createProduct);
        }

        [TestMethod]
        public void when_a_valid_product_is_getting_created_the_correct_status_should_get_returned()
        {
            // Arrange:
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(m => m.CreateProduct(It.IsAny<Product>())).Returns(Guid.NewGuid());
            var productOptionServiceMock = new Mock<IProductOptionService>();
            var productsController = new ProductsController(productServiceMock.Object, productOptionServiceMock.Object);
            var productToCreate = new Product { Name = "iPhone", Description = "Good Condition" };

            // Act:
            var createProductResponse = productsController.Create(productToCreate);

            // Assert:
            Assert.AreEqual(HttpStatusCode.Created, createProductResponse.StatusCode);
        }

        [TestMethod]
        public void when_product_is_getting_update_then_it_should_get_validated_first()
        {
            // Arrange:
            var productServiceMock = new Mock<IProductService>();
            var productOptionServiceMock = new Mock<IProductOptionService>();
            var productsController = new ProductsController(productServiceMock.Object, productOptionServiceMock.Object);
            var productToUpdate = new Product { Name = string.Empty, Description = string.Empty };

            // Act:
            Action updateProduct = () => productsController.Update(Guid.NewGuid() ,productToUpdate);

            // Assert:
            Assert.Throws<HttpResponseException>(updateProduct);
        }

        [TestMethod]
        public void when_a_non_existent_product_is_getting_updated_then_not_found_should_be_returned()
        {
            // Arrange:
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(m => m.GetProductById(It.IsAny<Guid>())).Returns(default(Product));
            productServiceMock.Setup(m => m.UpdateProduct(It.IsAny<Product>()));

            var productOptionServiceMock = new Mock<IProductOptionService>();
            var productsController = new ProductsController(productServiceMock.Object, productOptionServiceMock.Object);
            var productToUpdate = new Product { Name = string.Empty, Description = string.Empty };

            // Act:
            Action updateProduct = () => productsController.Update(Guid.NewGuid(), productToUpdate);

            // Assert:
            Assert.Throws<HttpResponseException>(updateProduct);
        }

        [TestMethod]
        public void when_a_valid_product_is_getting_updated_then_correct_status_code_should_be_returned()
        {
            // Arrange:
            var validExistingProduct = new Product { Name = "iPhone", Description = "Good condition" };
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(m => m.GetProductById(It.IsAny<Guid>())).Returns(validExistingProduct);
            productServiceMock.Setup(m => m.UpdateProduct(It.IsAny<Product>()));

            var productOptionServiceMock = new Mock<IProductOptionService>();
            var productsController = new ProductsController(productServiceMock.Object, productOptionServiceMock.Object);
            var productToUpdate = validExistingProduct;

            // Act:
            var updateProductResponse = productsController.Update(Guid.NewGuid(), productToUpdate);

            // Assert:
            Assert.AreEqual(HttpStatusCode.NoContent, updateProductResponse.StatusCode);
        }

        [TestMethod]
        public void when_returning_product_options_then_existence_of_product_should_get_validated_first()
        {
            // Arrange:
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(m => m.GetProductById(It.IsAny<Guid>())).Returns(default(Product));

            var productOptionServiceMock = new Mock<IProductOptionService>();
            var productsController = new ProductsController(productServiceMock.Object, productOptionServiceMock.Object);

            // Act:
            Action getProductOptions = () => productsController.GetOptions(Guid.NewGuid());

            // Assert:
            Assert.Throws<HttpResponseException>(getProductOptions);
        }

        [TestMethod]
        public void when_returning_a_product_option_then_existence_of_product_should_get_validated_first()
        {
            // Arrange:
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(m => m.GetProductById(It.IsAny<Guid>())).Returns(default(Product));

            var productOptionServiceMock = new Mock<IProductOptionService>();
            var productsController = new ProductsController(productServiceMock.Object, productOptionServiceMock.Object);

            // Act:
            Action getProductOption = () => productsController.GetOption(Guid.NewGuid(), Guid.NewGuid());

            // Assert:
            Assert.Throws<HttpResponseException>(getProductOption);
        }
    }
}
