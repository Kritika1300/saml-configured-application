using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Saml;
using SSO_TEST.Utility;

namespace SSO_TEST.Controllers
{
    public class HomeController : Controller
    {

        string metadataFilePath;
        public HomeController()
        {
            metadataFilePath = @"";
        }

        public IActionResult Index()
        {
            return View();
        }

      
        public ActionResult Login() {
        

            var idpEndPoint = XmlHandler.GetAttributeValue(metadataFilePath, "SingleSignOnService","Location");

            var request = new AuthRequest(
                "https://localhost:44353",
                "https://localhost:44353/assertionconsumerserviceurl"
             );

            return Redirect(request.GetRedirectUrl(idpEndPoint));
        }

        [Route("assertionconsumerserviceurl")]
        public ActionResult AssertionConsumerService()
        {

            var certificateContent = XmlHandler.GetNodeValue(metadataFilePath, "X509Certificate");

            string X509Certificate = $"-----BEGIN CERTIFICATE-----{certificateContent}-----END CERTIFICATE-----";
           
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


    }
}
