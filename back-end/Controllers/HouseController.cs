using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using EmlakApiProject.Models;

namespace EmlakApiProject.Controllers
{
    public class HouseController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

        [HttpGet]
        public string Get()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM HOUSE;",con);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            return JsonConvert.SerializeObject(dt);
        }
        [HttpPost]
        public string Post(HouseModel houseModel)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand("INSERT INTO HOUSE VALUES("+houseModel.houseCode+","+")", con);
            con.Open();
            int id = cmd.ExecuteNonQuery();
            if (id > 0)
                response = "House inserted";
            else
                response = "Something wrong";
            con.Close();
            return response;
        }
    }
}
