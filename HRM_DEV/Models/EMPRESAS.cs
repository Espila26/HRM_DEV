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
    
    public partial class EMPRESAS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPRESAS()
        {
            this.DEPARTAMENTOS = new HashSet<DEPARTAMENTOS>();
        }
    
        public int ID_EMPRESA { get; set; }
        public string NOMBRE { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public string CEDULA_JURIDICA { get; set; }
        public System.DateTime FECHA_FUNDACION { get; set; }
        public string PAIS_ORIGEN { get; set; }
        public string SEDE_CENTRAL { get; set; }
        public string ESTADO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEPARTAMENTOS> DEPARTAMENTOS { get; set; }
    }
}
