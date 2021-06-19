using System;
using System.Collections.Generic;
using GreenField.Common.Enums;
using GreenField.DAL.ValueObjects;

namespace GreenField.Api.Models.Culture
{
    public class UpdateCultureRequest
    {
        public string Name { get; set; }
        public Month MonthToSeed { get; set; }

        public int TermInMonths { get; set; }

        public List<ComponentWithDose> ConsumeComponents { get; set; }
        
        public List<ComponentWithDose> ProduceComponents { get; set; }

        public List<Guid> ForbidenComponents { get; set; } 
        public List<Guid> FriendlyCultures { get; set; }
        
        public List<Guid> EnemyCultures { get; set; }
    }
}