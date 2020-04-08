using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

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
            HttpResponseMessage getResponse = await
                (await Client.LoginAsAdmin())
                .GetAsync(url + IntegrationTestHelper.ToQueryString(param));

            await IntegrationTestHelper.CheckHttpErrorResponse(getResponse);

            return JsonSerializer.Deserialize<T>(await getResponse.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Execute a POST request with given url and optional parametrs.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<T> Post<T>(string url, object content = null)
        {
            HttpResponseMessage postResponse = await
                (await Client.LoginAsAdmin())
                .PostAsync(url, new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json"));

            await IntegrationTestHelper.CheckHttpErrorResponse(postResponse);

            return JsonSerializer.Deserialize<T>(await postResponse.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Execute a DELETE request with given url and optional parametrs.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<T> Delete<T>(string url)
        {
            HttpResponseMessage deleteResponse = await
                (await Client.LoginAsAdmin())
                .DeleteAsync(url);

            await IntegrationTestHelper.CheckHttpErrorResponse(deleteResponse);

            return JsonSerializer.Deserialize<T>(await deleteResponse.Content.ReadAsStringAsync());
        }
    }
}