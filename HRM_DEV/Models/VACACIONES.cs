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

    public partial class VACACIONES
    {
        public int ID_SOLICITUD { get; set; }

        public int ID_EMPLEADO { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, ingrese la fecha de inicio"), DisplayName("Fecha de inicio"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime INICIO { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, ingrese la fecha final"), DisplayName("Fecha final"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), FechaMayor("INICIO")]
        public System.DateTime FINAL { get; set; }
        public System.DateTime FECHA_CREACION { get; set; }

        [DisplayName("Cantidad de D�as")]
        public int CANT_DIAS { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, ingrese un nombre"), DisplayName("Autorizado por:"), StringLength(30, ErrorMessage = " El nombre del empleado es muy extenso. Por favor no exceda los 30 caracteres."), RegularExpression("([A-Za-z])+( [A-Za-z]+)*", ErrorMessage = "Formato Inv�lido.")]
        public string AUTORIZACION { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, ingrese una Descripci�n"), DisplayName("Descripci�n"), DataType(DataType.MultilineText)]
        public string DESCRIPCION { get; set; }
        public virtual EMPLEADOS EMPLEADOS { get; set; }

    }
}
