using System;
using System.Collections.Generic;

namespace GreenField.DAL.Entities
{
    public class Culture : BaseEntity
    {
        public string Name { get; set; }

        public int TermInMonths { get; set; }

        public List<Guid> EnemyCultures { get; set; } = new List<Guid>();

        public List<Guid> FriendCultures { get; set; } = new List<Guid>();
    }
}