using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ICampaignService
    {
        Task<string> NominateCustomerAsync(int agentId, NominateCustomerDto dto);
        Task<string> UploadPurchasesCsvAsync(Stream fileStream);
        Task<List<MonthlyReportDto>> GetCampaignResultAsync();
    }
}
