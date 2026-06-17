using Application.Interfaces;
using SOAPDemo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.SoapServices
{
    public class SoapCustomerService : ISoapCustomerService
    {
        public async Task<string?> GetCustomerNameByIdAsync(string customerId)
        {
            using var client = new SOAPDemoSoapClient(SOAPDemoSoapClient.EndpointConfiguration.SOAPDemoSoap);
            try
            {
                var response = await client.FindPersonAsync(customerId);
                if (response == null || string.IsNullOrEmpty(response.Name))
                {
                    return null;
                }
                return response.Name;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }
    }
}
