using System.Collections.Generic;

namespace refactor_me.Models
{
    public class Products
    {
        public List<Product> Items { get; }

        public Products()
        {
            Items = new List<Product>();
        }
    }
}