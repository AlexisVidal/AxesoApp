using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Helpers;
using AxesoConsumer.Models;
using AxesoConsumer.Services;
using AxesoConsumer.Views;
using Newtonsoft.Json;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;



namespace AxesoConsumer.ViewModels
{
    [Preserve]
    public class LoginPageViewModel
    // : INotifyPropertyChanged
    {
        private ModelsBL usuarioBL = new ModelsBL();
        UsuarioIngreso ingresoactual = new UsuarioIngreso();
        Usuarios entidad = new Usuarios();
        //public bool isRunning;
        public bool isEnabled;
        public bool isRemembered;
        public event PropertyChangedEventHandler PropertyChanged;
        ApiService apiService;
        string claveEncrip = "";
        IFacebookClient _facebookService = CrossFacebookClient.Current;
        Account account;
        AccountStore store;

        #region Commands
        public ICommand Login { get; }
        public ICommand LoginGoogle { get; }

        public ICommand FACEBOOK { get; }
        public ICommand Register { get; }

        private readonly IGoogleClientManager _googleClientManager;
        public bool IsLoggedIn { get; set; }
        #endregion

        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }
        public LoginPageViewModel()
        {
            try
            {
                apiService = new ApiService();
                IsEnabled = true;
                //IsRunning = false;

                Login = new Command(LoginConsumer);
                FACEBOOK = new Command(LoginFacebook);
                Register = new Command(RegisterConsumer);
                LoginGoogle = new Command(LoginGoogleConsumer);
                store = AccountStore.Create();
                _googleClientManager = CrossGoogleClient.Current;
                IsLoggedIn = false;
            }
            catch (Exception ex)
            {

            }
        }

        private async void LoginGoogleConsumer(object obj)
        {

            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

            _googleClientManager.OnLogin += OnLoginCompleted;
            try
            {
                await _googleClientManager.LoginAsync();
            }
            catch (GoogleClientSignInNetworkErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientSignInCanceledErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientSignInInvalidAccountErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientSignInInternalErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientNotInitializedErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientBaseException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }


        }

