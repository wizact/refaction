using System;
using refactor_me.Data.Entities;
using System.Data.SqlClient;
using refactor_me.Data.Repositories.Interfaces;
using System.Collections.Generic;

namespace refactor_me.Data.Repositories
{
    public class ProductOptionRepository : BaseRepository, IProductOptionRepository
    {
        public void CreateProductOptionForProduct(ProductOption productOption)
        {
            using (var conn = GetNewConnection())
            {
                var cmd = new SqlCommand($"insert into productoption (id, productid, name, description) values ('{productOption.Id}', '{productOption.ProductId}', '{productOption.Name}', '{productOption.Description}')", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProductOption(Guid id)
        {
            using (var conn = GetNewConnection())
            {
                conn.Open();
                var cmd = new SqlCommand($"delete from productoption where id = '{id}'", conn);
                cmd.ExecuteReader();
            }
        }

        public void DeleteProductOptionByProductId(Guid productId)
        {
            using (var conn = GetNewConnection())
            {
                conn.Open();
                var cmd = new SqlCommand($"delete from productoption where ProductId = '{productId}'", conn);
                cmd.ExecuteReader();
            }
        }

        public ProductOption GetProductOption(Guid productOptionId)
        {
            using (var conn = GetNewConnection())
            {
                var cmd = new SqlCommand($"select * from productoption where id = '{productOptionId}'", conn);
                conn.Open();

                var rdr = cmd.ExecuteReader();
                if (!rdr.Read())
                    return default(ProductOption);

                return MapToProductOption(rdr);
            }
        }

        public List<ProductOption> GetProductOptionsForProduct(Guid productId)
        {
            var productOptions = new List<ProductOption>();

            using (var conn = GetNewConnection())
            {
                var cmd = new SqlCommand($"select * from productoption where productid = '{productId}'", conn);
                conn.Open();

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    productOptions.Add(MapToProductOption(rdr));
                }
            }

            return productOptions;
        }

        public void UpdateProductOption(ProductOption productOption)
        {
            using (var conn = GetNewConnection())
            {
                var cmd = new SqlCommand($"update productoption set name = '{productOption.Name}', description = '{productOption.Description}' where id = '{productOption.Id}'", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private ProductOption MapToProductOption(SqlDataReader rdr)
        {
            return new ProductOption
            {
                Id = Guid.Parse(rdr["Id"].ToString()),
                ProductId = Guid.Parse(rdr["ProductId"].ToString()),
                Name = rdr["Name"].ToString(),
                Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString()
            };
        }

    }
}