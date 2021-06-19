using System;

namespace GreenField.IoT.Models
{
    public class Field
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public int Area { get; set; }
        public Guid OrganizationId { get; set; }
    }
}