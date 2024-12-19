namespace AdminApp.Models.Services
{
    public interface IDirectorioService
    {
        IEnumerable<Persona> GetAll();
        Persona GetById(int id);
        Persona GetByIdentificacion(string identificacion);
        Task<Persona> RemovePersonaAndFacturaAsync(Persona persona);
        Task<Persona> CreateAsync(Persona persona);
        Task<Persona> SetAsync(Persona persona);
    }
}
