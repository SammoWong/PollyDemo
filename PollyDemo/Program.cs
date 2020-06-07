using System;
using System.Threading.Tasks;

namespace PollyDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //await RetryDemo.Default.RunAsync();
            //await TimeoutDemo.Default.RunAsync();
            //await FallbackDemo.Default.RunAsync();
            //await CircuitBreakerDemo.Default.RunAsync();
            await PolicyWrapDemo.Default.RunAsync();

            Console.ReadKey();
        }
    }
}
