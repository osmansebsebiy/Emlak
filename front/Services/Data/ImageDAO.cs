using EmlakProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmlakProject.Services.Data
{
    public class ImageDAO
    {
        public List<ImageModel> GetHouseDetails(int houseId)
        {
            List<ImageModel> images = new List<ImageModel>();

            using (SqlConnection connection = new SqlConnection(SecurityDAO.connectionString))
            {
                string sqlQuery = "SELECT * FROM IMAGE WHERE houseId= @id;";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = houseId;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ImageModel image = new ImageModel();

                        image.imageId = reader.GetInt32(0);
                        image.imagePath = reader.GetString(1);
                        image.houseId = reader.GetInt32(2);

                        images.Add(image);
                    }
                }
            }
            return images;
        }
    }
}