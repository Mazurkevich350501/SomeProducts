
using System.Web.Mvc;
using SomeProducts.CrossCutting.ProjectLogger;

namespace SomeProducts.Controllers
{
    [HandleErrorLog]
    [RequireHttps]
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Default(string aspxerrorpath)
        {
            return View("Error", (object)$"Error on {aspxerrorpath} page");
        }

        public ActionResult NotFound(string aspxerrorpath)
        {
            return View("Error", (object)$"Page was not fount");
        }

        public ActionResult InternalServer(string aspxerrorpath)
        {
            return View("Error", (object)$"Internal server error");
        }
    }
}