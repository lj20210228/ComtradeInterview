using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ICampaignService
    {
        Task<string> NominateCustomerAsync(int agentId, NominateCustomerDto dto);
    }
}
