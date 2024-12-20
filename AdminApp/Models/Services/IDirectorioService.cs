namespace AdminApp.Models.Services
{
    public interface IDirectorioService
    {
        IEnumerable<Persona> GetAll();
        Persona GetById(int id);
        IEnumerable<Persona> GetByIdentificacion(string identificacion);
        Task<bool> RemovePersonaAndFacturaAsync(string id);
        Task<Persona> CreateAsync(Persona persona);
        Task<Persona> SetAsync(Persona persona);
    }
}
