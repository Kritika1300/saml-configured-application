using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Saml;
using SSO_TEST.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp2.Models;
using WebApp2.Utility;

namespace WebApp2.Controllers
{
    public class HomeController : Controller
    {
        string metadataFilePath;
        public HomeController()
        {
            metadataFilePath = @"C:\My Code\SAML app\WebApp2\WebApp2\WebApp2.xml";
        }

        public IActionResult Index()
        {
            return View("Index");

        }

        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();



            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            Response.Cookies.Append(key, value, option);
        }

        public ActionResult Login()
        {


            var idpEndPoint = XmlHandler.GetAttributeValue(metadataFilePath, "SingleSignOnService", "Location");

            var request = new MyAuthRequest(
                "https://localhost:44383",
                "https://localhost:44383/assertionconsumerserviceurl"
             );

            if (HttpContext.Request.Cookies["loggedIn"] == "true")  //(User.Identity.IsAuthenticated)
            {
                return Redirect(request.GetRedirectUrl(idpEndPoint));
            }

            return Redirect(request.GetRedirectUrl2(idpEndPoint));
        }

        [HttpPost]
        [Route("assertionconsumerserviceurl")]
        public ActionResult AssertionConsumerService()
        {
           
            var certificateContent = XmlHandler.GetNodeValue(metadataFilePath, "X509Certificate");

            string X509Certificate = $"{certificateContent}";

            Saml.Response samlResponse = new Response(X509Certificate, Request.Form["SAMLResponse"]);

            Set("loggedIn", "true", 20);


            var username = "";

            if (samlResponse.IsValid())
            {
                username = samlResponse.GetEmail();

            }
            else
            {
                username = "Invalid";
            }

            return View("AssertionConsumerService", username);
        }

        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Set("loggedIn", "false", 20);
            return RedirectToAction("Index");
        }
    }
}
