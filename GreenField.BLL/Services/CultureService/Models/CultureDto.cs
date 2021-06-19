using System;
using System.Collections.Generic;
using GreenField.Common.Enums;
using GreenField.DAL.ValueObjects;

namespace GreenField.BLL.Services.CultureService.Models
{
    public class CultureDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        public Month MonthToSeed { get; set; }

        public int TermInMonths { get; set; }
        
        public List<Guid> FriendlyCultures { get; set; }
        public List<string> FriendlyCulturesNames { get; set; }
        
        public List<Guid> EnemyCultures { get; set; }
        public List<string> EnemyCulturesNames { get; set; }
        
        public List<ComponentWithDose> ConsumeComponents { get; set; }
        
        public List<ComponentWithDose> ProduceComponents { get; set; }
        
        public List<Guid> ForbiddenComponents { get; set; }
    }
}