using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Dto.Employee.CustomAttribute
{
    public class ValidateDateAttribute : ValidationAttribute
    {
        private readonly string _errorMessageResourceKey;

        public ValidateDateAttribute(string errorMessageResourceKey)
        {
            _errorMessageResourceKey = errorMessageResourceKey;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            
            if (value != null && value.ToString().Trim() != "" && value is DateTime dateValue )
            {
                if (dateValue > DateTime.Now)
                {
                    string? errorMessage = Resources.Employee.EmployeeVN.ResourceManager.GetString(_errorMessageResourceKey);
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}
