using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmlakProject.Models
{
    public class HouseModel
    {
        public int houseId { get; set; }
        [Required]
        [DisplayName("Code")]
        public string houseCode { get; set; }
        [Required]
        [DisplayName("Address")]
        public string houseAddress { get; set; }
        [Required]
        [DisplayName("Price")]
        public float housePrice { get; set; }
        [Required]
        [DisplayName("Built Date")]
        public int houseBuiltDate { get; set; }
        [Required]
        [DisplayName("Heating")]
        public string houseHeating { get; set; }
        [Required]
        [DisplayName("Conditioning")]
        public string houseConditioning { get; set; }
        [Required]
        [DisplayName("Garage")]
        public string houseGarage { get; set; }
        public int houseVisitCount { get; set; }
        [Required]
        [DisplayName("Main Photo")]
        public string houseMainImg { get; set; }
        [DisplayName("Type")]
        public string houseType { get; set; }
        public int vendorId { get; set; }
        [DisplayName("Interior photos")]
        public List<string> images { get; set; }
        [DisplayName("Photo1")]
        public string houseImg1 { get; set; }
        [DisplayName("Photo2")]
        public string houseImg2 { get; set; }
        [DisplayName("Photo3")]
        public string houseImg3 { get; set; }
        [DisplayName("Photo4")]
        public string houseImg4 { get; set; }
        public HouseModel()
        {
           
        }
        public HouseModel(int houseId, string houseCode, string houseAddress, float housePrice, int houseBuiltDate, string houseHeating, string houseConditioning, 
                          string houseGarage, int houseVisitCount, string houseMainImg, string houseType, int vendorId , 
                          string houseImg1 , string houseImg2 , string houseImg3, string houseImg4)
        {
            this.houseId = houseId;
            this.houseCode = houseCode;
            this.houseAddress = houseAddress;
            this.housePrice = housePrice;
            this.houseBuiltDate = houseBuiltDate;
            this.houseHeating = houseHeating;
            this.houseConditioning = houseConditioning;
            this.houseGarage = houseGarage;
            this.houseVisitCount = houseVisitCount;
            this.houseMainImg = houseMainImg;
            this.houseType = houseType;
            this.vendorId = vendorId;
            this.houseImg1 = houseImg1;
            this.houseImg2 = houseImg2;
            this.houseImg3 = houseImg3;
            this.houseImg4 = houseImg4;

        }
    }
}