using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionMonitoring
{
    public static class Function1
    {
        [Function("function1")]
        public static HttpResponseData Run1([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
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
        public static HttpResponseData Run2([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
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
}
