using Application.DTOs;
using Application.Interfaces;
using CountryServiceReference;
using SOAPDemo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.SoapServices
{
    public class CountryInfoService : ICampaignValidationService
    {
       
        public async Task<ValidationResultDto?> ValidateTargetAsync(string countryCode)
        {
            using var client = new CountryInfoServiceSoapTypeClient(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);
            var name = await client.CountryNameAsync(countryCode.Trim().ToUpper());

            if (string.IsNullOrEmpty(name.Body.CountryNameResult) || name.Body.CountryNameResult
                .Contains("not found", StringComparison.OrdinalIgnoreCase))
                return null;

            return new ValidationResultDto { Identifier = countryCode.ToUpper(), Name = name.Body.CountryNameResult };
        }
    }
}
