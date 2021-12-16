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

namespace WebApp2.Controllers
{
    public class HomeController : Controller
    {
        string metadataFilePath;
        public HomeController()
        {
            metadataFilePath = @"C:\Users\kkaur\Desktop\SAML\saml-configured-application\WebApp2\WebApp2\WebApp2.xml";
        }

        public IActionResult Index()
        {
            var response = HttpContext.Session.GetString("product");
            return View("Index",response);

        }

        public ActionResult Login()
        {


            var idpEndPoint = XmlHandler.GetAttributeValue(metadataFilePath, "SingleSignOnService", "Location");

            var request = new AuthRequest(
                "https://localhost:44383",
                "https://localhost:44383/assertionconsumerserviceurl"
             );

            return Redirect(request.GetRedirectUrl(idpEndPoint));
        }

        [HttpPost]
        [Route("assertionconsumerserviceurl")]
        public ActionResult AssertionConsumerService()
        {
           
            var certificateContent = XmlHandler.GetNodeValue(metadataFilePath, "X509Certificate");

            string X509Certificate = $"{certificateContent}";

            Saml.Response samlResponse = new Response(X509Certificate, Request.Form["SAMLResponse"]);

           

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

        [Route("welcome")]
        public IActionResult Welcome()
        {
            return View();
        }


    }
}
