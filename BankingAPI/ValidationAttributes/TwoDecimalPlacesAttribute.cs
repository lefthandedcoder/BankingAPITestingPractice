using System.ComponentModel.DataAnnotations;

public class TwoDecimalPlacesAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is decimal)
        {
            decimal decimalValue = (decimal)value;
            string[] parts = decimalValue.ToString().Split('.');

            if (parts.Length == 1 || (parts.Length == 2 && parts[1].Length <= 2))
            {
                return ValidationResult.Success;
            }
        }

        return new ValidationResult("Amount must have a maximum of two decimal places.");
    }
}