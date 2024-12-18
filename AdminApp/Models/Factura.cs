using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace AdminApp.Models
{
    public class Factura
    {
        [Key]
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public decimal monto { get; set; }
        [ForeignKey("idpersona")]
        public int idpersona {  get; set; }
    }
}