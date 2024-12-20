
using AdminApp.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AdminApp.Models.Services
{
    public class DirectorioService : IDirectorioService
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IFacturaRepository _facturaRepository;
        public DirectorioService(IPersonaRepository personaRepository, IFacturaRepository facturaRepository) 
        { 
            _personaRepository = personaRepository;
            _facturaRepository = facturaRepository;
        }
        public async Task<Persona> CreateAsync(Persona persona)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(persona.nombre))
                {
                    persona.id = 0;
                    return persona;
                }

                if (string.IsNullOrWhiteSpace(persona.nombre))
                {
                    persona.id = 0;
                    return persona;
                }

                if (string.IsNullOrWhiteSpace(persona.apellido_paterno))
                {
                    persona.id = 0;
                    return persona;
                }

                if (string.IsNullOrWhiteSpace(persona.identificacion))
                {
                    persona.id = 0;
                    return persona;
                }

                return await _personaRepository.AddAsync(persona);
            }
            catch (DbUpdateException dbException)
            {
                Console.WriteLine(dbException.Message);

                return persona;
            }
            catch (OperationCanceledException operationException)
            {
                Console.WriteLine(operationException.Message);

                return persona;
            }
            catch (Exception exeption)
            {
                Console.WriteLine(exeption.Message);

                return persona;
            }
        }

        public IEnumerable<Persona> GetAll()
        {
            return _personaRepository.GetAll();
        }

        public Persona GetById(int id)
        {
            return _personaRepository.FindById(id);
        }

        public Persona GetByIdentificacion(string identificacion)
        {
            return _personaRepository.FindByIdentificacion(identificacion);
        }

        public async Task<bool> RemovePersonaAndFacturaAsync(string identificacion)
        {
            try
            {
                int actionRemove = await _personaRepository.DeleteByIdentificacionAsync(identificacion);
                if (actionRemove > 0)
                {
                    return true;
                }
                return false;
            }
            catch (DbUpdateException dbException)
            {
                Console.WriteLine(dbException.Message);

                return false;
            }
            catch (OperationCanceledException operationException)
            {
                Console.WriteLine(operationException.Message);

                return false;
            }
            catch (Exception exeption)
            {
                Console.WriteLine(exeption.Message);

                return false;
            }
        }

        public async Task<Persona> SetAsync(Persona persona)
        {
            try
            {
                if (persona.id == 0)
                {
                    return persona;
                }
                if (string.IsNullOrWhiteSpace(persona.nombre))
                {
                    persona.id = 0;
                }

                if (string.IsNullOrWhiteSpace(persona.nombre))
                {
                    persona.id = 0;
                }

                if (string.IsNullOrWhiteSpace(persona.apellido_paterno))
                {
                    persona.id = 0;
                }

                if (string.IsNullOrWhiteSpace(persona.identificacion))
                {
                    persona.id = 0;
                }

                int resultUpdate = await _personaRepository.UpdateAsync(persona);
                if (resultUpdate > 0)
                {
                    return persona;
                }
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
            return persona;
        }
    }
}
