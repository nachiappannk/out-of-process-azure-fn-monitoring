using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctionMonitoring
{
    public static class Function1
    {
        [Function("function1")]
        public static HttpResponseData Run1([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var random = new Random().Next(0, 1000);
            if (random > 950)
            {
                throw new NotImplementedException();
            }

            if (random > 900)
            {
                throw new ApplicationException();
            }

            var logger = executionContext.GetLogger("Function1");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            if (random < 100)
            {
                return SendResponse(req, HttpStatusCode.Accepted, "A different message");
            }
            return SendResponse(req, HttpStatusCode.OK, "Welcome to Azure Functions!");
        }

        private static HttpResponseData SendResponse(HttpRequestData req, HttpStatusCode code, string message)
        {
            var response = req.CreateResponse(code);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.WriteString(message);
            return response;
        }

        [Function("function2")]
        public static HttpResponseData Run2([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
    FunctionContext executionContext)
        {
            var random = new Random().Next(0, 1000);
            if (random > 950)
            {
                throw new NotImplementedException();
            }

            if (random > 900)
            {
                throw new ApplicationException();
            }

            var logger = executionContext.GetLogger("Function1");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            if (random < 100)
            {
                return SendResponse(req, HttpStatusCode.Accepted, "A different message");
            }
            return SendResponse(req, HttpStatusCode.OK, "Welcome to Azure Functions!");
        }
    }

    public class ExceptionLoggingMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            try
            {
                // Code before function execution here
                await next(context);
                // Code after function execution here
            }
            catch (Exception ex)
            {
                var log = context.GetLogger<ExceptionLoggingMiddleware>();
                log.LogError(ex, JsonConvert.SerializeObject(new Dictionary<string, string>() { 
                    ["UnhandeledException"] = "Yes",
                    ["ExceptionType"] = ex.GetType().FullName 
                }));
                throw;
            }
        }
    }
}
