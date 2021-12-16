using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Saml;
using SSO_TEST.Utility;
using System;

namespace SSO_TEST.Controllers
{
    public class HomeController : Controller
    {



        string metadataFilePath;

        public HomeController()
        {
            metadataFilePath = @"C:\Users\kkaur\Desktop\SAML\saml-configured-application\SSO_TEST\SAML.xml";
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
            HttpContext.Session.SetString(SessionName, "Jarvik");
            HttpContext.Session.SetInt32(SessionAge, 24);
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

            var request = new AuthRequest(
                "https://localhost:44396",
                "https://localhost:44396/assertionconsumerserviceurl"
             );

            return Redirect(request.GetRedirectUrl(idpEndPoint));
        }

        [Route("assertionconsumerserviceurl")]
        public IActionResult AssertionConsumerService()
        {

            var certificateContent = XmlHandler.GetNodeValue(metadataFilePath, "X509Certificate");

            string X509Certificate = $"{certificateContent}";

            Saml.Response samlResponse = new Response(X509Certificate, Request.Form["SAMLResponse"]);

            string res = samlResponse.Xml.ToString();

            Set("response", res, 10);

            var username = "";

            if (samlResponse.IsValid())
            {
                username = samlResponse.Xml;

            }
            else
            {
                username = "Invalid";
            }


            username = HttpContext.Session.GetString("product");
            //username = HttpContext.Session.Id;

            return View("AssertionConsumerService", username);
        }




    }
}

