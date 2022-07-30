using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneySave.Models
{
    internal class Ingreso
    {
        public int IdIngreso { get; set; }
        public string Cuenta { get; set; }
        public string TipoIngreso { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }                    
        public int IdUsuario { get; set; }
        
    }
}
