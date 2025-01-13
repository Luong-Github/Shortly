using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Domain.Enums
{
    [Description("User Status"), DefaultValue(Active)]
    public enum UserStatus
    {
        [Description("Active")]
        Active = 0,
        [Description("Inactive")]
        Inactive = 1,
        [Description("InDeprecated")]
        InDeprecated = 2
    }
}
