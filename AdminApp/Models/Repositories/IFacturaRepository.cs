namespace AdminApp.Models.Repositories
{
    public interface IFacturaRepository
    {
        Factura GetFacturaById(int id);
        IEnumerable<Factura> GetByPersona(int idPersona);
        Task<int> DeleteByIdAsync(int id);
        Task<int> DeleteByPersonaAsync(int idPersona);
        Task<int> AddAsync(Factura factura);
        Task<int> UpdateAsync(Factura factura);
    }
}
