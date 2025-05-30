using FluentValidation;
using FluentValidation.Validators;
using SistemaFinanceiro.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SistemaFinanceiro.Application.UseCases.Users
{
    public class PasswordValidator<T> : PropertyValidator<T, string>
    {

        const string ERROR_MESSAGE_KEY = "ErrorMessage"; 
        public override string Name => "PasswordValidator";


        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return $"{{{ERROR_MESSAGE_KEY}}}";
        }

        public override bool IsValid(ValidationContext<T> context, string password)
        {
            if(string.IsNullOrWhiteSpace(password) ||
               password.Length < 8 ||
               !Regex.IsMatch(password, @"[A-Z]+") ||
               !Regex.IsMatch(password, @"[a-z]+") ||
               !Regex.IsMatch(password, @"[1-9]+") ||
               !Regex.IsMatch(password, @"[\!\?\@\*]+"))
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.SENHA_USER_INVALIDO);
                return false;
            }


            return true;

        }

        
    }
}
