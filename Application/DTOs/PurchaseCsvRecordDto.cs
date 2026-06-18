using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class PurchaseCsvRecordDto
    {
        public string CustomerId { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
