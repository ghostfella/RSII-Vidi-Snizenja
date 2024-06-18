using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidiSnizenja.Application.Shared
{
    public class JwtConfiguration
    {
        public TimeSpan TokenDuration { get; }
        public TimeSpan RefreshTokenDuration { get; }
        public SymmetricSecurityKey SecurityKey { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public string SecretKey = "KqcL7s998JrfFHRP1NaondfarnJMPO$&sdf!";
    }
}
