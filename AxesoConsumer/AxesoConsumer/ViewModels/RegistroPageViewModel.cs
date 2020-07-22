using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Helpers;
using AxesoConsumer.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace AxesoConsumer.ViewModels
{
    [Preserve]
    public class RegistroPageViewModel : INotifyPropertyChanged
    {
        private ModelsBL usuarioBL = new ModelsBL();
        public event PropertyChangedEventHandler PropertyChanged;
        public string emailregistro;
        public DateTime? fechanacimientoregistro;
        public string nameregistro;
        public string passwordregistro;
        public string repasswordregistro;
        string claveEncrip = "";
        int contador = 0;

        #region Commands
        public ICommand AgregarRegistro { get; }
        #endregion
        public RegistroPageViewModel()
        {
            EmailRegistro = "";
            FechaNacimientoRegistro = DateTime.Now;
            NameRegistro = "";
            PasswordRegistro = "";
            RePasswordRegistro = "";
            AgregarRegistro = new Command(AgregarRegistroMehod);
        }

        private async void AgregarRegistroMehod(object obj)
        {
            var loadingPage = new LoadingPopupPage();
            try
            {
                if (this.FechaNacimientoRegistro == null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        "Ingrese una fecha válida",
                        Languages.Accept);
                    return;
                }
                else
                {
                    DateTime fechaselected = Convert.ToDateTime(this.FechaNacimientoRegistro);
                    int aniosdiff = GetDifferenceInYears(fechaselected);
                    if (aniosdiff < 18)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        "Debe ser mayor de edad para registrarse",
                        Languages.Accept);
                        return;
                    }
                }
                if (string.IsNullOrEmpty(this.EmailRegistro))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.EmailValidation,
                        Languages.Accept);
                    return;
                }
                if (!(this.EmailRegistro).Contains("@"))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.EmailValidation,
                        Languages.Accept);
                    return;
                }
                if (string.IsNullOrEmpty(this.NameRegistro))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.NameValidation,
                        Languages.Accept);
                    return;
                }


                if (string.IsNullOrEmpty(this.PasswordRegistro))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.PasswordValidation,
                        Languages.Accept);
                    return;
                }

                if (string.IsNullOrEmpty(this.RePasswordRegistro))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.PhoneValidation,
                        Languages.Accept);
                    return;
                }

                //if (!(this.IsAgree))
                //{
                //    await Application.Current.MainPage.DisplayAlert(
                //        Languages.Error,
                //        Languages.AgreeValidation,
                //        Languages.Accept);
                //    return;
                //}
                var current = Connectivity.NetworkAccess;
                if (current != NetworkAccess.Internet)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.TurnOnInternet,
                        Languages.Accept);
                    return;
                }

                //this.IsRunning = true;
                //this.IsEnabled = false;
                
                await PopupNavigation.Instance.PushAsync(loadingPage);

                var tokenmodel = usuarioBL.EncriptaClave(this.PasswordRegistro);
                if (tokenmodel.TokenEncrypted.Equals(""))
                {
                }
                else
                {
                    claveEncrip = tokenmodel.TokenEncrypted.ToString();
                }

                var existe = await CheckIfExistUser(this.EmailRegistro);
                if (existe == null)
                {
                    int idgenerado = contador + 1;
                    UsuarioInput entidadinsert = new UsuarioInput()
                    {
                        UsuarioID = idgenerado,
                        TipoUsuarioID = "1",
                        Usuario = this.EmailRegistro,
                        Descripcion = this.NameRegistro.ToUpper(),
                        Email = this.EmailRegistro,
                        Telefono = "",
                        Activo = true,
                        Bloqueado = false,
                        Token = claveEncrip,
                        FchHraCreacion = DateTime.Now,
                        FchHraActualizacion = DateTime.Now,
                        FchHraBloqueo = DateTime.Now,
                        FchHraDesbloqueo = DateTime.Now,
                        DataFarmaciasID = 0,
                        FechaNacimiento = Convert.ToDateTime(this.FechaNacimientoRegistro)
                    };
                    var registro = await usuarioBL.CreateUsuario(entidadinsert);
                    if (registro == null)
                    {
                        await PopupNavigation.Instance.RemovePageAsync(loadingPage);
                        //this.IsRunning = false;
                        //this.IsEnabled = true;
                        await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.SomethingWrong,
                        Languages.Accept);
                        return;
                    }
                    else
                    {
                        await PopupNavigation.Instance.RemovePageAsync(loadingPage);
                        //this.IsRunning = false;
                        //this.IsEnabled = true;
                        await Application.Current.MainPage.DisplayAlert(
                        Languages.Success,
                        Languages.SuccessRegister,
                        Languages.Accept);

                        //App.Current.MainPage = new LoginPage();
                        await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new LoginPage());

                    }
                }
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.RemovePageAsync(loadingPage);
                Debug.WriteLine(ex.Message);
            }
        }

        private int GetDifferenceInYears(DateTime fechaselected)
        {
            int finalResult = 0;

            const int DaysInYear = 365;

            DateTime endDate = DateTime.Now;

            TimeSpan timeSpan = endDate - fechaselected;

            if (timeSpan.TotalDays > 365)
            {
                finalResult = (int)Math.Round((timeSpan.TotalDays / DaysInYear), MidpointRounding.ToEven);
            }

            return finalResult;
        }

        private async Task<Usuarios> CheckIfExistUser(string emailaddress)
        {

            try
            {
                var usuarios = await usuarioBL.Listar();
                if (usuarios.Any())
                {
                    contador = usuarios.Select(x => x.UsuarioID).LastOrDefault();
                    var existe = usuarios.Where(x => x.Usuario.ToUpper().Equals(emailaddress.ToUpper())).FirstOrDefault();
                    return existe;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DateTime? FechaNacimientoRegistro
        {
            get
            {
                return this.fechanacimientoregistro;
            }
            set
            {
                if (this.fechanacimientoregistro != value)
                {
                    fechanacimientoregistro = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(FechaNacimientoRegistro)));
                }
            }
        }
        public string EmailRegistro
        {
            get
            {
                if (string.IsNullOrEmpty(emailregistro))
                    return "";
                else
                    return emailregistro;
            }
            set
            {
                if (emailregistro != value)
                {
                    emailregistro = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(EmailRegistro)));
                }
            }
        }
        public string NameRegistro
        {
            get
            {
                if (string.IsNullOrEmpty(nameregistro))
                    return "";
                else
                    return nameregistro;
            }
            set
            {
                if (nameregistro != value)
                {
                    nameregistro = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(NameRegistro)));
                }
            }
        }
        public string PasswordRegistro
        {
            get
            {
                if (string.IsNullOrEmpty(passwordregistro))
                    return "";
                else
                    return passwordregistro;
            }
            set
            {
                if (passwordregistro != value)
                {
                    passwordregistro = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(PasswordRegistro)));
                }
            }
        }
        public string RePasswordRegistro
        {
            get
            {
                if (string.IsNullOrEmpty(repasswordregistro))
                    return "";
                else
                    return repasswordregistro;
            }
            set
            {
                if (repasswordregistro != value)
                {
                    repasswordregistro = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(RePasswordRegistro)));
                }
            }
        }
    }
}
