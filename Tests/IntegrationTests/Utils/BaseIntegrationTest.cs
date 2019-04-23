using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Utils.Setup
{
    /// <summary>
    /// Base class for integration tests providing HTTP client for test server and common functions. 
    /// </summary>
    public class BaseIntegrationTest : IClassFixture<APITestFactory<TestStartup>>
    {
        private readonly WebApplicationFactory<TestStartup> _factory;
        private readonly HttpClient Client;

        /// <summary>
        /// Create new test server and http client with TestStartup.
        /// </summary>
        /// <param name="factory"></param>
        public BaseIntegrationTest(APITestFactory<TestStartup> factory)
        {
            _factory = factory;
            Client = _factory.CreateClient();
        }

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

            IntegrationTestHelper.CheckHttpErrorResponse(getResponse);

            return JsonConvert.DeserializeObject<T>(await getResponse.Content.ReadAsStringAsync());
        }
    }
}