
using System.Web.Mvc;

namespace SomeProducts.Controllers
{
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