//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MoneySave
{
    using System;
    using System.Collections.Generic;
    
    public partial class Gasto
    {
        public int IdGasto { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<int> IdTipoGasto { get; set; }
        public string Descripcion { get; set; }
        public Nullable<decimal> Monto { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<int> IdCuenta { get; set; }
    
        public virtual Cuenta Cuenta { get; set; }
        public virtual Tipogasto Tipogasto { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
