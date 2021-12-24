using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Saml;
using SSO_TEST.Utility;
using System;
using WebApp2.Utility;

namespace SSO_TEST.Controllers
{
    public class HomeController : Controller
    {



        string metadataFilePath;

        public HomeController()
        {
            metadataFilePath = @"C:\My Code\SAML app\SSO_TEST\SSO_Test.xml";
        }

        //public IActionResult Index()
        //{

        //    HttpContext.Session.SetString("product", "laptop");

        //    //Set("kay", "Hello from cookie", 10);

        //    return View();
        //}

        const string SessionName = "_Name";
        const string SessionAge = "_Age";
        public IActionResult Index()
        {
            return View();
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



        public IActionResult Login()
        {


            var idpEndPoint = XmlHandler.GetAttributeValue(metadataFilePath, "SingleSignOnService", "Location");

            var request = new MyAuthRequest(
                "https://localhost:44396",
                "https://localhost:44396/assertionconsumerserviceurl"
             );

            if (HttpContext.Request.Cookies["loggedIn"] == "true")   //(User.Identity.IsAuthenticated)
            {
                return Redirect(request.GetRedirectUrl(idpEndPoint));
            }

            return Redirect(request.GetRedirectUrl2(idpEndPoint));
        }

        [Route("assertionconsumerserviceurl")]
        public IActionResult AssertionConsumerService()
        {

            var certificateContent = XmlHandler.GetNodeValue(metadataFilePath, "X509Certificate");

            string X509Certificate = $"{certificateContent}";

            Saml.Response samlResponse = new Response(X509Certificate, Request.Form["SAMLResponse"]);

            Set("loggedIn", "true", 20);

            string res = samlResponse.Xml.ToString();
            
            var username = "";

            if (samlResponse.IsValid())
            {
                username = samlResponse.Xml;

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

