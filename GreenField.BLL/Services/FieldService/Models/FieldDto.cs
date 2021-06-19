using System;
using System.Collections.Generic;
using GreenField.BLL.Types;
using GreenField.Common.Enums;
using GreenField.DAL.ValueObjects;

namespace GreenField.BLL.Services.FieldService.Models
{
    public class FieldDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        
        public FieldCoordinates Coordinates { get; set; }
        
        public double Area { get; set; }
        
        public List<CropDto> Crops { get; set; }
        
        public CropDto CurrentCrop { get; set; }
        
        public FieldStatus Status { get; set; }
        
        public Guid OrganisationId { get; set; }
        public string ImagePath { get; set; }
    }
}