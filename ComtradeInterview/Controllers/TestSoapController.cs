using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestSoapController(ICampaignValidationService soapService) : ControllerBase
    {
        [HttpGet("client/{id}")]
        public async Task<IActionResult> TestSoap(string id)
        {
            var imeKorisnika = await soapService.ValidateTargetAsync(id);

            if (imeKorisnika == null)
            {
                return NotFound(new { Message = $"Korisnik sa ID-jem {id} nije pronađen na SOAP servisu." });
            }

            return Ok(new { Id = id, ImeIzXmla = imeKorisnika });
        }
        [HttpGet("country/{code}")]
        public async Task<IActionResult> TestCounry(string code)
        {
            var imeDrzave = await soapService.ValidateTargetAsync(code);

            if (imeDrzave == null)
            {
                return NotFound(new { Message = $"Drzava sa kodom {code} nije pronađena na SOAP servisu." });
            }

            return Ok(new { Code = code, Ime = imeDrzave });
        }
    }
}
