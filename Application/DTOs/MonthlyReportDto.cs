using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class MonthlyReportDto
    {
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName  { get; set; }
        public bool HasPurchased { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
