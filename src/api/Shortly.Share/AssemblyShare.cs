using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Share
{
    public static class AssemblyShare
    {
        public static readonly Assembly Assembly = typeof(AssemblyShare).Assembly;
    }
}
