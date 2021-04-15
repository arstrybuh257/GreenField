using System;
using System.Collections.Generic;

namespace GreenField.Api.Models.Culture
{
    public class CreateCultureRequest
    {

        public string Name { get; set; }

        public int TermInMonths { get; set; }

        public List<Guid> EnemyCultures { get; set; }

        public List<Guid> FriendCultures { get; set; } 
    }
}