        async void OnLoginCompleted(object sender, GoogleClientResultEventArgs<Plugin.GoogleClient.Shared.GoogleUser> loginEventArgs)
        {
            string email = "";
            string name = "";
            int idusuario = 0;

            if (loginEventArgs.Data != null)
            {
                GoogleUser googleUser = loginEventArgs.Data;
                //User.Name = googleUser.Name;
                //User.Email = googleUser.Email;
                //User.Picture = googleUser.Picture;
                //var GivenName = googleUser.GivenName;
                //var FamilyName = googleUser.FamilyName;

                var loadingPage = new LoadingPopupPage();
                await PopupNavigation.Instance.PushAsync(loadingPage);
                if (account != null)
                {
                    store.Delete(account, Constants.AppName);
                }
                Account newaccount = new Account()
                {
                    Username = googleUser.Email
                };
                await store.SaveAsync(account = newaccount, Constants.AppName);
                email = googleUser.Email;
                name = googleUser.Name;
                var userexist = await CheckIfExistUser(email);
                int idgenerado = 0;
                if (userexist == null)
                {
                    var todos = await usuarioBL.Listar();
                    //int contador = todos.Count();
                    int contador = todos.Select(x => x.UsuarioID).LastOrDefault();
                    idgenerado = contador + 1;
                    UsuarioInput entidadinsert = new UsuarioInput()
                    {
                        UsuarioID = idgenerado,
                        TipoUsuarioID = "1",
                        Usuario = email,
                        Descripcion = googleUser.Name,
                        Email = email,
                        Telefono = "",
                        Activo = true,
                        Bloqueado = false,
                        Token = "",
                        FchHraCreacion = DateTime.Now,
                        FchHraActualizacion = DateTime.Now,
                        FchHraBloqueo = DateTime.Now,
                        FchHraDesbloqueo = DateTime.Now,
                        DataFarmaciasID = 0,
                        FechaNacimiento = DateTime.Now
                    };
                    var registro = await usuarioBL.CreateUsuario(entidadinsert);
                    if (registro != null)
                    {
                        idusuario = registro.UsuarioID;
                        entidad = new Usuarios()
                        {
                            UsuarioID = registro.UsuarioID,
                            TipoUsuarioID = registro.TipoUsuarioID,
                            Usuario = registro.Email,
                            Descripcion = registro.Descripcion,
                            Email = registro.Email,
                            Telefono = registro.Token,
                            Activo = registro.Activo,
                            Bloqueado = registro.Bloqueado,
                            Token = "",
                            FchHraCreacion = registro.FchHraCreacion,
                            FchHraActualizacion = registro.FchHraActualizacion,
                            FchHraBloqueo = registro.FchHraBloqueo,
                            FchHraDesbloqueo = registro.FchHraDesbloqueo,
                            DataFarmaciasID = registro.DataFarmaciasID,
                            FechaNacimiento = registro.FechaNacimiento
                        };
                    }
                    var mainViewModel = MenuTabbedPageViewModel.GetInstance();
                    mainViewModel.Usuario = entidad;
                    TokenResponse newtoken = new TokenResponse()
                    {
                        AccessToken = "",
                        UserName = entidad.Descripcion,
                        Password = "",
                        IsRemembered = true,
                        Expires = DateTime.Now.AddDays(1)
                    };
                    Settings.UserEmail = email;
                    Settings.UserName = name;
                    Settings.UserID = idusuario;
                    Settings.IsRemember = true;
                    Settings.Expires = DateTime.Now.AddDays(1);
                    Settings.UserPassword = "";
                    Settings.Token = JsonConvert.SerializeObject(newtoken);
                    ingresoactual = await Task.Run(() => CommonFunctions.GetSetIngreso(idusuario));
                    await PopupNavigation.RemovePageAsync(loadingPage);
                    App.Current.MainPage = new MenuTabbedPage();
                    IsLoggedIn = true;

                    //var token = CrossGoogleClient.Current.ActiveToken;
                    //Token = token;
                }
                else
                {
                    var mainViewModel = MenuTabbedPageViewModel.GetInstance();
                    mainViewModel.Usuario = userexist;
                    TokenResponse newtoken = new TokenResponse()
                    {
                        AccessToken = "",
                        UserName = name,
                        Password = "",
                        IsRemembered = true,
                        Expires = DateTime.Now.AddDays(1)
                    };
                    Settings.UserEmail = email;
                    Settings.UserName = name;
                    Settings.UserID = userexist.UsuarioID;
                    Settings.IsRemember = true;
                    Settings.Expires = DateTime.Now.AddDays(1);
                    Settings.UserPassword = "";
                    Settings.Token = JsonConvert.SerializeObject(newtoken);

                    ingresoactual = await Task.Run(() => CommonFunctions.GetSetIngreso(userexist.UsuarioID));

                    //await Application.Current.MainPage.Navigation.PushAsync(new MenuTabbedPage());
                    App.Current.MainPage = new MenuTabbedPage();
                    await PopupNavigation.Instance.RemovePageAsync(loadingPage);
                }


            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", loginEventArgs.Message, "OK");
            }
            _googleClientManager.OnLogin -= OnLoginCompleted;
        }

