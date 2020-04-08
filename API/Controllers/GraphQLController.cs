using System;
using System.Threading.Tasks;
using CoreLibrary.Exceptions;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
    /// <summary>
    /// Graphql controller.
    /// </summary>
    [Authorize]
    [Route("graphql")]
    [Produces("application/json")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class GraphQLController : ControllerBase
    {

        #region GraphQL query

        public class GraphQLQuery
        {
            public string OperationName { get; set; }
            public string NamedQuery { get; set; }
            public string Query { get; set; }
            public JObject Variables { get; set; } //https://github.com/graphql-dotnet/graphql-dotnet/issues/389
        }
        #endregion

        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;

        public GraphQLController(ISchema schema, IDocumentExecuter documentExecuter)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
        }

        /// <summary>
        /// Endpoint for Graph QL API
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var result = await _documentExecuter
                .ExecuteAsync(new ExecutionOptions
                {
                    Schema = _schema,
                    Query = query.Query,
                    Inputs = query.Variables.ToInputs()
                })
                .ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                throw new BadRequestException(string.Join("\n", result.Errors));
            }

            return Ok(result);
        }
    }
}
