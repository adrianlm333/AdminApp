
using Microsoft.EntityFrameworkCore;

namespace AdminApp.Models.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly AdminAppContext _appContext;
        public PersonaRepository(AdminAppContext adminAppContext) 
        { 
            _appContext = adminAppContext;
        }
        public async Task<Persona> AddAsync(Persona persona)
        {
            await _appContext.AddAsync<Persona>(persona);
            _appContext.SaveChanges();
            return persona;            
        }

        public async Task<int> DeleteByIdAsync(int id)
        {
            _appContext.persona.RemoveRange(_appContext.persona.Where(p => p.id == id));
            return await _appContext.SaveChangesAsync();
        }

        public Persona FindById(int id)
        {
            return _appContext.persona.First(p => p.id == id);
        }

        public Persona FindByIdentificacion(string identificacion)
        {
            return _appContext.persona.First(p => p.identificacion == identificacion);
        }

        public IEnumerable<Persona> GetAll()
        {
            return _appContext.persona.ToList();
        }

        public async Task<int> UpdateAsync(Persona persona)
        {
            _appContext.Entry(persona).State = EntityState.Modified;

            return await _appContext.SaveChangesAsync();
        }
    }
}
