using Application.DTOs;
using Application.Interfaces;
using SOAPDemo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.SoapServices
{
    public class SoapCustomerService : ICampaignValidationService
    {
       

        public async Task<ValidationResultDto?> ValidateTargetAsync(string identifier)
        {
            using var client = new SOAPDemoSoapClient(SOAPDemoSoapClient.EndpointConfiguration.SOAPDemoSoap);
            try
            {
                var response = await client.FindPersonAsync(identifier);
                if (response == null || string.IsNullOrEmpty(response.Name))
                {
                    return null;
                }
                return new ValidationResultDto
                {
                    Identifier=identifier,
                   Name= response.Name
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
