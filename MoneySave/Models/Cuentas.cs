using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneySave.Models
{
    internal class Cuentas
    {
        public int IdCuenta { get; set; }
        public string Name { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public DateTime CreatedDate { get; set; }
        public int IdUsuario { get; set; }


    }
}
