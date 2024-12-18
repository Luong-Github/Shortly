using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Domain.Enums
{
    [Description("Account Status"), DefaultValue(Anonymous)]
    public enum AccountStatus
    {
        [Description("Anonymous")]
        Anonymous = 0,
        [Description("Free")]
        Free = 1,
        [Description("Monthly")]
        Monthly = 2,
        [Description("Premium")]
        Premium = 3,
        [Description("Expired")]
        Expired = 4
    }
}
