using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace AzureFunctionMonitoring
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults(builder =>
                    {
                        builder.UseMiddleware<ExceptionLoggingMiddleware>();
                    })
                .Build();
            host.Run();
        }
    }
}