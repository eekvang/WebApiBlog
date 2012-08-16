using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;  
using System.Web.Http.ModelBinding;

namespace WebApiBlog.Filters
{
    public class ValidationActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(BuildError).ToArray();

                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, errors);
            }   
        }

        private static Error BuildError(KeyValuePair<string, ModelState> e)
        {
            return new Error
                       {
                           Name = e.Key,
                           Message = e.Value.Errors.First().ErrorMessage
                       };
        }
    }

    public class Error
    {
        public string Name { get; set; }
        public string Message { get; set; }
    }
}