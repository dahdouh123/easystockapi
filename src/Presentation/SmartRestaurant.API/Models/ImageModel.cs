﻿namespace SmartRestaurant.API.Models
{
    public class ImageModel
    {
        public byte[] ImageBytes { get; set; }
        public string ImageTitle { get; set; }
        public bool IsLogo { get; set; }
    }
}