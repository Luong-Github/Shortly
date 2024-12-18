using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Persistence.Constants
{
    public static class TableName
    {
        internal const string ApplicationUser = nameof(ApplicationUser);
        internal const string ApplicationRole = nameof(ApplicationRole);
        internal const string AppUserRole = nameof(AppUserRole);

        internal const string ApplicationUserClaims = nameof(ApplicationUserClaims);
        internal const string ApplicationRoleClaims = nameof(ApplicationRoleClaims);
        internal const string ApplicationUserLogins = nameof(ApplicationUserLogins);
        internal const string ApplicationUserTokens = nameof(ApplicationUserTokens);

        internal const string Url = nameof(Url);
        internal const string UserUrl = nameof(UserUrl);
    }
}
