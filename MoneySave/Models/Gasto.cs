using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneySave.Models
{
    internal class Gasto
    {
        public int IdGasto { get; set; }
        public string Cuenta { get; set; }
        public string TipoGasto { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }      
        public int IdUsuario { get; set; }
        

    }
}
