using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmlakProject.Models
{
    public class ImageModel
    {
        public int imageId { get; set; }
        public string imagePath { get; set; }
        public int houseId { get; set; }

        public ImageModel()
        {

        }
        public ImageModel(int imageId, string imagePath, int houseId)
        {
            this.imageId = imageId;
            this.imagePath = imagePath;
            this.houseId = houseId;
        }
    }
}