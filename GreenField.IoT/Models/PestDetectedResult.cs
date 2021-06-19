using System;

namespace GreenField.IoT.Models
{
    public class PestDetectedResult
    {
        public Guid PestId { get; set; }
        public int CountOnSquareMeter { get; set; }
    }
}