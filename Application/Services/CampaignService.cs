using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class CampaignService(ICampaignRepository repository,ISoapCustomerService soap) : ICampaignService
    {
        public async Task<string> NominateCustomerAsync(int agentId, NominateCustomerDto dto)
        {
            int countToday = await repository.GetAgentNominationCountForTodayAsync(agentId);
            if (countToday >= 5)
            {
                throw new CampaignLimitException("Ispunili ste maksimalni dnevni limit od 5 nominacija.");
            }
            var customerName = await soap.GetCustomerNameByIdAsync(dto.CustomerId);
            if (string.IsNullOrEmpty(customerName))
            {
                throw new CustomerNotFoundException($"Kupac sa ID-jem '{dto.CustomerId}' ne postoji na eksternom SOAP sistemu.");
            }
            var nomination = new Nomination
            {
                AgentId = agentId,
                CustomerId = dto.CustomerId,
                CustomerName = customerName,
                NominatedAt = DateTime.UtcNow
            };

            await repository.AddNominationAsync(nomination);

            return $"Kupac {customerName} je uspešno nominovan!";
        }
    }
}
