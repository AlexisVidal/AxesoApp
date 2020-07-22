using Axeso_BE;
using GraphQL.Client;
using GraphQL.Common.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.Controllers
{
    [Route("Cultura")]
    public class CulturaController : Controller
    {
        [HttpGet]
        public async Task<List<Cultura>> Get()
        {
            using (GraphQLClient graphQLClient = new GraphQLClient("http://localhost:64034/graphql"))
            {
                var query = new GraphQLRequest
                {
                    Query = @" 
                        { culturas 
                            { ID CulturaID CulturaID_pad Nombre } 
                        }",
                };
                var response = await graphQLClient.PostAsync(query);
                return response.GetDataFieldAs<List<Cultura>>("culturas");
            }
        }
    }
}
