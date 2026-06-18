using Application.Interfaces;
using CountryServiceReference;
using SOAPDemo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.SoapServices
{
    public class CountryInfoService : ICountryInfoService
    {
        public async Task<string?> GetCountryNameBy(string countryCode)
        {
            using var countryClient = new CountryInfoServiceSoapTypeClient(
                 CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap
            );
            try
            {
                var countryName = await countryClient.CountryNameAsync(countryCode.Trim().ToUpper());
                if (string.IsNullOrEmpty(countryName.Body.CountryNameResult))
                {
                    return null;
                }
                
                Console.WriteLine($"Pronađena država: {countryName}");
                return countryName.Body.CountryNameResult;

            }
            catch(Exception ex)
            {
                Console.Write($"Greska pri komunikaciji sa servisom:{ex.Message}");
                return null;
            }

        }
    }
}
