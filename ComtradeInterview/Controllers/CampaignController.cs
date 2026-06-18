using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ServiceModel.Channels;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CampaignController (ICampaignService service): ControllerBase
    {
        [HttpPost("upload-csv")]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            using var stream=file.OpenReadStream   ();
            var resultMessage = await service.UploadPurchasesCsvAsync(stream);
            return Ok(new { Message = resultMessage });
        }
        [HttpGet("results")]
        public async Task<IActionResult> GetResults()
        {
            var results = await service.GetCampaignResultAsync();
            if (results == null||results.Count == 0)
            {
                return Ok(new { Message = "Trenutno nema podataka" });

            }
            return Ok(results);
        }
    }
}
