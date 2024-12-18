using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Share.Enums
{
    public enum HashAlgorithmTypeEnum
    {
        [Description("SHA-256 algorithm")]
        SHA256,
        [Description("MD5 algorithm")]
        MD5
    }
}