        //private async void LoginGoogleConsumer(object obj)
        //{
        //    string email = "";
        //    string name = "";
        //    int idusuario = 0;
        //    account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();
        //    var googleuser = await CrossGoogleClient.Current.LoginAsync();
        //    if (googleuser.Status == GoogleActionStatus.Completed)
        //    {
        //        var loadingPage = new LoadingPopupPage();
        //        await PopupNavigation.PushAsync(loadingPage);
        //        if (account != null)
        //        {
        //            store.Delete(account, Constants.AppName);
        //        }
        //        Account newaccount = new Account()
        //        {
        //            Username = googleuser.Data.Email
        //        };
        //        await store.SaveAsync(account = newaccount, Constants.AppName);
        //        email = googleuser.Data.Email;
        //        name = googleuser.Data.Name;
        //        var userexist = await CheckIfExistUser(email);
        //        int idgenerado = 0;
        //        if (userexist == null)
        //        {
        //            var todos = await usuarioBL.Listar();
        //            //int contador = todos.Count();
        //            int contador = todos.Select(x=>x.UsuarioID).LastOrDefault();
        //            idgenerado = contador + 1;
        //            UsuarioInput entidadinsert = new UsuarioInput()
        //            {
        //                UsuarioID = idgenerado,
        //                TipoUsuarioID = "1",
        //                Usuario = email,
        //                Descripcion = googleuser.Data.Name,
        //                Email = email,
        //                Telefono = "",
        //                Activo = true,
        //                Bloqueado = false,
        //                Token = "",
        //                FchHraCreacion = DateTime.Now,
        //                FchHraActualizacion = DateTime.Now,
        //                FchHraBloqueo = DateTime.Now,
        //                FchHraDesbloqueo = DateTime.Now
        //            };
        //            var registro = await usuarioBL.CreateUsuario(entidadinsert);
        //            if (registro != null)
        //            {
        //                idusuario = registro.UsuarioID;
        //                entidad = new UsuarioModel()
        //                {
        //                    UsuarioID = registro.UsuarioID,
        //                    TipoUsuarioID = registro.TipoUsuarioID,
        //                    Usuario = registro.Email,
        //                    Descripcion = registro.Descripcion,
        //                    Email = registro.Email,
        //                    Telefono = registro.Token,
        //                    Activo = registro.Activo,
        //                    Bloqueado = registro.Bloqueado,
        //                    Token = "",
        //                    FchHraCreacion = registro.FchHraCreacion,
        //                    FchHraActualizacion = registro.FchHraActualizacion,
        //                    FchHraBloqueo = registro.FchHraBloqueo,
        //                    FchHraDesbloqueo = registro.FchHraDesbloqueo
        //                };
        //            }
        //            var mainViewModel = MenuTabbedPageViewModel.GetInstance();
        //            mainViewModel.Usuario = entidad;
        //            TokenResponse newtoken = new TokenResponse()
        //            {
        //                AccessToken = "",
        //                UserName = entidad.Descripcion,
        //                Password = "",
        //                IsRemembered = true,
        //                Expires = DateTime.Now.AddDays(1)
        //            };
        //            Settings.UserEmail = email;
        //            Settings.UserName = name;
        //            Settings.UserID = idusuario;
        //            Settings.IsRemember = true;
        //            Settings.Expires = DateTime.Now.AddDays(1);
        //            Settings.UserPassword = "";
        //            Settings.Token = JsonConvert.SerializeObject(newtoken);

        //            await PopupNavigation.RemovePageAsync(loadingPage);
        //            App.Current.MainPage = new MenuTabbedPage();
        //            //MainPage = new MenuTabbedPage();
        //            //await Application.Current.MainPage.Navigation.PushAsync(new MenuTabbedPage());
        //        }
        //        else
        //        {
        //            var mainViewModel = MenuTabbedPageViewModel.GetInstance();
        //            mainViewModel.Usuario = userexist;
        //            TokenResponse newtoken = new TokenResponse()
        //            {
        //                AccessToken = "",
        //                UserName = name,
        //                Password = "",
        //                IsRemembered = true,
        //                Expires = DateTime.Now.AddDays(1)
        //            };
        //            Settings.UserEmail = email;
        //            Settings.UserName = name;
        //            Settings.UserID = userexist.UsuarioID;
        //            Settings.IsRemember = true;
        //            Settings.Expires = DateTime.Now.AddDays(1);
        //            Settings.UserPassword = "";
        //            Settings.Token = JsonConvert.SerializeObject(newtoken);

