using System.Collections.Generic;
using DuckLab.Common.ErrorHandling;

namespace GreenField.Api.Models.ErrorHandling
{
    public class ApiErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}