using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndWPF.Models
{
    internal class Persona
    {
        public int id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string apellido_paterno { get; set; } = string.Empty;
        public string? apellido_materno { get; set; }
        public string identificacion { get; set; } = string.Empty;
    }
}
