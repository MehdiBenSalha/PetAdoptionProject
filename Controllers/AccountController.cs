using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using MySql.Data.MySqlClient;
using PetAdoption_dotnet.Models;
using PetAdoption.Data;
using PetAdoption_dotnet.Data;
using PetAdoption.Models.DbModels;

namespace PetAdoption_dotnet.Controllers
{
    [AllowAnonymous, Route("account")]
    public class AccountController : Controller
    {
          /*public async Task Login()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
            return Json(claims);
        }*/
        [Route("google-login")]
        
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
           var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault()
                .Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
             //Json(claims);
            
            return RedirectToAction("CheckAccount");
        }
        public IActionResult CheckAccount()
        {      UnitOfWork u=new UnitOfWork(ApplicationContext.Instance);
               if(User.Identity.IsAuthenticated){
                string mail="http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
                var t=User.Claims.FirstOrDefault(c => c.Type == mail).Value.ToString();
                if(((UserrRepository)u.User).Get(t)!=null){                            /// to do IUserRepository..
                 return  RedirectToAction("Index","Home");
                }
                
                 
               }
               return  View(); 
        }
        [HttpPost]
        public IActionResult Signup(Userr ob)
        {    UnitOfWork u=new UnitOfWork(ApplicationContext.Instance);
             ob.userrId=User.Claims.FirstOrDefault(c => c.Type =="http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value.ToString();
             ((UserrRepository)u.User).Insert(ob);
            u.Complete();
             return  RedirectToAction("Index","Home");
        }
        /*public IActionResult NewAccount()
        {
               return  RedirectToAction("Index","Home"); 
        }*/
        [Route("google-logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}