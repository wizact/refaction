using Microsoft.VisualStudio.TestTools.UnitTesting;
using refactor_me.Business.Mappers;

namespace refactor_me.Tests
{
    [TestClass]
    public class ProductOptionMapperTests
    {
        [TestMethod]
        public void when_product_option_entity_is_null_then_a_default_model_should_be_returned()
        {
            // Arrange
            var productOptionEntity = default(Data.Entities.ProductOption);
            var productOptionMapper = new ProductOptionMapper();

            // Act:
            var model = productOptionMapper.ToModel(productOptionEntity);

            // Assert
            Assert.IsNull(model);
        }
    }
}
