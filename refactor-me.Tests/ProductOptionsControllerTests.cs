using MSTestExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using refactor_me.Business.Services.Interfaces;
using refactor_me.Controllers;
using refactor_me.Models;
using System;
using System.Web.Http;

namespace refactor_me.Tests
{
    [TestClass]
    public class ProductOptionsControllerTests: BaseTest
    {
        [TestMethod]
        public void when_returning_product_options_then_existence_of_product_should_get_validated_first()
        {
            // Arrange:
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(m => m.GetProductById(It.IsAny<Guid>())).Returns(default(Product));

            var productOptionServiceMock = new Mock<IProductOptionService>();
            var productsController = new ProductOptionsController(productServiceMock.Object, productOptionServiceMock.Object);

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
            var productsController = new ProductOptionsController(productServiceMock.Object, productOptionServiceMock.Object);

            // Act:
            Action getProductOption = () => productsController.GetOption(Guid.NewGuid(), Guid.NewGuid());

            // Assert:
            Assert.Throws<HttpResponseException>(getProductOption);
        }
    }
}
