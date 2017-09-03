using System.Collections.Generic;

namespace refactor_me.Data.Entities
{
    public class Products
    {
        public List<Product> Items { get; private set; }

        public Products()
        {
            Items = new List<Product>();
        }
    }
}