using EmlakProject.Controllers;
using EmlakProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmlakProject.Services.Data
{
    public class MessageDAO
    {
        internal MessageModel createMessage(MessageModel message)
        {
            MessageModel mainMsg = new MessageModel();

            #region add new
            using (SqlConnection connection = new SqlConnection(SecurityDAO.connectionString))
            {
                message.date = DateTime.Now.ToShortDateString();
                message.time = DateTime.Now.ToShortTimeString();
                message.msgType = HomeController.msgType;
                SecurityDAO sec = new SecurityDAO();
                if (HomeController.msgType == "m")
                {
                    UserModel user_ = sec.fetchOne(HomeController.venId);

                    message.customerName = SecurityDAO.loginUser.userFullName;
                    message.customerId = SecurityDAO.loginUser.userId;
                    message.vendorName = user_.userFullName;
                    message.vendorId = user_.userId;
                    message.vendorMobile = user_.userPhone;
                    message.vendorEmail = user_.userEmail;
                }
                else if (HomeController.msgType == "r")
                {
                    MessageDAO messageDAO = new MessageDAO();
                    mainMsg = messageDAO.fetchOne(HomeController.messageId);

                    UserModel vendor = sec.fetchOne(mainMsg.customerId);
                    UserModel customer = sec.fetchOne(mainMsg.vendorId);
                    message.customerName = customer.userFullName;
                    message.customerId = mainMsg.vendorId;
                    message.vendorName = vendor.userFullName;
                    message.vendorId = mainMsg.customerId;
                    message.vendorMobile = vendor.userPhone;
                    message.vendorEmail = vendor.userEmail;
                }

                string sqlQuery = "Insert into MESSAGE values(@msgTitle ,@msgContent , @vendorId , @customerId , @date , @time , @msgType )";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                string title = message.msgTitle;
                if (message.msgType == "r")
                    title = title + "(Reply :" + mainMsg.msgTitle+")";
                command.Parameters.Add("@msgTitle", System.Data.SqlDbType.NVarChar, 50).Value = title;
                command.Parameters.Add("@msgContent", System.Data.SqlDbType.NVarChar, 100).Value = message.msgContent;
                command.Parameters.Add("@vendorId", System.Data.SqlDbType.Int).Value = message.vendorId;
                command.Parameters.Add("@customerId", System.Data.SqlDbType.Int).Value = message.customerId;
                command.Parameters.Add("@date", System.Data.SqlDbType.NVarChar, 10).Value = message.date;
                command.Parameters.Add("@time", System.Data.SqlDbType.NVarChar, 10).Value = message.time;
                command.Parameters.Add("@msgType", System.Data.SqlDbType.NVarChar, 10).Value = message.msgType;

                connection.Open();
                int newID = command.ExecuteNonQuery();

            }
            #endregion

            #region get message id
            MessageModel returnedMessage = new MessageModel();

            using (SqlConnection connection1 = new SqlConnection(SecurityDAO.connectionString))
            {
                string sqlQuery1 = "SELECT * FROM MESSAGE WHERE msgId = (SELECT MAX(msgId) FROM MESSAGE);";
                SqlCommand command1 = new SqlCommand(sqlQuery1, connection1);
                connection1.Open();
                SqlDataReader reader1 = command1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        returnedMessage.msgId = reader1.GetInt32(0);
                        returnedMessage.msgTitle = reader1.GetString(1);
                        returnedMessage.msgContent = reader1.GetString(2);
                        returnedMessage.vendorId = reader1.GetInt32(3);
                        returnedMessage.customerId = reader1.GetInt32(4);
                        returnedMessage.date = reader1.GetString(5);
                        returnedMessage.time = reader1.GetString(6);
                        returnedMessage.msgType = reader1.GetString(7);
                    }
                }
            }
            #endregion

            return returnedMessage;
        }

        public List<MessageModel> fetchInbox(int vendorId)
        {
            List<MessageModel> messages = new List<MessageModel>();

            using (SqlConnection connection = new SqlConnection(SecurityDAO.connectionString))
            {
                string sqlQuery = "SELECT msgId,msgTitle,msgContent,vendorId,customerId,date,time,msgType,userFullName FROM MESSAGE,AGENT " +
                    "WHERE AGENT.userId=MESSAGE.customerId And vendorId=" + vendorId;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MessageModel message = new MessageModel();

                        message.msgId = reader.GetInt32(0);
                        message.msgTitle = reader.GetString(1);
                        message.msgContent = reader.GetString(2);
                        message.vendorId = reader.GetInt32(3);
                        message.customerId = reader.GetInt32(4);
                        message.date = reader.GetString(5);
                        message.time = reader.GetString(6);
                        message.msgType = reader.GetString(7);
                        message.vendorName = SecurityDAO.loginUser.userFullName;
                        message.vendorMobile = SecurityDAO.loginUser.userPhone;
                        message.vendorEmail = SecurityDAO.loginUser.userEmail;
                        message.customerName = reader.GetString(8);

                        messages.Add(message);
                    }
                }
            }
            return messages;
        }

        public MessageModel fetchOne(int id)
        {
            List<MessageModel> messages = new List<MessageModel>();

            using (SqlConnection connection = new SqlConnection(SecurityDAO.connectionString))
            {
                string sqlQuery = "SELECT * FROM MESSAGE WHERE msgId=" + id;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MessageModel message = new MessageModel();

                        message.msgId = reader.GetInt32(0);
                        message.msgTitle = reader.GetString(1);
                        message.msgContent = reader.GetString(2);
                        message.vendorId = reader.GetInt32(3);
                        message.customerId = reader.GetInt32(4);
                        message.date = reader.GetString(5);
                        message.time = reader.GetString(6);
                        message.msgType = reader.GetString(7);

                        messages.Add(message);
                    }
                }
            }
            return messages[0];
        }

        public List<MessageModel> fetchSent(int customerId)
        {
            List<MessageModel> messages = new List<MessageModel>();

            using (SqlConnection connection = new SqlConnection(SecurityDAO.connectionString))
            {
                string sqlQuery = "SELECT msgId,msgTitle,msgContent,vendorId,customerId,date,time,msgType,userFullName FROM MESSAGE,AGENT " +
                    "WHERE AGENT.userId=MESSAGE.vendorId And customerId=" + customerId;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MessageModel message = new MessageModel();

                        message.msgId = reader.GetInt32(0);
                        message.msgTitle = reader.GetString(1);
                        message.msgContent = reader.GetString(2);
                        message.vendorId = reader.GetInt32(3);
                        message.customerId = reader.GetInt32(4);
                        message.date = reader.GetString(5);
                        message.time = reader.GetString(6);
                        message.msgType = reader.GetString(7);
                        message.vendorName = SecurityDAO.loginUser.userFullName;
                        message.vendorMobile = SecurityDAO.loginUser.userPhone;
                        message.vendorEmail = SecurityDAO.loginUser.userEmail;
                        message.customerName = reader.GetString(8);

                        messages.Add(message);
                    }
                }
            }
            return messages;
        }
    }
}