﻿using System;

namespace GreenField.BLL.Dto
{
    public class CropDto
    {
        public string Id { get; set; }
        public Guid CultureId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int SuccessPercentage { get; set; }
    }
}