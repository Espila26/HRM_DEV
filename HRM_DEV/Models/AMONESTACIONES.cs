//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRM_DEV.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AMONESTACIONES
    {
        public int ID_AMONESTACION { get; set; }
        public int ID_EMPLEADO { get; set; }
        public System.DateTime FECHA_INICIO { get; set; }
        public System.DateTime FECHA_FINAL { get; set; }
        public System.DateTime FECHA_CREACION { get; set; }
        public string DESCRIPCION { get; set; }
        public string GOCE_SALARIO { get; set; }
        public string VERB_ESC { get; set; }
        public string AUTORIZACION { get; set; }
    
        public virtual EMPLEADOS EMPLEADOS { get; set; }
    }
}
