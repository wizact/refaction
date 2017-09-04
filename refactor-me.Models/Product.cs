using System;
using System.ComponentModel.DataAnnotations;
namespace refactor_me.Models
{
    public class Product
    {
        private const string ProductNameRequiredErrorMessage = "Product name is mandatoy";
        private const string ProductNameTooLongErrorMessage = "Product name cannot be more than 100 characters";
        private const string ProductDescriptionTooLongErrorMessage = "Product description cannot be more than 500 characters";

        public Guid Id { get; set; }

        [Required (AllowEmptyStrings =false, ErrorMessage = ProductNameRequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = ProductNameTooLongErrorMessage)]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = ProductDescriptionTooLongErrorMessage)]
        public string Description { get; set; }


        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }
    }
}