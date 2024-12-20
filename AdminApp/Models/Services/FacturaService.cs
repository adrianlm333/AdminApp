using AdminApp.Models.Repositories;
using Microsoft.EntityFrameworkCore;

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
            try
            {


                int resultAdd = await _facturaRepository.AddAsync(factura);
                if (resultAdd > 0)
                {
                    return factura;
                }

                factura.id = 0;
                return factura;
            }
            catch (DbUpdateException dbException)
            {
                Console.WriteLine(dbException.Message);
            }
            catch (OperationCanceledException operationException)
            {
                Console.WriteLine(operationException.Message);
            }
            catch (Exception exeption)
            {
                Console.WriteLine(exeption.Message);
            }
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
            try
            {
                int updResult = await _facturaRepository.UpdateAsync(factura);
                if (updResult > 0)
                {
                    return factura;
                }
                factura.id = 0;
                return factura;
            }
            catch (DbUpdateException dbException)
            {
                Console.WriteLine(dbException.Message);
            }
            catch (OperationCanceledException operationException)
            {
                Console.WriteLine(operationException.Message);
            }
            catch (Exception exeption)
            {
                Console.WriteLine(exeption.Message);
            }
            return factura;
        }
    }
}
