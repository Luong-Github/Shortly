using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Infrastructure.DependencyInjections.Options
{
    public class JwtOptions
    {
        public string Key { get; set; }

        public string Issuer { get; set; }
        
        public string Audience { get; set; }
        
        public double TokenExpiryInMinutes { get; set; }
    }
}
