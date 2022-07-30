using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneySave.Models
{
    internal class Usuario
    {
        public int IdUsuario { get; set; }
        public string Name { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string User { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int Telefono { get; set; }

    }
}
