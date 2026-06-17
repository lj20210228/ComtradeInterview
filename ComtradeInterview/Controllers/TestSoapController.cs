using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestSoapController(ISoapCustomerService soapService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> TestSoap(string id)
        {
            var imeKorisnika = await soapService.GetCustomerNameByIdAsync(id);

            if (imeKorisnika == null)
            {
                return NotFound(new { Message = $"Korisnik sa ID-jem {id} nije pronađen na SOAP servisu." });
            }

            return Ok(new { Id = id, ImeIzXmla = imeKorisnika });
        }
    }
}
