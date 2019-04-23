using System;
using System.Linq;
using System.Net;
using System.Net.Http;

using CoreLibrary.Exceptions;

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
        public static void CheckHttpErrorResponse(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException();
                case HttpStatusCode.NotFound:
                    throw new NotFoundException();
                case HttpStatusCode.InternalServerError:
                    throw new InternalServerException();
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
    }
}