using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Zip.InstallmentsService.Filters
{
    public class ProcessException : Exception
    {
        public ProcessException(string message)
        : base(message)
        {

        }
    }
    public class ProcessExceptionFilterAttribute : ExceptionFilterAttribute, IExceptionFilter
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //Checks the Exception Type

            if (actionExecutedContext.Exception is ProcessException)
            {
                //The Response Message Set by the Action during Execution
                var res = actionExecutedContext.Exception.Message;

                //Define the Response Message
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(res),
                    ReasonPhrase = res
                };

                //Create the Error Response
                actionExecutedContext.Response = response;
            }
        }
    }
}
