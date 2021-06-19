using System;
using GreenField.Common.Enums;

namespace GreenField.BLL.Services.PesticideService.Models
{
    public class BrowsePesticides
    {
        public string Name { get; set; }
        public PesticideType PesticideType { get; set; }
        public Guid Component { get; set; }
    }
}