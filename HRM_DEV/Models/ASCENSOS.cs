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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class ASCENSOS
    {
        public int ID_ASCENSO { get; set; }
        public int ID_EMPLEADO { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, ingrese una Descripci�n"), DisplayName("Descripci�n"), DataType(DataType.MultilineText)]
        public string DESCRIPCION { get; set; }

        [DisplayName("Puesto Anterior")]
        public string PUESTO_ANT { get; set; }

        public int PUESTO_NVO { get; set; }

        [DisplayName("Fecha")]
        public System.DateTime FECHA { get; set; }
        public System.DateTime FECHA_CREACION { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, ingrese un nombre"), DisplayName("Autorizado por:"), StringLength(30, ErrorMessage = " El nombre del empleado es muy extenso. Por favor no exceda los 30 caracteres."), RegularExpression("([A-Za-z])+( [A-Za-z]+)*", ErrorMessage = "Formato Inv�lido.")]
        public string AUTORIZACION { get; set; }

        public virtual EMPLEADOS EMPLEADOS { get; set; }
        public virtual PUESTOS PUESTOS { get; set; }

    }
}
