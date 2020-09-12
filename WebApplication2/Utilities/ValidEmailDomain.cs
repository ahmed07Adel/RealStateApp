using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Utilities
{
    public class ValidEmailDomain: ValidationAttribute
    {
        private readonly string AllowDomain;    
        public ValidEmailDomain(string AllowDomain)
        {
            this.AllowDomain = AllowDomain;
        }
        public override bool IsValid(object Value)
        {
           string[] strings = Value.ToString().Split('@');
            return strings[1].ToUpper() == AllowDomain.ToUpper();

        }
    }
}
