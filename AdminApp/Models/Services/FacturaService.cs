using AdminApp.Models.Repositories;

namespace AdminApp.Models.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _facturaRepository;
        public FacturaService(IFacturaRepository facturaRepository) 
        {
            _facturaRepository = facturaRepository;
        }
        public async Task<Factura> CreateAsync(Factura factura)
        {
            int resultAdd = await _facturaRepository.AddAsync(factura);
            if (resultAdd > 0)
            {
                return factura;
            }

            factura.id = 0;
            return factura;
        }

        public Factura GetFacturaById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Factura> GetFacturasPersona(int idPersona)
        {
            return _facturaRepository.GetByPersona(idPersona);
        }

        public Task<Factura> RemoveByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Factura> SetAsync(Factura factura)
        {
            int updResult = await _facturaRepository.UpdateAsync(factura);
            if (updResult > 0)
            {
                return factura;
            }
            factura.id = 0;
            return factura;
        }
    }
}
