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

        public IActionResult Index()
        {

          
            //Set("kay", "Hello from cookie", 10);
        
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



        public ActionResult Login() {
        

            var idpEndPoint = XmlHandler.GetAttributeValue(metadataFilePath,"SingleSignOnService","Location");

            var request = new AuthRequest(
                "https://localhost:44396",
                "https://localhost:44396/assertionconsumerserviceurl"
             );

            return Redirect(request.GetRedirectUrl(idpEndPoint));
        }

        [Route("assertionconsumerserviceurl")]
        public ActionResult AssertionConsumerService()
        {

            var certificateContent = XmlHandler.GetNodeValue(metadataFilePath, "X509Certificate");

            string X509Certificate = $"{certificateContent}";

            Saml.Response samlResponse = new Response(X509Certificate, Request.Form["SAMLResponse"]);

            string res = samlResponse.Xml.ToString();

            Set("response",res, 10);

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

 


    }
}
