using System;
using System.Data.SqlClient;
using refactor_me.Data.Entities;
using refactor_me.Data.Repositories.Interfaces;

namespace refactor_me.Data.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public Products GetAllProducts()
        {
            var products = new Products();

            using (var conn = GetNewConnection())
            {
                var cmd = new SqlCommand("select * from product", conn);
                conn.Open();

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    products.Items.Add(MapProduct(rdr));
                }
            }

            return products;
        }

        public Products SearchByProductName(string name)
        {
            var products = new Products();

            using (var conn = GetNewConnection())
            {
                var cmd = new SqlCommand($"select id from product where name like '%{name}%'", conn);
                conn.Open();

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    products.Items.Add(MapProduct(rdr));
                }
            }

            return products;
        }

        public Product GetProductById(Guid id)
        {
            using (var conn = GetNewConnection())
            {
                var cmd = new SqlCommand($"select * from product where id = '{id}'", conn);
                conn.Open();

                var rdr = cmd.ExecuteReader();
                if (!rdr.Read())
                    return default(Product);

                var product = MapProduct(rdr);

                return product;
            }
        }

        private Product MapProduct(SqlDataReader rdr)
        {
            var product = new Product
            {
                Id = Guid.Parse(rdr["Id"].ToString()),
                Name = rdr["Name"].ToString(),
                Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString(),
                Price = decimal.Parse(rdr["Price"].ToString()),
                DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString())
            };
            return product;
        }

        private bool ProductExists(Guid id)
        {
            using (var conn = GetNewConnection())
            {
                var cmd = new SqlCommand($"select count(id) from product where id = '{id}'", conn);
                conn.Open();

                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public Guid CreateProduct(Product product)
        {
            using (var conn = GetNewConnection())
            {
                var cmd = new SqlCommand(
                    $"insert into product (id, name, description, price, deliveryprice) values ('{product.Id}', '{product.Name}', '{product.Description}', {product.Price}, {product.DeliveryPrice})",
                    conn);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return product.Id;
        }

        public void UpdateProduct(Product product)
        {
            using (var conn = GetNewConnection())
            {
                var cmd = new SqlCommand($"update product set name = '{product.Name}', description = '{product.Description}', price = {product.Price}, deliveryprice = {product.DeliveryPrice} where id = '{product.Id}'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(Guid id)
        {
            using (var conn = GetNewConnection())
            {
                conn.Open();
                var cmd = new SqlCommand($"delete from product where id = '{id}'", conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}