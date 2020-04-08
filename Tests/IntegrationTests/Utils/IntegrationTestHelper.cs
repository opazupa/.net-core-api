using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoreLibrary.Exceptions;
using FeatureLibrary.Models;
using Newtonsoft.Json;
using static FeatureLibrary.Models.SeedData;

namespace IntegrationTests.Utils
{
    /// <summary>
    /// Helper methods for integration testing.
    /// </summary>
    public static class IntegrationTestHelper
    {
        /// <summary>
        /// Check given http response for certain errors.
        /// If configured error response is received, corresponding error is thrown.
        /// </summary>
        /// <param name="response"></param>
        public async static Task CheckHttpErrorResponse(HttpResponseMessage response)
        {
            string msg = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException(msg);
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedException();
                case HttpStatusCode.NotFound:
                    throw new NotFoundException(msg);
                case HttpStatusCode.InternalServerError:
                    throw new InternalServerException(msg);
                default:
                    break;
            }
        }

        /// <summary>
        /// Parse given object to a query string.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToQueryString(this object obj)
        {
            if (obj == null) return "";

            return "?" + string.Join("&", obj.GetType()
                .GetProperties()
                .Where(p => p.GetValue(obj) != null)
                .Select(p => $"{Uri.EscapeDataString(p.Name)}={Uri.EscapeDataString(p.GetValue(obj).ToString())}"));
        }

        /// <summary>
        /// Login and set bearer token by admin creds
        /// </summary>
        /// <returns></returns>
        public async static Task<HttpClient> LoginAsAdmin(this HttpClient client)
        {
            var adminAuth = new Authentication
            {
                Username = ADMIN_USER.UserName,
                Password = ADMIN_USER.Password
            };

            HttpResponseMessage authResponse = await client.PostAsync("api/auth/login", new StringContent(JsonConvert.SerializeObject(adminAuth), Encoding.UTF8, "application/json"));
            var token = JsonConvert.DeserializeObject<AuthenticationResult>(await authResponse.Content.ReadAsStringAsync()).Token;

            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            return client;
        }
    }
}