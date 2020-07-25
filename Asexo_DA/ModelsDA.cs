using Axeso_BE;
using GraphQL.Client;
using GraphQL.Common.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Security.Cryptography;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using Axeso_DA.Properties;

namespace Axeso_DA
{
    public class ModelsDA
    {
        //string urlgraphql = Settings.Default.AxesoUri;
        string urlgraphql = "http://190.117.184.215/AxesoGraphQL/graphql";
        //string urlgraphql = "http://192.168.1.100/AxesoGraphQL/graphql";
        UsuarioInput usuarioi = null;
        AnyBoolModel respuestabool = null;
        #region UnidadMedida
        public async Task<List<UnidadMedida>> GetUnidadMedidas()
        {
            List<UnidadMedida> lstEntidad = null;
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @" 
                        { 
                            unidadMedidas{
                                iD unidadID abreviatura nombre esUndiadComercial esUnidadGranel activo
                            }
                        }",
                };
                var response = await graphQLClient.PostAsync(query);
                var lists = response.GetDataFieldAs<List<UnidadMedida>>("unidadMedidas");
                if (lists.Any())
                {
                    lstEntidad = lists;
                }
            }
            return lstEntidad;
        }
        #endregion
        #region Categorias
        public async Task<List<Categoria>> GetCategorias()
        {
            List<Categoria> lstEntidad = null;
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @" 
                        { categorias 
                            {
                                iD categoriaID nombre abreviatura categoriaID_pad tipoNegocioID activo orden imagen
                            }
                        }",
                };
                var response = await graphQLClient.PostAsync(query);
                var lists = response.GetDataFieldAs<List<Categoria>>("categorias");
                if (lists.Any())
                {
                    lstEntidad = lists;
                }
            }
            return lstEntidad;
        }
        #endregion
        #region UsuarioModel
        //{ usuarioID tipoUsuarioID usuario descripcion email telefono activo bloqueado token fchHraCreacion fchHraActualizacion fchHraBloqueo fchHraDesbloqueo} 
        public async Task<List<Usuarios>> GetUsuarios()
        {
            List<Usuarios> lstEntidad = null;
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @" 
                        { usuarios 

                            { 
                                usuarioID 
                                tipoUsuarioID 
                                usuario 
                                descripcion 
                                email 
                                telefono 
                                activo 
                                bloqueado 
                                token 
                                fchHraCreacion 
                                fchHraActualizacion 
                                fchHraBloqueo 
                                fchHraDesbloqueo 
                                dataFarmaciasID 
                                fechaNacimiento
                            } 
                        }",
                };
                var response = await graphQLClient.PostAsync(query);
                var lists = response.GetDataFieldAs<List<Usuarios>>("usuarios");
                if (lists.Any())
                {
                    lstEntidad = lists;
                }
            }
            return lstEntidad;
        }

        public async Task<Usuarios> Get(int id)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query usuarioQuery($usuarioID: ID!)  
                        { usuario(usuarioID: $usuarioID)   
                            { usuarioID tipoUsuarioID usuario descripcion email telefono activo bloqueado token fchHraCreacion fchHraActualizacion fchHraBloqueo fchHraDesbloqueo dataFarmaciasID fechaNacimiento }  
                        }",
                    Variables = new { UsuarioID = id }
                };
                var response = await graphQLClient.PostAsync(query);
                return response.GetDataFieldAs<Usuarios>("usuario");
            }
        }
        public async Task<Usuarios> Login(string login)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query loginQuery($login: String!)  
                        { login(login: $login)   
                            { usuarioID tipoUsuarioID usuario descripcion email telefono activo bloqueado token fchHraCreacion fchHraActualizacion fchHraBloqueo fchHraDesbloqueo dataFarmaciasID fechaNacimiento }  
                        }",
                    Variables = new { Login = login }
                };

                try
                {

                    var response = await graphQLClient.PostAsync(query);
                    var getdat = response.GetDataFieldAs<Usuarios>("login");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public async Task<Usuarios> CreateUsuario(UsuarioInput usuarioToCreate)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"
                    mutation($usuario: usuarioInput!){
                      createUsuario(usuario: $usuario){
                        usuarioID,
                        tipoUsuarioID,
                        usuario,
                        descripcion,
                        email,
                        activo,
                        bloqueado,
                        token,
                        fchHraCreacion,
                        fchHraActualizacion,
                        fchHraBloqueo,
                        fchHraDesbloqueo,
                        dataFarmaciasID,
                        fechaNacimiento
                      }
                    }",
                    Variables = new { usuario = usuarioToCreate }
                };

                var response = await graphQLClient.PostAsync(query);
                return response.GetDataFieldAs<Usuarios>("createUsuario");
            }
        }



        public async Task<Usuarios> UpdateUsuario(int usuarioID, UsuarioInput usuarioToUpdate)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"
                mutation($usuario: usuarioInput!, $usuarioID: ID!){
                  updateUsuario(usuario: $usuario, usuarioID: $usuarioID){
                        usuarioID,
                        tipoUsuarioID,
                        usuario,
                        descripcion,
                        email,
                        activo
                  }
               }",
                    Variables = new { usuario = usuarioToUpdate, usuarioID = usuarioID }
                };

                var response = await graphQLClient.PostAsync(query);
                return response.GetDataFieldAs<Usuarios>("updateUsuario");
            }
        }
        public async Task<string> DeleteUsuario(int usuarioID)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"
                   mutation($usuarioID: ID!){
                      deleteUsuario(usuarioID: $usuarioID)
                    }",
                    Variables = new { usuarioID = usuarioID }
                };

                var response = await graphQLClient.PostAsync(query);
                return response.Data.deleteOwner;
            }
        }
        public async Task<Usuarios> RecoverPassUsuario(int id, UsuarioInput usuarioToUpdate)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"
                mutation($usuario: usuarioInput!, $usuarioID: ID!){
                  updatetokenUsuario(usuario: $usuario, usuarioID: $usuarioID){
                    usuarioID,
                    token
                  }
               }",
                    Variables = new { usuario = usuarioToUpdate, usuarioID = id }
                };

                var response = await graphQLClient.PostAsync(query);
                return response.GetDataFieldAs<Usuarios>("updatetokenUsuario");
            }
        }
        public EncryptedTokenModel EncryptedToken(string token)
        {
            string claveencrip = "";
            EncryptedTokenModel newencrypt = new EncryptedTokenModel();
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] inputBytes = (new System.Text.UnicodeEncoding()).GetBytes(token);
                byte[] hash = sha1.ComputeHash(inputBytes);

                claveencrip = Convert.ToBase64String(hash);
                newencrypt = new EncryptedTokenModel()
                {
                    TokenEncrypted = claveencrip
                };
            }
            catch (Exception)
            {
                newencrypt = new EncryptedTokenModel()
                {
                    TokenEncrypted = ""
                };
            }
            return newencrypt;
        }
        public AnyStringModel GeneraPass()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            AnyStringModel anynuevo = new AnyStringModel()
            {
                AnyString = new String(stringChars)
            };
            return anynuevo;

        }
        public async Task<AnyBoolModel> CambiaClave(MailModel newmailmodel)
        {
            respuestabool = new AnyBoolModel();
            string newpass = "";
            if (String.IsNullOrEmpty(newmailmodel.newPassword))
            {
                var anynuevo = GeneraPass();
                newpass = anynuevo.AnyString.ToString();
            }
            else
            {
                newpass = newmailmodel.newPassword;
            }

            var subject = newmailmodel.AppName + " - " + newmailmodel.PasswordRecovery;
            string yournewp = newmailmodel.PasswordYourNew;
            string dontforg = newmailmodel.PasswordDontForget;
            var body = string.Format(@"
                        <h1>{0}</h1>
                        <p>{1} <strong>{2}</strong></p>
                        <p>{3}", subject, yournewp, newpass, dontforg);
            string claveencrip = "";
            try
            {
                var tokenmodel = EncryptedToken(newpass);
                if (tokenmodel.TokenEncrypted.Equals(""))
                {
                    respuestabool = new AnyBoolModel()
                    {
                        respuesta = false
                    };
                }
                else
                {
                    claveencrip = tokenmodel.TokenEncrypted.ToString();
                }

                usuarioi = new UsuarioInput()
                {
                    UsuarioID = newmailmodel.id,
                    Token = claveencrip
                };

                var actualiza = await RecoverPassUsuario(newmailmodel.id, usuarioi);
                if (actualiza != null)
                {
                    await MailHelper.SendMail(newmailmodel);
                }
                //resultado = true;
                respuestabool = new AnyBoolModel()
                {
                    respuesta = true
                };
            }
            catch (Exception ex)
            {
                respuestabool = new AnyBoolModel()
                {
                    respuesta = false
                };
            }
            return await Task.FromResult(respuestabool);
        }
        #endregion
        #region Producto

        public async Task<IEnumerable<ProductoMarca>> GetAllProductoMarca()
        {
            List<ProductoMarca> lstEntidad = null;
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @" 
                        { 
                            productomarcas{
                                productoMarcaID
                                nombre
                                imagen
                                activo
                            }
                        }",
                };
                var response = await graphQLClient.PostAsync(query);
                var lists = response.GetDataFieldAs<List<ProductoMarca>>("productomarcas");
                if (lists.Any())
                {
                    lstEntidad = lists;
                }
            }
            return lstEntidad;
        }


        public async Task<IEnumerable<Producto>> GetProductos()
        {
            List<Producto> lstEntidad = null;
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @" 
                        { productos{
                            iD productoID	tipoNegocioID	nombre	abreviatura	unidadID_com	unidadID_gra imagen requiereReceta productoMarcaID precioRef_com precioRef_gra
                            categoria{
                              iD tipoNegocioID	categoriaID	categoriaID_pad	nombre	abreviatura	orden	activo
                            }
                            unidadMedida{
                              iD unidadID nombre abreviatura esUnidadGranel esUndiadComercial activo
                            }
                          }
                        }",
                };
                var response = await graphQLClient.PostAsync(query);
                var lists = response.GetDataFieldAs<List<Producto>>("productos");
                if (lists.Any())
                {
                    lstEntidad = lists;
                }
            }
            return lstEntidad;
        }

        public async Task<IEnumerable<Producto>> BuscaProducto(string productosnombre)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query productosnombreQuery($productosnombre: String!)  
                        { 
                            productosnombre(productosnombre: $productosnombre)   {
                                iD 
                                productoID	
                                tipoNegocioID	
                                nombre	
                                abreviatura	
                                unidadID_com	
                                unidadID_gra
                                imagen
                                requiereReceta
                                productoMarcaID
                                precioRef_com 
                                precioRef_gra
                                categoria {
                                    iD 
                                    tipoNegocioID	
                                    categoriaID	
                                    nombre	
                                    abreviatura	
                                    orden	
                                    activo
                                }
                                unidadMedida {
                                    iD 
                                    unidadID 
                                    nombre 
                                    abreviatura 
                                    esUnidadGranel 
                                    esUndiadComercial 
                                    activo
                                }
                            }
                        }",
                    Variables = new { Productosnombre = productosnombre }
                };

                try
                {

                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<List<Producto>>("productosnombre");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<IEnumerable<Producto>> BuscaProductoId(int productosproductoID)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query productosproductoIDQuery($productosproductoID: Int!)  
                        { 
                            productosproductoID(productosproductoID: $productosproductoID)   {
                                iD 
                                productoID	
                                tipoNegocioID	
                                nombre	
                                abreviatura	
                                unidadID_com	
                                unidadID_gra
                                imagen
                                requiereReceta
                                productoMarcaID
                                precioRef_com 
                                precioRef_gra
                                categoria {
                                    iD 
                                    tipoNegocioID	
                                    categoriaID	
                                    nombre	
                                    abreviatura	
                                    orden	
                                    activo
                                }
                                unidadMedida {
                                    iD 
                                    unidadID 
                                    nombre 
                                    abreviatura 
                                    esUnidadGranel 
                                    esUndiadComercial 
                                    activo
                                }
                            }
                        }",
                    Variables = new { ProductosproductoID = productosproductoID }
                };

                try
                {

                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<List<Producto>>("productosproductoID");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<IEnumerable<Producto>> BuscaProductoCategoriaId(int productoscategoriaID)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query productoscategoriaIDQuery($productoscategoriaID: Int!)  
                        { 
                            productoscategoriaID(productoscategoriaID: $productoscategoriaID)   {
                                iD 
                                productoID	
                                tipoNegocioID	
                                nombre	
                                abreviatura	
                                unidadID_com	
                                unidadID_gra
                                imagen
                                requiereReceta
                                precioRef_com 
                                precioRef_gra
                                productoMarcaID
                                categoria {
                                    iD 
                                    tipoNegocioID	
                                    categoriaID	
                                    nombre	
                                    abreviatura	
                                    orden	
                                    activo
                                }
                                unidadMedida {
                                    iD 
                                    unidadID 
                                    nombre 
                                    abreviatura 
                                    esUnidadGranel 
                                    esUndiadComercial 
                                    activo
                                }
                            }
                        }",
                    Variables = new { ProductoscategoriaID = productoscategoriaID }
                };

                try
                {

                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<List<Producto>>("productoscategoriaID");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<IEnumerable<Producto>> BuscaProductosByProductoMarcaID(int productoMarcaID)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query productoMarcaIDQuery($productoMarcaID: Int!)  
                        { 
                            productoMarcaID(productoMarcaID: $productoMarcaID)   {
                                iD 
                                productoID	
                                tipoNegocioID	
                                nombre	
                                abreviatura	
                                unidadID_com	
                                unidadID_gra
                                imagen
                                requiereReceta
                                productoMarcaID
                                precioRef_com 
                                precioRef_gra
                                categoria {
                                    iD 
                                    tipoNegocioID	
                                    categoriaID	
                                    nombre	
                                    abreviatura	
                                    orden	
                                    activo
                                }
                                unidadMedida {
                                    iD 
                                    unidadID 
                                    nombre 
                                    abreviatura 
                                    esUnidadGranel 
                                    esUndiadComercial 
                                    activo
                                }
                            }
                        }",
                    Variables = new { ProductoMarcaID = productoMarcaID }
                };

                try
                {

                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<List<Producto>>("productoMarcaID");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        #endregion

        #region Solicitud
        public async Task<IEnumerable<Solicitud>> GetAllSolicitudes()
        {
            List<Solicitud> lstEntidad = null;
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @" 
                        { 
                            solicitudes {
                                solicitudID solicitudCode usuarioID direccion latitud longitud distancia fecha activo fechaEnviado cotizado
                              }
                        }",
                };
                var response = await graphQLClient.PostAsync(query);
                var lists = response.GetDataFieldAs<List<Solicitud>>("solicitudes");
                if (lists.Any())
                {
                    lstEntidad = lists;
                }
            }
            return lstEntidad;
        }

        public async Task<IEnumerable<Solicitud>> GetAllSolicitudesByUsuario(int solicitudesusuario)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query solicitudesusuarioQuery($solicitudesusuario: Int!)  
                        { 
                            solicitudesusuario(solicitudesusuario: $solicitudesusuario)   {
                                solicitudID solicitudCode usuarioID direccion latitud longitud distancia fecha activo fechaEnviado cotizado

                            }
                        }",
                    Variables = new { Solicitudesusuario = solicitudesusuario }
                };

                try
                {

                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<List<Solicitud>>("solicitudesusuario");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<IEnumerable<Solicitud>> GetAllSolicitudesByUsuarioID(int solicitudesusuarioID, int solicitudid)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query solicitudesusuarioIDQuery($solicitudesusuarioID: Int!, $solicitudid: Int!)  
                        { 
                            solicitudesusuarioID(solicitudesusuarioID: $solicitudesusuarioID, solicitudid:$solicitudid)   {
                                solicitudID solicitudCode usuarioID direccion latitud longitud distancia fecha activo fechaEnviado cotizado
                                usuarios {
                                      usuarioID
                                      tipoUsuarioID
                                      usuario
                                      descripcion
                                      email
                                      telefono
                                      activo
                                      dataFarmaciasID
                                    }
                            }
                        }",
                    Variables = new { SolicitudesusuarioID = solicitudesusuarioID, Solicitudid = solicitudid }
                };

                try
                {

                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<List<Solicitud>>("solicitudesusuarioID");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public async Task<Solicitud> AddSolicitud(SolicitudInput solicitudToCreate)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"
                    mutation($solicitud: solicitudInput!){
                      createSolicitud(solicitud: $solicitud){
                        solicitudCode,
                        usuarioID,
                        direccion,
                        latitud,
                        longitud,
                        distancia,
                        fecha,
                        activo,
                        fechaEnviado,
                        cotizado
                      }
                    }",
                    Variables = new { solicitud = solicitudToCreate }
                };

                var response = await graphQLClient.PostAsync(query);
                return response.GetDataFieldAs<Solicitud>("createSolicitud");
            }
        }
        #endregion

        #region SolicitudProducto
        public async Task<IEnumerable<SolicitudProducto>> GetAllSolicitudesProducto()
        {
            List<SolicitudProducto> lstEntidad = null;
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @" 
                        { 
                            solicitudesproducto {
                                SolicitudProductoID
                                solicitudID
                                productoLiteID
                                productoID
                                tipoNegocioID
                                nombre
                                abreviatura
                                unidadID
                                unidadNombre
                                categoriaID
                                categoriaNombre
                                categoriaAbreviatura
                                cantidad
                                activo
                                imagen
                                precioUnitario
                                precioTotal
                                requiereReceta
                                solicitud{
    	                            solicitudID
                                    solicitudCode
                                    usuarioID
                                    direccion
                                    latitud
                                    longitud
                                    distancia
                                    fecha
                                    activo
                                    cotizado
                                }
                              }
                        }",
                };
                var response = await graphQLClient.PostAsync(query);
                var lists = response.GetDataFieldAs<List<SolicitudProducto>>("solicitudesproducto");
                if (lists.Any())
                {
                    lstEntidad = lists;
                }
            }
            return lstEntidad;
        }

        public async Task<IEnumerable<SolicitudProducto>> GetAllSolicitudesProductoByUsuario(int solicitudesproductousuario)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query solicitudesproductousuarioQuery($solicitudesproductousuario: Int!)  
                        { 
                            solicitudesproductousuario(solicitudesproductousuario: $solicitudesproductousuario)   {
                                SolicitudProductoID
                                solicitudID
                                productoLiteID
                                productoID
                                tipoNegocioID
                                nombre
                                abreviatura
                                unidadID
                                unidadNombre
                                categoriaID
                                categoriaNombre
                                categoriaAbreviatura
                                cantidad
                                activo
                                imagen
                                precioUnitario
                                precioTotal
                                requiereReceta
                                solicitud{
    	                            solicitudID
                                    solicitudCode
                                    usuarioID
                                    direccion
                                    latitud
                                    longitud
                                    distancia
                                    fecha
                                    activo
                                    cotizado
                                }
                            }
                        }",
                    Variables = new { Solicitudesproductousuario = solicitudesproductousuario }
                };

                try
                {

                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<List<SolicitudProducto>>("solicitudesproductousuario");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<IEnumerable<SolicitudProducto>> GetAllSolicitudesProductoByUsuarioID(int solicitudesproductousuarioID, int solicitudid)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query solicitudesproductousuarioIDQuery($solicitudesproductousuarioID: Int!, $solicitudid: Int!)  
                        { 
                            solicitudesproductousuarioID(solicitudesproductousuarioID: $solicitudesproductousuarioID, solicitudid:$solicitudid)   {
                                solicitudProductoID
                                solicitudID
                                productoLiteID
                                productoID
                                tipoNegocioID
                                nombre
                                abreviatura
                                unidadID
                                unidadNombre
                                categoriaID
                                categoriaNombre
                                categoriaAbreviatura
                                cantidad
                                activo
                                imagen
                                precioUnitario
                                precioTotal
                                requiereReceta
                                solicitud{
    	                            solicitudID
                                    solicitudCode
                                    usuarioID
                                    direccion
                                    latitud
                                    longitud
                                    distancia
                                    fecha
                                    activo
                                    cotizado
                                }
                            }
                        }",
                    Variables = new { SolicitudesproductousuarioID = solicitudesproductousuarioID, Solicitudid = solicitudid }
                };

                try
                {

                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<List<SolicitudProducto>>("solicitudesproductousuarioID");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public async Task<SolicitudProducto> AddSolicitudProducto(SolicitudProductoInput solicitudproductoToCreate)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"
                    mutation($solicitudProducto: solicitudProductoInput!){
                      createSolicitudProducto(solicitudproducto: $solicitudProducto){
                        solicitudID
                        productoLiteID
                        productoID
                        tipoNegocioID
                        nombre
                        abreviatura
                        unidadID
                        unidadNombre
                        categoriaID
                        categoriaNombre
                        categoriaAbreviatura
                        cantidad
                        activo
                        imagen
                        precioUnitario
                        precioTotal
                        requiereReceta
                      }
                    }",
                    Variables = new { solicitudProducto = solicitudproductoToCreate }
                };

                var response = await graphQLClient.PostAsync(query);
                return response.GetDataFieldAs<SolicitudProducto>("createSolicitudProducto");
            }
        }

        #endregion

        #region DataFarmacias
        public async Task<IEnumerable<DataFarmacias>> GetAllDataFarmacias()
        {
            List<DataFarmacias> lstEntidad = null;
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @" 
                        { 
                            datafarmacias {
                                dataFarmaciasID
                                ruc
                                razon_social
                                direccion
                                paisID
                                tipoUbigeoID
                                ubigeoID
                                departamento
                                provincia
                                distrito
                                latitud
                                longitud
                              }
                        }",
                };
                var response = await graphQLClient.PostAsync(query);
                var lists = response.GetDataFieldAs<List<DataFarmacias>>("datafarmacias");
                if (lists.Any())
                {
                    lstEntidad = lists;
                }
            }
            return lstEntidad;
        }
        #endregion

        #region SolicitudDataFarmacias
        public async Task<IEnumerable<SolicitudDataFarmacias>> GetAllSolicitudDataFarmaciasBySolicitudID(int solicitudid)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query solicitudDataFarmaciassolicitudidQuery($solicitudDataFarmaciassolicitudid: Int!)  
                        { 
                            solicitudDataFarmaciassolicitudid(solicitudDataFarmaciassolicitudid:$solicitudDataFarmaciassolicitudid)   {
                                solicitudDataFarmaciasID
                                solicitudID
                                usuarioID
                                activo
                                usuarios {
                                    usuarioID
                                    tipoUsuarioID
                                    usuario
                                    descripcion
                                    email
                                    telefono
                                    activo
                                    dataFarmaciasID
                                }
                                solicitud {
                                  solicitudID
                                  solicitudCode
                                  usuarioID
                                  direccion
                                  latitud
                                  longitud
                                  distancia
                                  fecha
                                  activo
                                    fechaEnviado
                                    cotizado
                                }
                            }
                        }",
                    Variables = new { SolicitudDataFarmaciassolicitudid = solicitudid }
                };

                try
                {

                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<List<SolicitudDataFarmacias>>("solicitudDataFarmaciassolicitudid");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<SolicitudDataFarmacias> AddSolicitudDataFarmacias(SolicitudDataFarmaciasInput solicitudDataFarmaciasToCreate)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"
                    mutation($solicitudDataFarmacias:solicitudDataFarmaciasInputType!){
                      createSolicitudDataFarmacias(solicituddatafarmacias:$solicitudDataFarmacias){
                        solicitudID
                        usuarioID
                        activo
                      }
                    }",
                    Variables = new { solicitudDataFarmacias = solicitudDataFarmaciasToCreate }
                };

                var response = await graphQLClient.PostAsync(query);
                return response.GetDataFieldAs<SolicitudDataFarmacias>("createSolicitudDataFarmacias");
            }
        }
        #endregion

        #region Cotizaciones

        public async Task<Cotizacion> UpdateCotizacion(Cotizacion cotizacionToUpdate)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"
                mutation($cotizacion: cotizacionUpInput!){
                  updateCotizacion(cotizacion: $cotizacion){
                        cotizacionID
                        estado
                  }
               }",
                    Variables = new { cotizacion = cotizacionToUpdate }
                };

                var response = await graphQLClient.PostAsync(query);
                return response.GetDataFieldAs<Cotizacion>("updateCotizacion");
            }
        }

        public async Task<IEnumerable<Cotizacion>> GetAllCotizaciones()
        {
            List<Cotizacion> lstEntidad = null;
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @" 
                        { 
                            cotizacion{
                                cotizacionID
                                usuarioID
                                solicitudID
                                activo
                                fecha
                                fechaGenerado
                                nombre
                                direccion
                                latitud
                                longitud
                                solicitud {
                                  solicitudID
                                  solicitudCode
                                  usuarioID
                                  direccion
                                  latitud
                                  longitud
                                  distancia
                                  fecha
                                  activo
                                  fechaEnviado
                                    cotizado
                                }
                                usuarios {
                                    usuarioID
                                    tipoUsuarioID
                                    usuario
                                    descripcion
                                    email
                                    telefono
                                    activo
                                    dataFarmaciasID
                                }
                              }
                        }",
                };
                var response = await graphQLClient.PostAsync(query);
                var lists = response.GetDataFieldAs<List<Cotizacion>>("cotizacion");
                if (lists.Any())
                {
                    lstEntidad = lists;
                }
            }
            return lstEntidad;
        }

        public async Task<IEnumerable<Cotizacion>> GetAllCotizacionBySolicitud(int cotizacionsolicitudID)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query cotizacionsolicitudIDQuery($cotizacionsolicitudID: Int)  
                        { 
                            cotizacionsolicitudID(cotizacionsolicitudID:$cotizacionsolicitudID)   {
                                cotizacionID
                                usuarioID
                                solicitudID
                                activo
                                fecha
                                fechaGenerado
                                nombre
                                direccion
                                latitud
                                longitud
                                solicitud {
                                  solicitudID
                                  solicitudCode
                                  usuarioID
                                  direccion
                                  latitud
                                  longitud
                                  distancia
                                  fecha
                                  activo
                                  fechaEnviado
                                    cotizado
                                }
                                usuarios {
                                    usuarioID
                                    tipoUsuarioID
                                    usuario
                                    descripcion
                                    email
                                    telefono
                                    activo
                                    dataFarmaciasID
                                }
                            }
                        }",
                    Variables = new { CotizacionsolicitudID = cotizacionsolicitudID }
                };
                try
                {
                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<List<Cotizacion>>("cotizacionsolicitudID");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }


        public async Task<Cotizacion> GetAllCotizacionById(int cotizacionID)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query cotizacionIDQuery($cotizacionID: Int)  
                        { 
                            cotizacionID(cotizacionID:$cotizacionID)   {
                                cotizacionID
                                usuarioID
                                solicitudID
                                activo
                                fecha
                                fechaGenerado
                                nombre
                                direccion
                                latitud
                                longitud
                                solicitud {
                                  solicitudID
                                  solicitudCode
                                  usuarioID
                                  direccion
                                  latitud
                                  longitud
                                  distancia
                                  fecha
                                  activo
                                  fechaEnviado
                                    cotizado
                                }
                                usuarios {
                                    usuarioID
                                    tipoUsuarioID
                                    usuario
                                    descripcion
                                    email
                                    telefono
                                    activo
                                    dataFarmaciasID
                                }
                            }
                        }",
                    Variables = new { CotizacionID = cotizacionID }
                };
                try
                {
                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<List<Cotizacion>>("cotizacionID");
                    return getdat.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<IEnumerable<Cotizacion>> GetAllCotizacionesByUsuarioID(int cotizacionusuarioID)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query cotizacionusuarioIDQuery($cotizacionusuarioID: Int!)  
                        { 
                            cotizacionusuarioID(cotizacionusuarioID:$cotizacionusuarioID)   {
                                cotizacionID
                                usuarioID
                                solicitudID
                                activo
                                fecha
                                fechaGenerado
                                nombre
                                direccion
                                latitud
                                longitud
                                solicitud {
                                  solicitudID
                                  solicitudCode
                                  usuarioID
                                  direccion
                                  latitud
                                  longitud
                                  distancia
                                  fecha
                                  activo
                                  fechaEnviado
                                    cotizado
                                }
                                usuarios {
                                    usuarioID
                                    tipoUsuarioID
                                    usuario
                                    descripcion
                                    email
                                    telefono
                                    activo
                                    dataFarmaciasID
                                }
                            }
                        }",
                    Variables = new { CotizacionusuarioID = cotizacionusuarioID }
                };
                try
                {
                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<List<Cotizacion>>("cotizacionusuarioID");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<IEnumerable<CotizacionProducto>> GetAllCotizacionProductosByCotizacionID(int cotizacionproductoFK)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query cotizacionproductoFKQuery($cotizacionproductoFK: Int!)  
                        { 
                            cotizacionproductoFK(cotizacionproductoFK:$cotizacionproductoFK)   {
                                cotizacionProductoID
                                cotizacionID
                                solicitudProductoID
                                precioUnitario
                                precioTotal
                                activo
                                cantidad
                                solicitudProducto {
                                  solicitudProductoID
                                  solicitudID
                                  productoLiteID
                                  productoID
                                  tipoNegocioID
                                  nombre
                                  abreviatura
                                  unidadID
                                  unidadNombre
                                  categoriaID
                                  categoriaNombre
                                  categoriaAbreviatura
                                  cantidad
                                  activo
                                  imagen
                                  requiereReceta
                                }
                                cotizacion {
                                  cotizacionID
                                  usuarioID
                                  solicitudID
                                  activo
                                  fecha
                                  fechaGenerado
                                nombre
                                direccion
                                latitud
                                longitud
                                    solicitud {
                                        solicitudID
                                        solicitudCode
                                        usuarioID
                                        direccion
                                        latitud
                                        longitud
                                        distancia
                                        fecha
                                        activo
                                        fechaEnviado
                                    cotizado
                                    }
                                    usuarios {
                                        usuarioID
                                        tipoUsuarioID
                                        usuario
                                        descripcion
                                        email
                                        telefono
                                        activo
                                        dataFarmaciasID
                                    }
                                }
                            }
                        }",
                    Variables = new { CotizacionproductoFK = cotizacionproductoFK }
                };
                try
                {
                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<List<CotizacionProducto>>("cotizacionproductoFK");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        #endregion

        #region Cotizaciones
        public async Task<IEnumerable<UsuarioDireccion>> GetAllUsuarioDireccion()
        {
            List<UsuarioDireccion> lstEntidad = null;
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @" 
                        { 
                        usuarioDireccion
                            {
                            usuarioDireccionID
                            usuarioID
                            nombre
                            direccion
                            latitud
                            longitud
                            activo
                            }
                        }",
                };
                var response = await graphQLClient.PostAsync(query);
                var lists = response.GetDataFieldAs<List<UsuarioDireccion>>("usuarioDireccion");
                if (lists.Any())
                {
                    lstEntidad = lists;
                }
            }
            return lstEntidad;
        }

        public async Task<IEnumerable<UsuarioDireccion>> GetAllUsuarioDireccionByUsuarioID(int usuarioDireccionUsuarioID)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query usuarioDireccionusuarioIDQuery($usuarioDireccionUsuarioID: Int!)  
                        { 
                            usuarioDireccionUsuarioID(usuarioDireccionUsuarioID:$usuarioDireccionUsuarioID)   {
                                usuarioDireccionID
                                usuarioID
                                nombre
                                direccion
                                latitud
                                longitud
                                activo
                            }
                        }",
                    Variables = new { UsuarioDireccionUsuarioID = usuarioDireccionUsuarioID }
                };
                try
                {
                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<List<UsuarioDireccion>>("usuarioDireccionUsuarioID");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<IEnumerable<UsuarioDireccion>> GetAllUsuarioDireccionEstablecimiento()
        {
            List<Usuarios> lstUsuarios = null;
            List<UsuarioDireccion> lstEntidad = new List<UsuarioDireccion>();
            UsuarioDireccion entidad = null;
            try
            {
                var usuarios = await GetUsuarios();
                if (usuarios.Any())
                {
                    lstUsuarios = usuarios.Where(x => x.TipoUsuarioID.Equals("2")).ToList();
                    if (lstUsuarios.Any())
                    {
                        foreach (var item in lstUsuarios)
                        {
                            var xingreso = await GetAllUsuarioDireccionByUsuarioID(item.UsuarioID);
                            if (xingreso != null)
                            {
                                entidad = (UsuarioDireccion)xingreso.FirstOrDefault();
                                lstEntidad.Add(entidad);
                            }
                            else
                            {
                                lstEntidad = new List<UsuarioDireccion>();
                            }
                        }
                    }
                    else
                    {
                        lstEntidad = new List<UsuarioDireccion>();
                    }
                }
                else
                {
                    lstEntidad = new List<UsuarioDireccion>();
                }
            }
            catch (Exception ex)
            {
                lstEntidad = new List<UsuarioDireccion>();
            }

            return lstEntidad;
        }

        public async Task<UsuarioDireccion> GetAllUsuarioDireccionByID(int usuarioDireccionID)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query usuarioDireccionIDQuery($usuarioDireccionID: Int!)  
                        { 
                            usuarioDireccionID(usuarioDireccionID:$usuarioDireccionID)   {
                                usuarioDireccionID
                                usuarioID
                                nombre
                                direccion
                                latitud
                                longitud
                                activo
                            }
                        }",
                    Variables = new { UsuarioDireccionID = usuarioDireccionID }
                };
                try
                {
                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<UsuarioDireccion>("usuarioDireccionID");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<UsuarioDireccion> AddUsuarioDireccion(UsuarioDireccionInput usuarioDireccionToCreate)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"
                        mutation($usuarioDireccion:usuarioDireccionInput!){
                          createUsuarioDireccion(usuarioDireccion:$usuarioDireccion){
                            usuarioID
                            nombre
                            direccion
                            latitud
                            longitud
                            activo
                          }
                        }",
                    Variables = new { usuarioDireccion = usuarioDireccionToCreate }
                };

                var response = await graphQLClient.PostAsync(query);
                return response.GetDataFieldAs<UsuarioDireccion>("createUsuarioDireccion");
            }
        }

        public async Task<UsuarioDireccion> UpdateUsuarioDireccion(UsuarioDireccion usuarioDireccionToUpdate)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"
                mutation($usuariodirnew: usuarioDireccionUpInput!){
                  updateUsuarioDireccion(usuariodirnew: $usuariodirnew){
                        usuarioID
                        nombre
                        direccion
                        latitud
                        longitud
                        activo
                  }
               }",
                    Variables = new { usuariodirnew = usuarioDireccionToUpdate }
                };

                var response = await graphQLClient.PostAsync(query);
                return response.GetDataFieldAs<UsuarioDireccion>("updateUsuarioDireccion");
            }
        }

        public async Task<IEnumerable<UsuarioIngreso>> GetAllUsuarioIngreso()
        {
            List<UsuarioIngreso> lstEntidad = null;
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @" 
                        { 
                            usuarioIngreso {
                                usuarioIngresoID
                                usuarioID
                                fechaRegistro
                                fechaUltimaActualizacion
                                disponible
                            }
                        }",
                };
                var response = await graphQLClient.PostAsync(query);
                var lists = response.GetDataFieldAs<List<UsuarioIngreso>>("usuarioIngreso");
                if (lists.Any())
                {
                    lstEntidad = lists;
                }
            }
            return lstEntidad;
        }


        public async Task<UsuarioIngreso> GetAllUsuarioIngresoById(int usuarioIngresoID)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query usuarioIngresoIDQuery($usuarioIngresoID: Int!)  
                        { 
                            usuarioIngresoID(usuarioIngresoID:$usuarioIngresoID)   {
                                usuarioIngresoID
                                usuarioID
                                fechaRegistro
                                fechaUltimaActualizacion
                                disponible
                            }
                        }",
                    Variables = new { UsuarioIngresoID = usuarioIngresoID }
                };
                try
                {
                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<UsuarioIngreso>("usuarioIngresoID");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<UsuarioIngreso> AddUsuarioIngreso(UsuarioIngresoInput usuarioIngresoToCreate)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"
                        mutation($usuarioIngreso:usuarioIngresoInput!){
                          createUsuarioIngreso(usuarioIngreso:$usuarioIngreso){
                            usuarioID
                            fechaRegistro
                            fechaUltimaActualizacion
                            disponible
                          }
                        }",
                    Variables = new { usuarioIngreso = usuarioIngresoToCreate }
                };

                var response = await graphQLClient.PostAsync(query);
                return response.GetDataFieldAs<UsuarioIngreso>("createUsuarioDireccion");
            }
        }

        public async Task<UsuarioIngreso> UpdateUsuarioIngreso(UsuarioIngreso usuarioDireccionToUpdate)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"
                mutation($usuarioIngresonew: usuarioIngresoUpInput!){
                  updateUsuarioIngreso(usuarioIngresonew: $usuarioIngresonew){
                        usuarioIngresoID
                        usuarioID
                        fechaRegistro
                        fechaUltimaActualizacion
                        disponible
                  }
               }",
                    Variables = new { usuarioIngresonew = usuarioDireccionToUpdate }
                };

                var response = await graphQLClient.PostAsync(query);
                return response.GetDataFieldAs<UsuarioIngreso>("updateUsuarioIngreso");
            }
        }


        #endregion

        public async Task<IEnumerable<Parametro>> GetAllParametros()
        {
            List<Parametro> lstEntidad = null;
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @" 
                        { 
                            parametros {
                                parametroID
                                nombre
                                valor
                                activo
                              }
                        }",
                };
                var response = await graphQLClient.PostAsync(query);
                var lists = response.GetDataFieldAs<List<Parametro>>("parametros");
                if (lists.Any())
                {
                    lstEntidad = lists;
                }
            }
            return lstEntidad;
        }


        public async Task<IEnumerable<Pedido>> GetAllPedidos()
        {
            List<Pedido> lstEntidad = null;
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @" 
                        { 
                            pedidos {
                                pedidoID
                                numero
                                cotizacionID
                                usuarioID
                                solicitudID
                                activo
                                fechaGenerado
                                nombre
                                direccion
                                latitud
                                longitud
                                estado
                                nombreEntrega
                                direccionEntrega
                                latitudEntrega
                                longitudEntrega
                                totalPagar
                                tipoPagar
                                montoPagar
                                usuarioClienteID
                              }
                        }",
                };
                var response = await graphQLClient.PostAsync(query);
                var lists = response.GetDataFieldAs<List<Pedido>>("pedidos");
                if (lists.Any())
                {
                    lstEntidad = lists;
                }
            }
            return lstEntidad;
        }
        public async Task<IEnumerable<Pedido>> GetAllPedidosByPedido(int pedidoID)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query pedidosIDQuery($pedidosID: Int!)  
                        { 
                            pedidosID(pedidosID:$pedidosID)   {
                                pedidoID
                                numero
                                cotizacionID
                                usuarioID
                                solicitudID
                                activo
                                fechaGenerado
                                nombre
                                direccion
                                latitud
                                longitud
                                estado
                                nombreEntrega
                                direccionEntrega
                                latitudEntrega
                                longitudEntrega
                                totalPagar
                                tipoPagar
                                montoPagar
                                usuarioClienteID
                            }
                        }",
                    Variables = new { pedidosID = pedidoID }
                };
                try
                {
                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<List<Pedido>>("pedidosID");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public async Task<IEnumerable<Pedido>> GetAllPedidosBySolicitud(int pedidosolicitudID)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query pedidosolicitudIDQuery($pedidosolicitudID: Int!)  
                        { 
                            pedidosolicitudID(pedidosolicitudID:$pedidosolicitudID)   {
                                pedidoID
                                numero
                                cotizacionID
                                usuarioID
                                solicitudID
                                activo
                                fechaGenerado
                                nombre
                                direccion
                                latitud
                                longitud
                                estado
                                nombreEntrega
                                direccionEntrega
                                latitudEntrega
                                longitudEntrega
                                totalPagar
                                tipoPagar
                                montoPagar
                                usuarioClienteID
                            }
                        }",
                    Variables = new { pedidosolicitudID = pedidosolicitudID }
                };
                try
                {
                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<List<Pedido>>("pedidosolicitudID");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<Pedido> AddPedido(PedidoInput pedidoToCreate)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"
                        mutation($pedido:pedidoInput!){
                          createPedido(pedido:$pedido){
                            numero
                                cotizacionID
                                usuarioID
                                solicitudID
                                activo
                                fechaGenerado
                                nombre
                                direccion
                                latitud
                                longitud
                                estado
                                nombreEntrega
                                direccionEntrega
                                latitudEntrega
                                longitudEntrega
                                totalPagar
                                tipoPagar
                                montoPagar
                          }
                        }",
                    Variables = new { pedido = pedidoToCreate }
                };

                var response = await graphQLClient.PostAsync(query);
                return response.GetDataFieldAs<Pedido>("createPedido");
            }
        }


        public async Task<IEnumerable<PedidoProducto>> GetAllPedidoProductoByPedido(int pedidoproductopedidoID)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query pedidoproductopedidoIDQuery($pedidoproductopedidoID: Int!)  
                        { 
                            pedidoproductopedidoID(pedidoproductopedidoID:$pedidoproductopedidoID)   {
                                pedidoProductoID
                                pedidoID
                                cotizacionProductoID
                                cotizacionID
                                solicitudProductoID
                                activo
                                productoID
                                tipoNegocioID
                                nombre
                                abreviatura
                                unidadId
                                unidadNombre
                                categoriaID
                                categoriaNombre
                                categoriaAbreviatura
                                cantidad
                                imagen
                                requiereReceta
                                precioUnitario
                                precioTotal
                            }
                        }",
                    Variables = new { pedidoproductopedidoID = pedidoproductopedidoID }
                };
                try
                {
                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<List<PedidoProducto>>("pedidoproductopedidoID");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<PedidoProducto> AddPedidoProducto(PedidoProductoInput pedidoProductoToCreate)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"
                        mutation($pedidoProducto:pedidoProductoInput!){
                          createPedidoProducto(pedidoProducto:$pedidoProducto){
                            pedidoID
                                cotizacionProductoID
                                cotizacionID
                                solicitudProductoID
                                activo
                                productoID
                                tipoNegocioID
                                nombre
                                abreviatura
                                unidadId
                                unidadNombre
                                categoriaID
                                categoriaNombre
                                categoriaAbreviatura
                                cantidad
                                imagen
                                requiereReceta
                                precioUnitario
                                precioTotal
                          }
                        }",
                    Variables = new { pedidoProducto = pedidoProductoToCreate }
                };

                var response = await graphQLClient.PostAsync(query);
                return response.GetDataFieldAs<PedidoProducto>("createPedidoProducto");
            }
        }

        public async Task<IEnumerable<PedidoLineaTiempo>> GetAllPedidoLineaTiempoByPedido(int pedidoLineaTiemposID)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query pedidoLineaTiemposIDQuery($pedidoLineaTiemposID: Int!)  
                        { 
                            pedidoLineaTiemposID(pedidoLineaTiemposID:$pedidoLineaTiemposID)   {
                                pedidoLineaTiempoID
                                pedidoID
                                estadoPedidoID
                                descripcion
                                fecha
                                fechaTexto
                                numero
                                usuarioClienteID
                            }
                        }",
                    Variables = new { pedidoLineaTiemposID = pedidoLineaTiemposID }
                };
                try
                {
                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<List<PedidoLineaTiempo>>("pedidoLineaTiemposID");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<IEnumerable<Notificacion>> GetAllNotificacionByUsuario(int notificacionusuarioID)
        {
            using (GraphQLClient graphQLClient = new GraphQLClient(urlgraphql))
            {
                var query = new GraphQLRequest
                {
                    Query = @"   
                        query notificacionusuarioIDQuery($notificacionusuarioID: Int!)  
                        { 
                            notificacionusuarioID(notificacionusuarioID:$notificacionusuarioID)   {
                                notificacionID
                                usuarioID
                                hora
                                fecha
                                descripcion
                                accion
                            }
                        }",
                    Variables = new { notificacionusuarioID = notificacionusuarioID }
                };
                try
                {
                    var response = await graphQLClient.PostAsync(query);
                    if (response.Errors != null && response.Errors.Any())
                    {
                        throw new ApplicationException(response.Errors[0].Message);
                    }
                    var getdat = response.GetDataFieldAs<List<Notificacion>>("notificacionusuarioID");
                    return getdat;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }


        #region Nodejs
        //string urlnodejs = "http://190.117.184.215:5300";
        string urlnodejs = "http://192.168.1.100:5300";


        public async Task<Usuarios> ValidaLogueo(Usuarios usuario)
        {
            Usuarios entidadfound = new Usuarios();
            try
            {
                //Usuarios entidad = new Usuarios { Usuario = usuario.Usuario, Token = usuario.Token };
                HttpClient client = new HttpClient();
                var connectionInfo = urlnodejs;
                client.BaseAddress = new Uri(connectionInfo);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string serializedObject = JsonConvert.SerializeObject(usuario);
                HttpContent contentPost = new StringContent(serializedObject, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Usuario/login", contentPost);
                if (response.IsSuccessStatusCode && response.RequestMessage != null)
                {
                    JArray usuarioe = JArray.Parse(await response.Content.ReadAsStringAsync());
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    settings.MissingMemberHandling = MissingMemberHandling.Ignore;
                    if (usuarioe != null && usuarioe.Count > 0)
                    {
                        var _lusuario = JsonConvert.DeserializeObject<List<Usuarios>>(usuarioe.ToString(), settings);
                        if (_lusuario.Any())
                        {
                            entidadfound = _lusuario.FirstOrDefault();
                        }
                    }
                }
                return entidadfound;
            }
            catch (Exception ex)
            {
                return entidadfound;
            }
        }

        #endregion
    }
}