using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;

namespace StoryboardAPI.Authorization
{
    public class AuthorizationServerProvide :OAuthAuthorizationServerProvider
    {
        validateUser objvalidateuser = new validateUser();
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            var companyCode = string.Join(",",context.Scope);
            var status = objvalidateuser.isvalid(context.UserName, context.Password, companyCode);
            if(status==true)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim("username", "admin"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Admin"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Provided user credentials are incorrect");
                return;
            }
            //if (context.UserName=="admin" && context.Password=="admin")
            //{
            //    identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            //    identity.AddClaim(new Claim("username", "admin"));
            //    identity.AddClaim(new Claim(ClaimTypes.Name, "Admin"));
            //    context.Validated(identity);
            //}
            //else if(context.UserName == "admin" && context.Password == "admin")
            //{
            //    identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            //    identity.AddClaim(new Claim("username", "user"));
            //    identity.AddClaim(new Claim(ClaimTypes.Name, "user"));
            //    context.Validated(identity);
            //}
            //else
            //{
            //    context.SetError("invalid_grant", "Provided user credentials are incorrect");
            //    return;
            //}
        }
    }
}