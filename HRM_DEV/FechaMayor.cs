using System;
using System.ComponentModel.DataAnnotations;


namespace HRM_DEV
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FechaMayor : ValidationAttribute
    {
        public FechaMayor(string dateToCompareToFieldName)
        {
            DateToCompareToFieldName = dateToCompareToFieldName;
        }

        public string DateToCompareToFieldName { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime FechaFinal;
            if (value != null)
            {
                FechaFinal = (DateTime)value;
                DateTime FechaInicial = (DateTime)validationContext.ObjectType.GetProperty(DateToCompareToFieldName).GetValue(validationContext.ObjectInstance, null);


                if (FechaInicial < FechaFinal)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("La Fecha Final debe ser posterior a la Inicial.");
                }
            }
            else
            {
                return new ValidationResult("Formato Invalido");
            }
        }
    }


}