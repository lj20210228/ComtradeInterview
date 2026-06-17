using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Nomination
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public string  CustomerName  { get; set; }
        public DateTime NominatedAt { get; set; }
        public Agent Agent { get; set; } = null!;
    }
}
