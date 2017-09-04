using System;
using System.Data.SqlClient;
using refactor_me.Data.Entities;
using refactor_me.Data.Repositories.Interfaces;
using System.Collections.Generic;

namespace refactor_me.Data.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();

            using (var conn = GetNewConnection())
            {
                var cmd = new SqlCommand("select * from product", conn);
                conn.Open();

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    products.Add(MapProduct(rdr));
                }
            }

            return products;
        }

        public List<Product> SearchByProductName(string name)
        {
            var products = new List<Product>();

            using (var conn = GetNewConnection())
            {
                var cmd = new SqlCommand("select * from product where name like @ProductName", conn);
                cmd.Parameters.AddWithValue("@ProductName", $"%{name}%");

                conn.Open();

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    products.Add(MapProduct(rdr));
                }
            }

            return products;
        }

        public Product GetProductById(Guid id)
        {
            using (var conn = GetNewConnection())
            {
                var cmd = new SqlCommand("select * from product where id = @ProductId", conn);
                cmd.Parameters.AddWithValue("@ProductId", id);
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
                var cmd = new SqlCommand("select count(id) from product where id = @ProductId", conn);
                cmd.Parameters.AddWithValue("@ProductId", id);

                conn.Open();

                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public Guid CreateProduct(Product product)
        {
            using (var conn = GetNewConnection())
            {
                var cmd = new SqlCommand(
                    "insert into product (id, name, description, price, deliveryprice) values (@Id, @Name, @Description, @Price, @DeliveryPrice)",
                    conn);

                cmd.Parameters.AddWithValue("@Id", product.Id);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@DeliveryPrice", product.DeliveryPrice);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return product.Id;
        }

        public void UpdateProduct(Product product)
        {
            using (var conn = GetNewConnection())
            {
                var cmd = new SqlCommand("update product set name = @Name, description = @Description, price = @Price, deliveryprice = @DeliveryPrice where id = @Id", conn);

                cmd.Parameters.AddWithValue("@Id", product.Id);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@DeliveryPrice", product.DeliveryPrice);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(Guid id)
        {
            using (var conn = GetNewConnection())
            {
                var cmd = new SqlCommand("delete from product where id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}