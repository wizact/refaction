using Microsoft.VisualStudio.TestTools.UnitTesting;
using refactor_me.Business.Mappers;

namespace refactor_me.Tests
{
    [TestClass]
    public class ProductMapperTests
    {
        [TestMethod]
        public void when_product_entity_is_null_then_a_default_model_should_be_returned()
        {
            // Arrange
            var productEntity = default(Data.Entities.Product);
            var productMapper = new ProductMapper();

            // Act:
            var model = productMapper.ToModel(productEntity);

            // Assert
            Assert.IsNull(model);
        }
    }
}