        //            //await Application.Current.MainPage.Navigation.PushAsync(new MenuTabbedPage());
        //            App.Current.MainPage = new MenuTabbedPage();
        //            await PopupNavigation.RemovePageAsync(loadingPage);
        //        }
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}

        private async void RegisterConsumer(object obj)
        {
            //await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new RegistroPage());
        }

        private async void LoginFacebook(object obj)
        {
            try
            {

                if (_facebookService.IsLoggedIn)
                {
                    _facebookService.Logout();
                }

                EventHandler<FBEventArgs<string>> userDataDelegate = null;

                userDataDelegate = async (object sender, FBEventArgs<string> e) =>
                {
                    if (e == null) return;

                    switch (e.Status)
                    {
                        case FacebookActionStatus.Completed:
                            var facebookProfile = await Task.Run(() => JsonConvert.DeserializeObject<FacebookProfile>(e.Data));
                            var loadingPage = new LoadingPopupPage();
                            await PopupNavigation.PushAsync(loadingPage);
                            var socialLoginData = new NetworkAuthData
                            {
                                Email = facebookProfile.Email,
                                Name = $"{facebookProfile.FirstName} {facebookProfile.LastName}",
                                Id = facebookProfile.Id
                            };
                            Usuarios entidad = new Usuarios();
                            var userexist = await CheckIfExistUser(facebookProfile.Email);
                            int idgenerado = 0;
                            int idusuario = 0;
                            if (userexist == null)
                            {

                                var todos = await usuarioBL.Listar();
                                //int contador = todos.Count();
                                int contador = todos.Select(x => x.UsuarioID).LastOrDefault();
                                idgenerado = contador + 1;
                                UsuarioInput entidadinsert = new UsuarioInput()
                                {
                                    UsuarioID = idgenerado,
                                    TipoUsuarioID = "1",
                                    Usuario = socialLoginData.Email,
                                    Descripcion = socialLoginData.Name,
                                    Email = socialLoginData.Email,
                                    Telefono = "",
                                    Activo = true,
                                    Bloqueado = false,
                                    Token = "",
                                    FchHraCreacion = DateTime.Now,
                                    FchHraActualizacion = DateTime.Now,
                                    FchHraBloqueo = DateTime.Now,
                                    FchHraDesbloqueo = DateTime.Now,
                                    DataFarmaciasID = 0,
                                    FechaNacimiento = DateTime.Now
                                };
                                var registro = await usuarioBL.CreateUsuario(entidadinsert);
                                if (registro != null)
                                {
                                    idusuario = registro.UsuarioID;
                                    entidad = new Usuarios()
                                    {
                                        UsuarioID = registro.UsuarioID,
                                        TipoUsuarioID = registro.TipoUsuarioID,
                                        Usuario = registro.Email,
                                        Descripcion = registro.Descripcion,
                                        Email = registro.Email,
                                        Telefono = registro.Token,
                                        Activo = registro.Activo,
                                        Bloqueado = registro.Bloqueado,
                                        Token = "",
                                        FchHraCreacion = registro.FchHraCreacion,
                                        FchHraActualizacion = registro.FchHraActualizacion,
                                        FchHraBloqueo = registro.FchHraBloqueo,
                                        FchHraDesbloqueo = registro.FchHraDesbloqueo,
                                        DataFarmaciasID = registro.DataFarmaciasID,
                                        FechaNacimiento = registro.FechaNacimiento
                                    };
                                }
                            }
                            else
                            {
                                idusuario = userexist.UsuarioID;
                                entidad = new Usuarios()
                                {
                                    UsuarioID = userexist.UsuarioID,
                                    Email = socialLoginData.Email,
                                    Descripcion = socialLoginData.Name,
                                    Usuario = socialLoginData.Email,
                                    Telefono = "",
                                    Token = "",
                                    TipoUsuarioID = "1",
                                    Activo = true,
                                    Bloqueado = false,
                                    FchHraCreacion = userexist.FchHraCreacion,
                                    FchHraBloqueo = userexist.FchHraBloqueo,
                                    FchHraActualizacion = userexist.FchHraActualizacion,
                                    FchHraDesbloqueo = userexist.FchHraDesbloqueo,
                                    DataFarmaciasID = userexist.DataFarmaciasID
                                };
                            }


                            var mainViewModel = MenuTabbedPageViewModel.GetInstance();
                            mainViewModel.Usuario = entidad;
                            //mainViewModel.RegisterDevice();

                            TokenResponse newtoken = new TokenResponse()
                            {
                                AccessToken = socialLoginData.Email,
                                UserName = entidad.Descripcion,
                                Password = "",
                                IsRemembered = true,
                                Expires = DateTime.Now.AddDays(1)
                            };
                            Settings.UserEmail = socialLoginData.Email;
                            Settings.UserName = socialLoginData.Name;
                            Settings.UserID = idusuario;
                            Settings.IsRemember = false;
                            Settings.Expires = DateTime.Now.AddDays(1);
                            Settings.UserPassword = "";
                            Settings.Token = JsonConvert.SerializeObject(newtoken);
                            ingresoactual = await Task.Run(() => CommonFunctions.GetSetIngreso(idusuario));
                            //this.IsRunning = false;
                            this.IsEnabled = true;
                            await PopupNavigation.RemovePageAsync(loadingPage);
                            App.Current.MainPage = new MenuTabbedPage();
                            break;
                        case FacebookActionStatus.Canceled:
                            break;
                    }

                    _facebookService.OnUserData -= userDataDelegate;
                };

                _facebookService.OnUserData += userDataDelegate;

                string[] fbRequestFields = { "email", "first_name", "gender", "last_name" };
                string[] fbPermisions = { "email" };
                await _facebookService.RequestUserDataAsync(fbRequestFields, fbPermisions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private async Task<Usuarios> CheckIfExistUser(string emailaddress)
        {
            int contador = 0;
            try
            {
                var usuarios = await usuarioBL.Listar();
                if (usuarios.Any())
                {
                    //contador = usuarios.Count();
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

        private async void LoginConsumer(object obj)
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new LoginMail());
        }
    }

}