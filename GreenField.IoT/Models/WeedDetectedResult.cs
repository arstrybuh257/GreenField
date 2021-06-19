using System;

namespace GreenField.IoT.Models
{
    public class WeedDetectedResult
    {
        public Guid WeedId { get; set; }
        public int Percentage { get; set; }
    }
}