using System;
using System.Collections.Generic;

namespace GreenField.BLL.Dto
{
    public class CultureDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public int TermInMonths { get; set; }

        public List<Guid> EnemyCultures { get; set; }

        public List<Guid> FriendCultures { get; set; } 
    }
}