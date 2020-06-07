using Polly;
using Polly.Timeout;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PollyDemo
{
    /// <summary>
    /// 超时策略
    /// </summary>
    public class TimeoutDemo
    {
        public static TimeoutDemo Default = new TimeoutDemo();

        private HttpClient client = new HttpClient();

        private async Task<string> HttpInvokeAsync()
        {
            Console.WriteLine(DateTime.Now.ToString() + "-Begin Http Invoke...");
            await Task.Delay(3000);
            return await client.GetStringAsync("https://www.baidu.com/");
        }

        public async Task RunAsync()
        {
            var policy = Policy.TimeoutAsync(1, (context, timespan, task) =>
            {
                Console.WriteLine("It's Timeout, throw TimeoutRejectedException.");
                return Task.CompletedTask;
            });

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    var res = await policy.ExecuteAsync(HttpInvokeAsync);
                    Console.WriteLine(DateTime.Now.ToString() + "-Run[" + i + "]->Response" + ": Ok->" + res);
                }
                catch (TimeoutRejectedException ex)
                {
                    Console.WriteLine(DateTime.Now.ToString() + "-Run[" + i + "]->Response" + ": TimeoutRejectedException->" + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(DateTime.Now.ToString() + "-Run[" + i + "]->Response" + ": Exception->" + ex.Message);
                }
            }
        }
    }
}
