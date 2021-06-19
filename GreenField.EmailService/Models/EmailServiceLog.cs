using System;
using GreenField.DAL.Entities;

namespace GreenField.EmailService.Models
{
    public class EmailServiceLog : BaseEntity
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }

    public static class LogStatus
    {
        public const string Error = "ERROR";
        public const string Info = "INFO";
    }
}