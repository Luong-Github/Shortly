using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Persistence.Options
{
    public class PasswordValidatorOptions
    {
        public int RequiredMinLength { get; set; }
        public int RequiredMaxLenght { get; set; }
        public int RequiredNonLetterOrDigitLength { get; set; }
        public int RequiredLowercaseLength { get; set; }
        public int RequiredUppercaseLength { get; set; }
        public int RequiredDigitLength { get; set; }
    }
}
