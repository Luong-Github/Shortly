using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Domain.Enums
{
    [Description("Url Privacy Level"), DefaultValue(Public)]
    public enum UrlPrivacyLevel
    {
        [Description("Public")]
        Public,
        [Description("Private")]
        Private,
        [Description("Protected")]
        Protected
    }
}
