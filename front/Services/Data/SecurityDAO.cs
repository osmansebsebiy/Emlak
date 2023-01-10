using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using EmlakProject.Models;

namespace EmlakProject.Services.Data
{
    public class SecurityDAO
    {
        public static UserModel loginUser = new UserModel();
        public static string connectionString = "Data Source=.;Initial Catalog=emlakDB;Integrated Security=True";
        internal bool FindByUser(UserModel user)
        {
            //return (user.Username == "Admin" && user.Password == "secret");
            bool success = false;

            string queryString = "Select * from Agent where userEmail = @Email And userPassword = @Password";

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString , connection);
                command.Parameters.Add("@Email"   , System.Data.SqlDbType.NVarChar,15).Value  = user.userEmail;
                command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 15).Value = user.userPassword;
               
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        success = true;
                        //SecurityDAO.loginUser = reader.
                        while (reader.Read())
                        {
                            UserModel userModel = new UserModel();

                            userModel.userId = reader.GetInt32(0);
                            userModel.userFullName = reader.GetString(1);
                            userModel.userEmail = reader.GetString(2);
                            userModel.userPassword = reader.GetString(3);
                            userModel.userPhone = reader.GetString(4);

                            SecurityDAO.loginUser = userModel;
                        }
                    }
                    else
                        success = false;
                    connection.Close();
                
                return success;
            }
        }

        internal UserModel createUser(UserModel userModel)
        {
            #region chk if user is exist
            List<UserModel> users = new List<UserModel>();

            using (SqlConnection connection = new SqlConnection(SecurityDAO.connectionString))
            {
                string sqlQuery = "SELECT * FROM AGENT WHERE userEmail='" + userModel.userEmail + "' AND userId!=" + userModel.userId;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserModel user_ = new UserModel();

                        user_.userId = reader.GetInt32(0);
                        user_.userFullName = reader.GetString(1);
                        user_.userEmail = reader.GetString(2);
                        user_.userPassword = reader.GetString(3);
                        user_.userPhone = reader.GetString(4);

                        users.Add(user_);
                    }
                }
            }
            #endregion

            if (users.Count > 0)
                return null;
            else
            {
                #region add new
                using (SqlConnection connection = new SqlConnection(SecurityDAO.connectionString))
                {
                    string sqlQuery = "Insert into Agent values(@userFullName , @userEmail , @userPassword , @userPhone )";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    command.Parameters.Add("@userFullName", System.Data.SqlDbType.NVarChar, 15).Value = userModel.userFullName;
                    command.Parameters.Add("@userEmail", System.Data.SqlDbType.NVarChar, 15).Value = userModel.userEmail;
                    command.Parameters.Add("@userPassword", System.Data.SqlDbType.NVarChar, 15).Value = userModel.userPassword;
                    command.Parameters.Add("@userPhone", System.Data.SqlDbType.NVarChar, 15).Value = userModel.userPhone;

                    connection.Open();
                    int newID = command.ExecuteNonQuery();
                   
                }
                #endregion

                #region get user id
                UserModel returnedUser = new UserModel();

                using (SqlConnection connection1 = new SqlConnection(SecurityDAO.connectionString))
                {
                    string sqlQuery1 = "SELECT * FROM AGENT WHERE userId = (SELECT MAX(userId) FROM AGENT);";
                    SqlCommand command1 = new SqlCommand(sqlQuery1, connection1);
                    connection1.Open();
                    SqlDataReader reader1 = command1.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        while (reader1.Read())
                        {
                            returnedUser.userId = reader1.GetInt32(0);
                            returnedUser.userFullName = reader1.GetString(1);
                            returnedUser.userEmail = reader1.GetString(2);
                            returnedUser.userPassword = reader1.GetString(3);
                            returnedUser.userPhone = reader1.GetString(4);
                        }
                    }
                }
                #endregion

                return returnedUser;
            }

        }

        public UserModel fetchOne(int id)
        {
            UserModel user_ = new UserModel();

            using (SqlConnection connection = new SqlConnection(SecurityDAO.connectionString))
            {
                string sqlQuery = "SELECT * FROM AGENT WHERE userId=" + id;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user_.userId = reader.GetInt32(0);
                        user_.userFullName = reader.GetString(1);
                        user_.userEmail = reader.GetString(2);
                        user_.userPassword = reader.GetString(3);
                        user_.userPhone = reader.GetString(4);
                    }
                }
            }
            return user_;
        }
    }
}