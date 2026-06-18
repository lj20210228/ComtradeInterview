using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class MonthlyReportDto
    {
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public string Id { get; set; }
        public string Name  { get; set; }
        public bool HasPurchased { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
