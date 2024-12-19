namespace AdminApp.Models.Services
{
    public interface IFacturaService
    {
        Factura GetFacturaById(int id);
        IEnumerable<Factura> GetFacturasPersona(int idPersona);
        Task<Factura> RemoveByIdAsync(int id);
        Task<Factura> CreateAsync(Factura factura);
        Task<Factura> SetAsync(Factura factura);
    }
}
