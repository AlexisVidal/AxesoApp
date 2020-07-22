using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AxesoConsumer.Helpers
{
    public static class CommonFunctions
    {
        static ModelsBL usuarioBL = new ModelsBL();

        public static HtmlWebViewSource GetLoaderSource()
        {
            var loaderSource = new HtmlWebViewSource
            {
                Html = @"<html><body bgcolor='#87ceeb'style='margin: 0; padding: 0'><img src='loading.gif'style='width: 60px; height:60px; ' /></body></html>",

                BaseUrl = DependencyService.Get<IGif>().GetGifImageUrl()
            };

            return loaderSource;
        }

        public static HtmlWebViewSource GetLoaderSourceForBlurEffect()
        {
            var loaderSource = new HtmlWebViewSource
            {
                Html = @"<html><body bgcolor='#000000'style='margin: 0; padding: 0'><img src='loading.gif'style='width: 60px; height:60px; ' /></body></html>",

                BaseUrl = DependencyService.Get<IGif>().GetGifImageUrl()
            };

            return loaderSource;
        }

        internal static UsuarioIngreso GetSetIngreso(int usuarioID)
        {
            UsuarioIngreso newusuarioingreso = new UsuarioIngreso();
            UsuarioIngresoInput newusuarioingresoInput = new UsuarioIngresoInput();
            try
            {
                var existe = Task.Run(async () => await usuarioBL.GetAllUsuarioIngresoById(usuarioID)).Result;
                if (existe != null)
                {
                    newusuarioingreso = new UsuarioIngreso()
                    {
                        UsuarioIngresoID = existe.UsuarioIngresoID,
                        UsuarioID = usuarioID,
                        FechaRegistro = DateTime.Now,
                        FechaUltimaActualizacion = DateTime.Now,
                        Disponible = true
                    };
                    var actualiza = Task.Run(async () => await usuarioBL.UpdateUsuarioIngreso(newusuarioingreso)).Result;
                    return actualiza;
                }
                else
                {
                    newusuarioingresoInput = new UsuarioIngresoInput()
                    {
                        UsuarioID = usuarioID,
                        FechaRegistro = DateTime.Now,
                        FechaUltimaActualizacion = DateTime.Now,
                        Disponible = true
                    };
                    var inserta = Task.Run(async () => await usuarioBL.AddUsuarioIngreso(newusuarioingresoInput)).Result;
                    return inserta;
                }
            }
            catch (Exception ex)
            {
                return newusuarioingreso;
            }
        }

        internal static List<UsuarioDireccion> GetFarmaciasOnline()
        {
            List<UsuarioDireccion> lfarmaciasonline = new List<UsuarioDireccion>();
            try
            {
                var listado = Task.Run(async () => await usuarioBL.GetAllUsuarioDireccionEstablecimiento()).Result;
                if (listado.Any())
                {
                    lfarmaciasonline = (List<UsuarioDireccion>)listado;
                }
                return lfarmaciasonline;
            }
            catch (Exception ex)
            {
                return lfarmaciasonline;
            }
        }

        internal static List<Parametro> GetParametros()
        {
            List<Parametro> lexist = new List<Parametro>();
            try
            {
                var existe = Task.Run(async () => await usuarioBL.GetAllParametros()).Result;
                if (existe != null)
                {
                    lexist = (List<Parametro>)existe;
                }
                else
                {
                    lexist = new List<Parametro>();
                }
                return lexist;
            }
            catch (Exception ex )
            {

                return lexist;
            }
        }
    }
}
