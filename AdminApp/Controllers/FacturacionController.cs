using AdminApp.Models;
using AdminApp.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturacionController : ControllerBase
    {
        private readonly IFacturaService _facturaService;
        public FacturacionController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Factura>> GetFacturasByPersona(int idPersona)
        {
            return Ok(_facturaService.GetFacturasPersona(idPersona));
        }
        [HttpPut]
        public async Task<ActionResult<Factura>> PutFactura(Factura factura)
        {
            return Ok(await _facturaService.CreateAsync(factura));
        }
    }
}
