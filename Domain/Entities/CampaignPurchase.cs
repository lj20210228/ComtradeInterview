using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CampaignPurchase
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public decimal Amount { get; set; }
    }
}
