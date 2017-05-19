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

    public partial class PUESTOS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PUESTOS()
        {
            this.ASCENSOS = new HashSet<ASCENSOS>();
            this.EMPLEADOS = new HashSet<EMPLEADOS>();
        }

        public int PTS_ID { get; set; }
        public string ID_PUESTO { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, ingrese un nombre"), DisplayName("Nombre del Puesto"), StringLength(45, ErrorMessage = " El nombre del puesto es muy extenso. Por favor no exceda los 45 caracteres."), RegularExpression("([A-Za-z])+( [A-Za-z]+)*", ErrorMessage = "Formato Inv�lido.")]
        public string NOMBRE { get; set; }
        public int DEPARTAMENTO { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, ingrese el nivel academico requerido para el puesto"), DisplayName("Nivel Acad�mico"), StringLength(45, ErrorMessage = "Por favor no exceda los 45 caracteres."), RegularExpression("([A-Za-z])+( [A-Za-z]+)*", ErrorMessage = "Formato Inv�lido.")]
        public string NIVEL_ACADEMICO { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor ingrese la experiencia m�nima"), DisplayName("Experiencia M�nima (A�os)"), StringLength(2, ErrorMessage = "Muchos A�os."), RegularExpression("([1-9][0-9]*)", ErrorMessage = "Formato Inv�lido (Ingrese n�meros.)")]
        public string EXP_MIN { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor ingrese la experiencia deseada"), DisplayName("Experiencia Deseada (A�os)"), StringLength(2, ErrorMessage = "Muchos A�os."), RegularExpression("([1-9][0-9]*)", ErrorMessage = "Formato Inv�lido (Ingrese n�meros.)")]
        public string EXP_DESEADA { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, ingrese una descripci�n"), DisplayName("Descripci�n del Puesto"), DataType(DataType.MultilineText)]
        public string DESCRIPCION { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, seleccione el estado del puesto"), DisplayName("Estado")]
        public string ESTADO { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ASCENSOS> ASCENSOS { get; set; }
        public virtual DEPARTAMENTOS DEPARTAMENTOS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMPLEADOS> EMPLEADOS { get; set; }
    }
}
