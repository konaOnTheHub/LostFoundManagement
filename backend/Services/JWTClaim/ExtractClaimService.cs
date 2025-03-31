using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace backend.Services.JWTClaim
{
    public class ExtractClaimService
    {
        public static int ExtractNameIdentifier(ClaimsPrincipal user) 
        {
            //Extract the NameIdentifier claim from the JWT token which is the PK of the user in the database
            var loggedUserString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //Convert to INT as claims are stored in string
            var loggedUserId = int.Parse(loggedUserString!);
            return loggedUserId;
        }
    }
}