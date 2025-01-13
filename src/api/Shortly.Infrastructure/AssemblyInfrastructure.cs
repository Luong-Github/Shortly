using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Infrastructure
{
    public static class AssemblyInfrastructure
    {
        public static Assembly Assembly = typeof(AssemblyInfrastructure).Assembly;
    }
}
