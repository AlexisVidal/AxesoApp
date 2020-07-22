using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Helpers;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AxesoConsumer.Services
{
    public class ApiService
    {
        private ModelsBL usuarioBL = new ModelsBL();
        Usuarios usuariom = null;
        UsuarioInput usuarioi = null;
        List<Usuarios> lusuario = null;
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Please turn on your internet settings.",
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable(
                "google.com", 500);
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Check you internet connection.",
                };
            }

            return new Response
            {
                IsSuccess = true,
                Message = "Ok",
            };
        }

        public async Task<FacebookResponse> GetFacebook(string accessToken)
        {
            var requestUrl = "https://graph.facebook.com/v2.8/me/?fields=name," +
                "picture.width(999),cover,age_range,devices,email,gender," +
                "is_verified,birthday,languages,work,website,religion," +
                "location,locale,link,first_name,last_name," +
                "hometown&access_token=" + accessToken;
            var httpClient = new HttpClient();
            var userJson = await httpClient.GetStringAsync(requestUrl);
            var facebookResponse = JsonConvert.DeserializeObject<FacebookResponse>(userJson);
            return facebookResponse;
        }
        internal async Task<bool> CambiaClave(int id, string email, string newPassword)
        {
            string newpass = "";
            if (String.IsNullOrEmpty(newPassword))
            {
                newpass = Constants.GeneraPass();
            }
            else
            {
                newpass = newPassword;
            }

            var subject = Resources.Resource.AppName + " - " + Resources.Resource.PasswordRecovery;
            string yournewp = Resources.Resource.PasswordYourNew;
            string dontforg = Resources.Resource.PasswordDontForget;
            var body = string.Format(@"
                        <h1>{0}</h1>
                        <p>{1} <strong>{2}</strong></p>
                        <p>{3}", subject, yournewp, newpass, dontforg);
            bool resultado = false;
            try
            {
                string claveencrip = Constants.EncriptaClave(newpass);
                usuarioi = new UsuarioInput()
                {
                    UsuarioID = id,
                    Token = claveencrip
                };

                var actualiza = await usuarioBL.RecoverPassUsuario(id, usuarioi);
                if (actualiza != null)
                {
                    await MailHelper.SendMail(email, subject, body);
                }
                resultado = true;
            }
            catch (Exception ex)
            {
                resultado = false;
            }
            return await Task.FromResult(resultado);
        }

    }
}
