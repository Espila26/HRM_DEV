//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
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
