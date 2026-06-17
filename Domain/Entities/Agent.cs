using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Agent
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public ICollection<Nomination> Nominations { get; set; } = new List<Nomination>();
    }
}
