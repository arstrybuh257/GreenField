using System;
using System.Collections.Generic;
using GreenField.BLL.Dto;
using GreenField.BLL.Types;
using GreenField.DAL.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace GreenField.Api.Models.Field
{
    public class CreateFieldRequest
    {
        public string Address { get; set; }
        public double Area { get; set; }
        public FieldCoordinates Coordinates { get; set; }
        public List<CropDto> Crops { get; set; }
        public CropDto CurrentCrop { get; set; }
        public Guid OrganisationId { get; set; }
    }
}