using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace AdminApp.Models
{
    public class Persona
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string apellido_paterno { get; set; } = string.Empty;
        public string? apellido_materno { get; set; } 
        public string identificacion { get; set; } = string.Empty;
    }
}