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
                var cmd = new SqlCommand("insert into productoption (id, productid, name, description) values (@ProductOptionId, @ProductId , @Name, @Description)", conn);

                cmd.Parameters.AddWithValue("@ProductOptionId", productOption.Id);
                cmd.Parameters.AddWithValue("@ProductId", productOption.ProductId);
                cmd.Parameters.AddWithValue("@Name", productOption.Name);
                cmd.Parameters.AddWithValue("@Description", productOption.Description);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProductOption(Guid id)
        {
            using (var conn = GetNewConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("delete from productoption where id = @ProductOptionId", conn);
                cmd.Parameters.AddWithValue("@ProductOptionId", id);
                cmd.ExecuteReader();
            }
        }

        public void DeleteProductOptionByProductId(Guid productId)
        {
            using (var conn = GetNewConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("delete from productoption where ProductId = @ProductId", conn);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                cmd.ExecuteReader();
            }
        }

        public ProductOption GetProductOption(Guid productOptionId)
        {
            using (var conn = GetNewConnection())
            {
                var cmd = new SqlCommand("select * from productoption where id = @ProductOptionId", conn);
                cmd.Parameters.AddWithValue("@ProductOptionId", productOptionId);
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
                var cmd = new SqlCommand("select * from productoption where productid = @ProductId", conn);
                cmd.Parameters.AddWithValue("@ProductId", productId);

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
                var cmd = new SqlCommand("update productoption set name = @Name, description = @Description where id = @productOptionId", conn);

                cmd.Parameters.AddWithValue("@Name", productOption.Name);
                cmd.Parameters.AddWithValue("@Description", productOption.Description);
                cmd.Parameters.AddWithValue("@productOptionId", productOption.Id);

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