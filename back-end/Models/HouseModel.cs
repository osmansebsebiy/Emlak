using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmlakApiProject.Models
{
    public class HouseModel
    {
        public int houseId { get; set; }
        public string houseCode { get; set; }
        public string houseAddress { get; set; }
        public float housePrice { get; set; }
        public int houseBuiltDate { get; set; }
        public string houseHeating { get; set; }
        public string houseConditioning { get; set; }
        public string houseGarage { get; set; }
        public int houseVisitCount { get; set; }
        public string houseMainImg { get; set; }
        public int vendorId { get; set; }
        public List<string> images { get; set; }
        public string houseImg1 { get; set; }
        public string houseImg2 { get; set; }
        public string houseImg3 { get; set; }
        public string houseImg4 { get; set; }
    }
}