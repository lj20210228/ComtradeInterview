using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ICampaignValidationService
    {
        Task<ValidationResultDto?> ValidateTargetAsync(string identifier);
    }
}
