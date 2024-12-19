namespace AdminApp.Models.Repositories
{
    public interface IPersonaRepository
    {
        IEnumerable<Persona> GetAll();
        Persona FindById(int id);
        Persona FindByIdentificacion(string identificacion);
        Task<int> DeleteByIdAsync(int id);
        Task<Persona> AddAsync(Persona persona);
        Task<int> UpdateAsync(Persona persona);
        Task<int> DeleteByIdentificacionAsync(string identificacion);
    }
}
