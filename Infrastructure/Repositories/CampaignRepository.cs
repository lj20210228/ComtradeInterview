using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class CampaignRepository(ApplicationDBContext context) : ICampaignRepository
    {
        public async Task AddNominationAsync(Nomination nomination)
        {
            await context.Nominations.AddAsync(nomination);
            await context.SaveChangesAsync();
        }

        public async Task AddPurchasesAsync(IEnumerable<CampaignPurchase> purchases)
        {
            await context.CampaignPurchases.AddRangeAsync(purchases);
            await context.SaveChangesAsync();
        }

        public async Task<Agent?> GetAgentByEmailAsync(string email)
        {
            return await context.Agents.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<List<Nomination>> GetAgentNominationsForTodayAsync(int agentId)
        {
            var today = DateTime.UtcNow.Date;
            return await context.Nominations.Where(n => n.AgentId == agentId && n.NominatedAt.Date == today).ToListAsync();
        }

        public async Task<List<Nomination>> GetAllNominationAsync()
        {
            return await context.Nominations.Include(n => n.Agent).ToListAsync();
        }

        public async Task<List<CampaignPurchase>> GetAllPurchasesAsync()
        {
            return await context.CampaignPurchases.ToListAsync();
        }

        public async Task<List<MonthlyReportDto>> GetCampaigntResultsDataAsync()
        {
            var nominations = await context.Nominations
                .Include(n => n.Agent)
                .ToListAsync();

            var purchases = await context.CampaignPurchases.ToListAsync();

            var report = new List<MonthlyReportDto>();

            foreach (var nomination in nominations)
            {
                var hasPurchased = purchases.Any(p => p.CustomerId == nomination.CustomerId);

                var totalAmount = purchases
                    .Where(p => p.CustomerId == nomination.CustomerId)
                    .Sum(p => p.Amount);

                report.Add(new MonthlyReportDto
                {
                    AgentId = nomination.AgentId,
                    AgentName = nomination.Agent?.Name ?? "Nepoznat Agent",
                    Id = nomination.CustomerId,       
                    Name = nomination.CustomerName,   
                    HasPurchased = hasPurchased,
                    TotalAmount = totalAmount
                });
            }

            return report;
        }
    }
}
