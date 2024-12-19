using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndWPF.Models
{
    internal class Factura
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public decimal monto { get; set; }
        public int idpersona { get; set; }
    }
}
