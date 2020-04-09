using System.Linq;
using System.Threading.Tasks;
using API.GraphQL;
using CoreLibrary.Exceptions;
using GraphQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
    /// <summary>
    /// Graphql controller.
    /// </summary>
    // TODO remove 
    // [Authorize]
    [Route("graphql")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class GraphQLController : ControllerBase
    {

        #region GraphQL query

        /// <summary>
        /// Graph QL Query model
        /// </summary>
        public class GraphQLQuery
        {
            public string OperationName { get; set; }
            public string NamedQuery { get; set; }
            public string Query { get; set; }
            public JObject Variables { get; set; }
        }
        #endregion

        private readonly IDocumentExecuter _documentExecuter;
        private readonly APISchema _schema;

        public GraphQLController(APISchema schema, IDocumentExecuter documentExecuter)
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
                throw new BadRequestException($"{nameof(GraphQLQuery)} must be provided.");
            }

            var result = await _documentExecuter
                .ExecuteAsync(new ExecutionOptions
                {
                    Schema = _schema,
                    Query = query.Query,
                    Inputs = query.Variables.ToInputs()
                })
                .ConfigureAwait(false);

            if (result.Errors?.Count() > 0)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
