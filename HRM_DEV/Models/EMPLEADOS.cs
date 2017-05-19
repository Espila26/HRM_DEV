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

    public partial class EMPLEADOS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPLEADOS()
        {
            this.AMONESTACIONES = new HashSet<AMONESTACIONES>();
            this.ASCENSOS = new HashSet<ASCENSOS>();
            this.PERMISOS = new HashSet<PERMISOS>();
            this.SUSPENSIONES = new HashSet<SUSPENSIONES>();
            this.VACACIONES = new HashSet<VACACIONES>();
        }

        public int EMP_ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor ingrese el N�mero de empleado"), DisplayName("N�mero de Empleado"), StringLength(9, ErrorMessage = "La longitud de la c�dula es muy extensa, no exceda los 9 caracteres."), RegularExpression("([1-9][0-9]*)", ErrorMessage = "Formato Inv�lido (Ingrese n�meros.)")]
        public string ID_EMPLEADO { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor ingrese la c�dula del empleado"), DisplayName("C�dula"), StringLength(9, ErrorMessage = "La longitud de la c�dula es muy extensa, no exceda los 9 caracteres."), RegularExpression("([1-9][0-9]*)", ErrorMessage = "Formato Inv�lido (Ingrese n�meros.)")]
        public string CEDULA { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, ingrese un nombre"), DisplayName("Nombre"), StringLength(30, ErrorMessage = " El nombre del empleado es muy extenso. Por favor no exceda los 30 caracteres."), RegularExpression("([A-Za-z])+( [A-Za-z]+)*", ErrorMessage = "Formato Inv�lido.")]
        public string NOMBRE { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, ingrese el primer apellido"), DisplayName("Primer Apellido"), StringLength(15, ErrorMessage = " El apellido del empleado es muy extenso. Por favor no exceda los 15 caracteres."), RegularExpression("([A-Za-z])+( [A-Za-z]+)*", ErrorMessage = "Formato Inv�lido.")]
        public string APE1 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, ingrese el segundo apelliod"), DisplayName("Segundo Apellido"), StringLength(15, ErrorMessage = " El apellido del empleado es muy extenso. Por favor no exceda los 15 caracteres."), RegularExpression("([A-Za-z])+( [A-Za-z]+)*", ErrorMessage = "Formato Inv�lido.")]
        public string APE2 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, ingrese la fecha de nacimiento"), DisplayName("Fecha de Nacimiento"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime FECHA_NAC { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, ingrese la fecha de contrataci�n"), DisplayName("Fecha de Contrataci�n"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime FECHA_CONTR { get; set; }

        public int DIAS_VAC_DISP { get; set; }
        public int DIAS_VAC_UTILIZAD { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, ingrese la direcci�n del empleado"), DisplayName("Direcci�n"), DataType(DataType.MultilineText)]
        public string DIRECCION { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, ingrese una Descripci�n"), DisplayName("Descripci�n"), DataType(DataType.MultilineText)]
        public string DESCRIPCION { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor ingrese el tel�fono de habitaci�n del empleado"), DisplayName("Tel�fono de Habitaci�n"), StringLength(8, ErrorMessage = "El n�mero es muy extenso, no exceda los 8 caracteres."), RegularExpression("([1-9][0-9]*)", ErrorMessage = "Formato Inv�lido (Ingrese n�meros.)")]
        public string TEL_HABITACION { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor ingrese el tel�fono m�vil del empleado"), DisplayName("Tel�fono M�vil"), StringLength(8, ErrorMessage = "El n�mero es muy extenso, no exceda los 8 caracteres."), RegularExpression("([1-9][0-9]*)", ErrorMessage = "Formato Inv�lido (Ingrese n�meros.)")]
        public string TEL_MOVIL { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor ingrese un correo electr�nico v�lido"), DisplayName("Correo Electr�nico "), EmailAddress(ErrorMessage = "Correo El�ctronico con formato inv�lido")]
        public string E_MAIL { get; set; }

        public int PUESTO { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, ingrese la direcci�n del empleado"), DisplayName("Salario")]
        public double SALARIO { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor, seleccione el estado del empleado"), DisplayName("Estado")]
        public string ESTADO { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AMONESTACIONES> AMONESTACIONES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ASCENSOS> ASCENSOS { get; set; }
        public virtual PUESTOS PUESTOS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PERMISOS> PERMISOS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUSPENSIONES> SUSPENSIONES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VACACIONES> VACACIONES { get; set; }
    }
}
