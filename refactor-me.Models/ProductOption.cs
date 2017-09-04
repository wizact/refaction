using System;
using System.ComponentModel.DataAnnotations;

namespace refactor_me.Models
{
    public class ProductOption
    {
        private const string ProductIsRequiredErrorMessage = "Product option requires a valid product parent";
        private const string ProductOptionNameIsRequiredErrorMessage = "Product option name is required";
        private const string ProductOptionNameTooLongErrorMessage = "Product option name cannot be more than 100 characters";
        private const string ProductOptionDescriptionTooLongErrorMessage = "Product option description cannot be more than 500 characters";

        public Guid Id { get; set; }

        [Required(ErrorMessage = ProductIsRequiredErrorMessage)]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = ProductOptionNameIsRequiredErrorMessage)]
        [MaxLength(100, ErrorMessage = ProductOptionNameTooLongErrorMessage)]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = ProductOptionDescriptionTooLongErrorMessage)]
        public string Description { get; set; }
    }
}