

using Microsoft.EntityFrameworkCore;

namespace AdminApp.Models.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly AdminAppContext _appContext;
        public FacturaRepository(AdminAppContext adminAppContext) 
        { 
            _appContext = adminAppContext;
        }
        public async Task<int> AddAsync(Factura factura)
        {
            await _appContext.AddAsync<Factura>(factura);
            return _appContext.SaveChanges();
        }

        public async Task<int> DeleteByIdAsync(int id)
        {
            _appContext.factura.RemoveRange(_appContext.factura.Where(f => f.id == id));
            return await _appContext.SaveChangesAsync();
        }

        public async Task<int> DeleteByPersonaAsync(int idPersona)
        {
            _appContext.factura.RemoveRange(_appContext.factura.Where(f => f.idpersona == idPersona));
            return await _appContext.SaveChangesAsync();
        }

        public IEnumerable<Factura> GetByPersona(int idPersona)
        {
            return _appContext.factura.Where(f => f.idpersona == idPersona).ToList();            
        }

        public Factura GetFacturaById(int id)
        {
            return _appContext.factura.First(f => f.id == id);
        }

        public async Task<int> UpdateAsync(Factura factura)
        {
            _appContext.Entry(factura).State = EntityState.Modified;

            return await _appContext.SaveChangesAsync();
        }
    }
}
