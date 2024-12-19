using AdminApp.Models;
using AdminApp.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorioController : ControllerBase
    {
        private readonly IDirectorioService _directorioService;
        public  DirectorioController(IDirectorioService directorioService) 
        { 
            _directorioService = directorioService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Persona>> GetPersonas()
        {
            return Ok(_directorioService.GetAll());
        }
        [HttpPut]
        public async Task<ActionResult<Persona>> PutPersona(Persona persona)
        {
            return Ok(await _directorioService.CreateAsync(persona));
        }
        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona(Persona persona)
        {
            return Ok(await _directorioService.SetAsync(persona));
        }
        [HttpDelete]
        public async Task<ActionResult<Persona>> DeletePersona(Persona persona)
        {
            return Ok(await _directorioService.RemovePersonaAndFacturaAsync(persona));
        }
        
        [HttpGet("{identificacion}")]
        [ActionName(nameof(GetPersona))]
        public ActionResult<Persona> GetPersona(string identificacion)
        {
            return Ok(_directorioService.GetByIdentificacion(identificacion));
        }
    }
}
