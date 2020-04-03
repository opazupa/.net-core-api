using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntegrationTests.Utils.Setup
{
    /// <summary>
    /// Base class for integration tests providing HTTP client for test server and common functions. 
    /// </summary>
    public class BaseIntegrationTest
    {
        #region test client
        private readonly HttpClient Client = new HostBuilder()
                            .ConfigureWebHost(webHost =>
                            {
                                webHost.UseContentRoot(".");
                                // Use testing environment
                                webHost.UseEnvironment("Testing");
                                webHost.ConfigureAppConfiguration((ctx, conf) =>
                                {
                                    // Include testing appsettings
                                    conf.SetBasePath(Directory.GetCurrentDirectory());
                                    conf.AddJsonFile($"appsettings.json", optional: false);
                                    conf.AddJsonFile($"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json", optional: false);
                                });
                                // Add TestServer
                                webHost.UseTestServer();
                                webHost.UseStartup<TestStartup>();
                            })
                            .Start()
                            .GetTestClient();

        #endregion


        /// <summary>
        /// Execute a GET request with given url and optional parametrs.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<T> Get<T>(string url, object param = null)
        {
            HttpResponseMessage getResponse = await Client.GetAsync(url + IntegrationTestHelper.ToQueryString(param));

            await IntegrationTestHelper.CheckHttpErrorResponse(getResponse);

            return JsonConvert.DeserializeObject<T>(await getResponse.Content.ReadAsStringAsync());
        }
    }
}