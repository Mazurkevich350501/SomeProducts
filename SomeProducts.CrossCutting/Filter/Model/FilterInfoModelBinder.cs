
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace SomeProducts.CrossCutting.Filter.Model
{
    public class FilterInfoModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            var jsonResult = request.QueryString[bindingContext.ModelName];
            return jsonResult != null
                ? new FilterInfo() {
                    Filters = JsonConvert.DeserializeObject<List<Filter>>(jsonResult)
                }
                : null;
        }
    }
}
