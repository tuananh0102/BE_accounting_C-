using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Dto.Employee.CustomAttribute
{
    public class ValidationEmailAttribute : ValidationAttribute
    {
        private readonly string _errorMessageResourceKey;

        public ValidationEmailAttribute(string errorMessageResourceKey)
        {
            _errorMessageResourceKey = errorMessageResourceKey;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && value.ToString().Trim() != "" && value is string emailValue)
            {
                if (!IsValidEmail(emailValue))
                {
                    string? errorMessage = Resources.Employee.EmployeeVN.ResourceManager.GetString(_errorMessageResourceKey);
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }

        /// <summary>
        /// kiểm tra email
        /// </summary>
        /// <param name="strIn">email</param>
        /// <returns>true or false</returns>
        /// Created by: ttanh (30/06/2023)
        private bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}
