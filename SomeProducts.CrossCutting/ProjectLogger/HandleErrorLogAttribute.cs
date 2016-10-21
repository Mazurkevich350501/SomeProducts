

using System.Web.Mvc;

namespace SomeProducts.CrossCutting.ProjectLogger
{
    public class HandleErrorLogAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            ProjectLogger.Error($"Message: {filterContext.Exception.Message};" +
                                $" Source: {filterContext.Exception.Source}");
            base.OnException(filterContext);
        }        
    }
}
