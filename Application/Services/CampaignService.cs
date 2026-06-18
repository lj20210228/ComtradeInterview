using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Application.Services
{
    public class CampaignService(ICampaignRepository repository, ICampaignValidationService soap) : ICampaignService
    {
        public async Task<List<MonthlyReportDto>> GetCampaignResultAsync()
        {
            return await repository.GetCampaigntResultsDataAsync();
        }

        public async Task<string> NominateCustomerAsync(int agentId, NominateDto dto)
        {
            int countToday = await repository.GetAgentNominationCountForTodayAsync(agentId);
            if (countToday >= 5)
            {
                throw new CampaignLimitException("Ispunili ste maksimalni dnevni limit od 5 nominacija.");
            }
            var result = await soap.ValidateTargetAsync(dto.Id);
            if (string.IsNullOrEmpty(result.Name))
            {
                throw new NotFoundException($"Korisnik sa ID-jem '{dto.Id}' ne postoji na eksternom SOAP sistemu.");
            }
            var nomination = new Nomination
            {
                AgentId = agentId,
                CustomerId = dto.Id,
                CustomerName = result.Name,
                NominatedAt = DateTime.UtcNow
            };

            await repository.AddNominationAsync(nomination);

            return $"Kupac {result.Name} je uspešno nominovan!";
        }

        public async Task<string> UploadPurchasesCsvAsync(Stream fileStream)
        {
            if (fileStream == null || fileStream.Length == 0)
            {
                throw new ArgumentException("Fajl je prazan ili nije uspešno prosleđen.");
            }

            var purchases = new List<CampaignPurchase>();
            using var reader = new StreamReader(fileStream);

            bool isHeader = true;
            int lineCount = 0;

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (isHeader)
                {
                    isHeader = false;
                    if (line.Contains("CustomerId", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                }

                var parts = line.Split(',');
                if (parts.Length < 3) continue;

                try
                {
                    var purchase = new CampaignPurchase
                    {
                        CustomerId = parts[0].Trim().ToUpper(),
                        Amount = decimal.Parse(parts[1].Trim(), CultureInfo.InvariantCulture),
                        PurchaseDate = DateTime.Parse(parts[2].Trim(), CultureInfo.InvariantCulture)
                    };

                    purchases.Add(purchase);
                    lineCount++;
                }
                catch (Exception)
                {
                    throw new FormatException($"Greška na biznis nivou: Red {lineCount + 1} u CSV-u nije u ispravnom formatu (CustomerId, Amount, PurchaseDate).");
                }
            }

            if (purchases.Count == 0)
            {
                throw new InvalidOperationException("CSV fajl ne sadrži nijedan validan red za uvoz.");
            }

            await repository.AddPurchasesAsync(purchases);

            return $"Uspešno uvezeno {purchases.Count} zapisa o kupovinama!";
        }
    }

}
