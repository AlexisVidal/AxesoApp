using Axeso_BE;
using GraphQL.Client;
using GraphQL.Common.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Axeso_DA
{
    public class CulturaDA
    {
        //Config 
        public async Task<List<Cultura>> GetCulturas()
        {
            List<Cultura> lstEntidad = null;
            //using (GraphQLClient graphQLClient = new GraphQLClient("http://localhost:64034/graphql"))
            using (GraphQLClient graphQLClient = new GraphQLClient("http://localhost/webapi/graphql"))
            {
                var query = new GraphQLRequest
                {
                    Query = @" 
                        { culturas 
                            { iD culturaID culturaID_pad nombre } 
                        }",
                };
                var response = await graphQLClient.PostAsync(query);
                var lists = response.GetDataFieldAs<List<Cultura>>("culturas");
                if (lists.Any())
                {
                    lstEntidad = lists;
                }
            }
            return lstEntidad;
        }

        public async Task<Cultura> Get(int id)
        {
            //using (GraphQLClient graphQLClient = new GraphQLClient("http://localhost:64034/graphql"))
            using (GraphQLClient graphQLClient = new GraphQLClient("http://localhost/webapi/graphql"))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                       query culturaQuery($id: UsuarioID!)  
                        { usuario(id: $UsuarioID)   
                            { usuarioID tipoUsuarioID usuario descripcion email telefono activo bloqueado token fchHraCreacion fchHraActualizacion fchHraBloqueo fchHraDesbloqueo }  
                        }",
                    Variables = new { ID = id }
                };
                var response = await graphQLClient.PostAsync(query);
                return response.GetDataFieldAs<Cultura>("cultura");
            }
        }
    }
}
