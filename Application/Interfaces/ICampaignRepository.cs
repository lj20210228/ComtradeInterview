using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ICampaignRepository
    {
        Task<int> GetAgentNominationCountForTodayAsync(int agentId);
        Task AddNominationAsync(Nomination nomination);
        Task AddPurchasesAsync(IEnumerable<CampaignPurchase> purchases);
        Task<List<Nomination>> GetAllNominationAsync();
        Task<List<CampaignPurchase>> GetAllPurchasesAsync();
        Task<Agent?> GetAgentByEmailAsync(string email);
        Task<List<MonthlyReportDto>> GetCampaigntResultsDataAsync();
    }
}
