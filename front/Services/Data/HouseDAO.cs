using EmlakProject.Controllers;
using EmlakProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace EmlakProject.Services.Data
{
    public class HouseDAO
    {
        public HouseModel fetchOne(int id)
        {
            List<HouseModel> houses = new List<HouseModel>();

            using (SqlConnection connection = new SqlConnection(SecurityDAO.connectionString))
            {
                string sqlQuery = "SELECT * FROM HOUSE WHERE houseId="+id;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseModel house = new HouseModel();

                        house.houseId = reader.GetInt32(0);
                        house.houseCode = reader.GetString(1);
                        house.houseAddress = reader.GetString(2);
                        house.housePrice = Convert.ToSingle(reader.GetDouble(3));
                        house.houseBuiltDate = reader.GetInt32(4);
                        house.houseHeating = reader.GetString(5);
                        house.houseConditioning = reader.GetString(6);
                        house.houseGarage = reader.GetString(7);
                        house.houseVisitCount = reader.GetInt32(8);
                        house.houseMainImg = house.houseId + ".jpg";
                        house.houseType = reader.GetString(10);
                        house.vendorId = reader.GetInt32(11);
                        #region get images
                        house.houseImg1 = house.houseId + ".1.jpg";
                        house.houseImg2 = house.houseId + ".2.jpg";
                        house.houseImg3 = house.houseId + ".3.jpg";
                        house.houseImg4 = house.houseId + ".4.jpg";
                        #endregion

                        houses.Add(house);
                    }
                }
            }
            return houses[0];
        }
        public List<HouseModel> fetchMostVisited()
        {
            List<HouseModel> houses = new List<HouseModel>();

            using (SqlConnection connection = new SqlConnection(SecurityDAO.connectionString))
            {
                string sqlQuery = "SELECT Top 3 * FROM House order by houseVisitCount desc;";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseModel house = new HouseModel();

                        house.houseId = reader.GetInt32(0);
                        house.houseCode = reader.GetString(1);
                        house.houseAddress = reader.GetString(2);
                        house.housePrice = Convert.ToSingle(reader.GetDouble(3));
                        house.houseBuiltDate = reader.GetInt32(4);
                        house.houseHeating = reader.GetString(5);
                        house.houseConditioning = reader.GetString(6);
                        house.houseGarage = reader.GetString(7);
                        house.houseVisitCount = reader.GetInt32(8);
                        house.houseMainImg = house.houseId + ".jpg";
                        house.houseType = reader.GetString(10);
                        house.vendorId = reader.GetInt32(11);
                        #region get images
                        house.houseImg1 = house.houseId + ".1.jpg";
                        house.houseImg2 = house.houseId + ".2.jpg";
                        house.houseImg3 = house.houseId + ".3.jpg";
                        house.houseImg4 = house.houseId + ".4.jpg";
                        #endregion

                        houses.Add(house);
                    }
                }
            }
            #region api
            //List<HouseModel> houses = null;
            //using (var httpClientHandler = new HttpClientHandler())
            //{
            //    // NB: You should make this more robust by actually checking the certificate:
            //    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            //    using (var client = new HttpClient(httpClientHandler))
            //    {
            //        //using (var client = new HttpClient())
            //        {
            //            client.BaseAddress = new Uri("https://localhost:44361/api/House");
            //            //HTTP GET
            //            var responseTask = client.GetAsync("House");
            //            responseTask.Wait();

            //            var result = responseTask.Result;
            //            if (result.IsSuccessStatusCode)
            //            {
            //                var readTask = result.Content.ReadAsAsync<List<HouseModel>>();
            //                readTask.Wait();
            //            }
            //            else //web api sent error response 
            //            {
            //                //log response status here..

            //                houses = Enumerable.Empty<HouseModel>().ToList();

            //                //ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            //            }
            //        }
            //    }
            //}

            #endregion
            return houses.ToList();
        }
        public List<HouseModel> fetchMyHouses(int vendorId)
        {
            List<HouseModel> houses = new List<HouseModel>();

            using (SqlConnection connection = new SqlConnection(SecurityDAO.connectionString))
            {
                string sqlQuery = "SELECT * FROM HOUSE WHERE vendorId=" + vendorId;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseModel house = new HouseModel();

                        house.houseId = reader.GetInt32(0);
                        house.houseCode = reader.GetString(1);
                        house.houseAddress = reader.GetString(2);
                        house.housePrice = Convert.ToSingle(reader.GetDouble(3));
                        house.houseBuiltDate = reader.GetInt32(4);
                        house.houseHeating = reader.GetString(5);
                        house.houseConditioning = reader.GetString(6);
                        house.houseGarage = reader.GetString(7);
                        house.houseVisitCount = reader.GetInt32(8);
                        house.houseMainImg = house.houseId + ".jpg";
                        house.houseType = reader.GetString(10);
                        house.vendorId = reader.GetInt32(11);
                        #region get images
                        house.houseImg1 = house.houseId + ".1.jpg";
                        house.houseImg2 = house.houseId + ".2.jpg";
                        house.houseImg3 = house.houseId + ".3.jpg";
                        house.houseImg4 = house.houseId + ".4.jpg";
                        #endregion

                        houses.Add(house);
                    }
                }
            }
            return houses;
        }
        public List<HouseModel> fetchAllSell()
        {
            List<HouseModel> houses = new List<HouseModel>();

            using (SqlConnection connection = new SqlConnection(SecurityDAO.connectionString))
            {
                string sqlQuery = "SELECT * FROM HOUSE WHERE houseType='S'order by houseVisitCount desc;";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseModel house = new HouseModel();

                        house.houseId = reader.GetInt32(0);
                        house.houseCode = reader.GetString(1);
                        house.houseAddress = reader.GetString(2);
                        house.housePrice = Convert.ToSingle(reader.GetDouble(3));
                        house.houseBuiltDate = reader.GetInt32(4);
                        house.houseHeating = reader.GetString(5);
                        house.houseConditioning = reader.GetString(6);
                        house.houseGarage = reader.GetString(7);
                        house.houseVisitCount = reader.GetInt32(8);
                        house.houseMainImg = house.houseId + ".jpg";
                        house.houseType = reader.GetString(10);
                        house.vendorId = reader.GetInt32(11);

                        houses.Add(house);
                    }
                }
            }
            return houses;
        }
        public List<HouseModel> fetchAllRent()
        {
            List<HouseModel> houses = new List<HouseModel>();

            using (SqlConnection connection = new SqlConnection(SecurityDAO.connectionString))
            {
                string sqlQuery = "SELECT * FROM HOUSE WHERE houseType='R'order by houseVisitCount desc;";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseModel house = new HouseModel();

                        house.houseId = reader.GetInt32(0);
                        house.houseCode = reader.GetString(1);
                        house.houseAddress = reader.GetString(2);
                        house.housePrice = Convert.ToSingle(reader.GetDouble(3));
                        house.houseBuiltDate = reader.GetInt32(4);
                        house.houseHeating = reader.GetString(5);
                        house.houseConditioning = reader.GetString(6);
                        house.houseGarage = reader.GetString(7);
                        house.houseVisitCount = reader.GetInt32(8);
                        house.houseMainImg = house.houseId + ".jpg";
                        house.houseType = reader.GetString(10);
                        house.vendorId = reader.GetInt32(11);

                        houses.Add(house);
                    }
                }
            }
            return houses;
        }
        public int createHouse(HouseModel houseModel)
        {
            #region chk if house is exist
            List<HouseModel> houses = new List<HouseModel>();

            using (SqlConnection connection = new SqlConnection(SecurityDAO.connectionString))
            {
                string sqlQuery = "SELECT * FROM HOUSE WHERE houseCode='"+houseModel.houseCode+"' AND houseId!="+houseModel.houseId;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseModel house = new HouseModel();

                        house.houseId = reader.GetInt32(0);
                        house.houseCode = reader.GetString(1);
                        house.houseAddress = reader.GetString(2);
                        house.housePrice = Convert.ToSingle(reader.GetDouble(3));
                        house.houseBuiltDate = reader.GetInt32(4);
                        house.houseHeating = reader.GetString(5);
                        house.houseConditioning = reader.GetString(6);
                        house.houseGarage = reader.GetString(7);
                        house.houseVisitCount = reader.GetInt32(8);
                        house.houseMainImg = reader.GetString(9);
                        house.houseType = reader.GetString(10);
                        house.vendorId = reader.GetInt32(11);

                        houses.Add(house);
                    }
                }
            }
            #endregion

            if (houses.Count > 0)//house is exist
                return -1;
            else//house is not exist
            {
                #region insert/update
                using (SqlConnection connection = new SqlConnection(SecurityDAO.connectionString))
                {
                    
                    string sqlQuery = "";
                    if (houseModel.houseId == 0)
                        sqlQuery = "Insert into house values(@houseCode , @houseAddress , @housePrice , @houseBuiltDate , @houseHeating , @houseConditioning , " +
                                                               "@houseGarage , @houseVisitCount ,@houseMainImg , @houseType ,@vendorId)";
                    else
                    {
                        sqlQuery = "UPDATE HOUSE SET houseCode=@houseCode , houseAddress = @houseAddress , housePrice = @housePrice , houseBuiltDate = @houseBuiltDate , " +
                                   "houseHeating = @houseHeating , houseConditioning = @houseConditioning , houseGarage = @houseGarage , houseVisitCount =  @houseVisitCount " +
                                   ",houseMainImg = @houseMainImg , houseType = @houseType " +
                                   "WHERE houseId = @houseId;";
                    }
                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    command.Parameters.Add("@houseId", System.Data.SqlDbType.Int).Value = houseModel.houseId;
                    command.Parameters.Add("@houseCode", System.Data.SqlDbType.NVarChar, 10).Value = houseModel.houseCode;
                    command.Parameters.Add("@houseAddress", System.Data.SqlDbType.NVarChar, 50).Value = houseModel.houseAddress;
                    command.Parameters.Add("@housePrice", System.Data.SqlDbType.Float).Value = houseModel.housePrice;
                    command.Parameters.Add("@houseBuiltDate", System.Data.SqlDbType.Int).Value = houseModel.houseBuiltDate;
                    command.Parameters.Add("@houseHeating", System.Data.SqlDbType.NVarChar, 50).Value = houseModel.houseHeating;
                    command.Parameters.Add("@houseConditioning", System.Data.SqlDbType.NVarChar, 50).Value = houseModel.houseConditioning;
                    command.Parameters.Add("@houseGarage", System.Data.SqlDbType.NVarChar, 50).Value = houseModel.houseGarage;
                    command.Parameters.Add("@houseVisitCount", System.Data.SqlDbType.Int).Value = houseModel.houseVisitCount;
                    command.Parameters.Add("@houseMainImg", System.Data.SqlDbType.NVarChar, 1000).Value = houseModel.houseMainImg;
                    command.Parameters.Add("@houseType", System.Data.SqlDbType.NVarChar, 1).Value = houseModel.houseType;
                    command.Parameters.Add("@vendorId", System.Data.SqlDbType.Int).Value = SecurityDAO.loginUser.userId;

                    connection.Open();
                    int newID = command.ExecuteNonQuery();
                    #endregion

                #region get id 
                int houseId = 0;
                if (houseModel.houseId != 0)
                    houseId = houseModel.houseId;
                else
                    using (SqlConnection connection1 = new SqlConnection(SecurityDAO.connectionString))
                    {
                        string sqlQuery1 = "SELECT MAX(houseId) FROM HOUSE ";
                        SqlCommand command1 = new SqlCommand(sqlQuery1, connection1);
                        connection1.Open();
                        SqlDataReader reader1 = command1.ExecuteReader();
                        if (reader1.HasRows)
                        {
                            while (reader1.Read())
                            {
                                houseId = reader1.GetInt32(0);
                            }
                        }
                    }
                    #endregion

                #region save images
                string rootDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string Img_name = System.IO.Path.Combine(rootDirectory, "pic", houseModel.houseMainImg);
                try
                {
                    if (HomeController.isChangeMain)
                    {
                        if (File.Exists(System.IO.Path.Combine(rootDirectory + @"\pictures", houseId.ToString() + ".jpg")))
                            File.Delete(System.IO.Path.Combine(rootDirectory + @"\pictures", houseId.ToString() + ".jpg"));
                        File.Copy(Img_name, System.IO.Path.Combine(rootDirectory + @"\pictures", houseId.ToString() + ".jpg"));
                    }
                }
                catch { }

                string Img_name1 = System.IO.Path.Combine(rootDirectory, "pic", houseModel.houseImg1);
                try
                {
                    if (HomeController.isChangeFirst)
                    {
                        if(File.Exists(System.IO.Path.Combine(rootDirectory + @"\pictures", houseId.ToString() + ".1.jpg")))
                            File.Delete(System.IO.Path.Combine(rootDirectory + @"\pictures", houseId.ToString() + ".1.jpg"));
                        File.Copy(Img_name1, System.IO.Path.Combine(rootDirectory + @"\pictures", houseId.ToString() + ".1.jpg"));
                    }
                }
                catch { }

                string Img_name2 = System.IO.Path.Combine(rootDirectory, "pic", houseModel.houseImg2);
                try
                {
                    if (HomeController.isChangeSecond)
                    {
                        if(File.Exists(System.IO.Path.Combine(rootDirectory + @"\pictures", houseId.ToString() + ".2.jpg")))
                            File.Delete(System.IO.Path.Combine(rootDirectory + @"\pictures", houseId.ToString() + ".2.jpg"));
                        File.Copy(Img_name2, System.IO.Path.Combine(rootDirectory + @"\pictures", houseId.ToString() + ".2.jpg"));
                    }
                }
                catch { }

                string Img_name3 = System.IO.Path.Combine(rootDirectory, "pic", houseModel.houseImg3);
                try
                {
                    if (HomeController.isChangeThird)
                    {
                        if(File.Exists(System.IO.Path.Combine(rootDirectory + @"\pictures", houseId.ToString() + ".3.jpg")))
                            File.Delete(System.IO.Path.Combine(rootDirectory + @"\pictures", houseId.ToString() + ".3.jpg"));
                        File.Copy(Img_name3, System.IO.Path.Combine(rootDirectory + @"\pictures", houseId.ToString() + ".3.jpg"));
                    }
                }
                catch { }

                string Img_name4 = System.IO.Path.Combine(rootDirectory, "pic", houseModel.houseImg4);
                try
                {
                    if (HomeController.isChangeForth)
                    {
                        if(File.Exists(System.IO.Path.Combine(rootDirectory + @"\pictures", houseId.ToString() + ".4.jpg")))
                            File.Delete(System.IO.Path.Combine(rootDirectory + @"\pictures", houseId.ToString() + ".4.jpg"));
                        File.Copy(Img_name4, System.IO.Path.Combine(rootDirectory + @"\pictures", houseId.ToString() + ".4.jpg"));
                    }
                }
                catch { }
                #endregion

                return houseId;
            }
        }
    }
        internal List<HouseModel> SearchForName(string searchPhrase)
        {
            List<HouseModel> houses = new List<HouseModel>();

            using (SqlConnection connection = new SqlConnection(SecurityDAO.connectionString))
            {
                string sqlQuery = "SELECT * FROM HOUSE WHERE houseCode LIKE @SearchForMe OR houseAddress LIKE @SearchForMe OR housePrice LIKE @SearchForMe " +
                    "OR houseBuiltDate LIKE @SearchForMe OR houseHeating LIKE @SearchForMe OR houseConditioning LIKE @SearchForMe OR houseGarage LIKE @SearchForMe" ;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.Add("@SearchForMe", System.Data.SqlDbType.NVarChar).Value = "%"+searchPhrase+"%";
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HouseModel house = new HouseModel();

                        house.houseId = reader.GetInt32(0);
                        house.houseCode = reader.GetString(1);
                        house.houseAddress = reader.GetString(2);
                        house.housePrice = Convert.ToSingle(reader.GetDouble(3));
                        house.houseBuiltDate = reader.GetInt32(4);
                        house.houseHeating = reader.GetString(5);
                        house.houseConditioning = reader.GetString(6);
                        house.houseGarage = reader.GetString(7);
                        house.houseVisitCount = reader.GetInt32(8);
                        house.houseMainImg = house.houseId + ".jpg";
                        house.houseType = reader.GetString(10);
                        house.vendorId = reader.GetInt32(11);

                        houses.Add(house);
                    }
                }
            }
            return houses;
        }
        public int updateVisitCount(HouseModel houseModel)
        {
            int visitCount = houseModel.houseVisitCount+1;
            using (SqlConnection connection = new SqlConnection(SecurityDAO.connectionString))
            {
                string sqlQuery  = "UPDATE HOUSE SET houseVisitCount =  @houseVisitCount WHERE houseId="+houseModel.houseId;

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@houseVisitCount", System.Data.SqlDbType.Int).Value = visitCount;

                connection.Open();
                int newID = command.ExecuteNonQuery();

            }
            return houseModel.houseId;

        }
        internal int Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(SecurityDAO.connectionString))
            {
                string sqlQuery = "DELETE FROM HOUSE WHERE houseId = @houseId";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@houseId", System.Data.SqlDbType.Int).Value = id;

                connection.Open();
                int deletedID = command.ExecuteNonQuery();

                return deletedID;
            }
        }


    }  
}