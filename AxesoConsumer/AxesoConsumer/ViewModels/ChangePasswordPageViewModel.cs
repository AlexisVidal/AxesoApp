using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Helpers;
using AxesoConsumer.Services;
using AxesoConsumer.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AxesoConsumer.ViewModels
{
    public class ChangePasswordPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Usuarios usuariom = null;

        List<Usuarios> lusuario = null;

        string claveEncrip = "";
        public ICommand SaveChangeBtn { get; }
        private ModelsBL usuarioBL = new ModelsBL();
        #region Services
        ApiService apiService;
        //NavigationService navigationService;
        string AdminUser = Application.Current.Resources["AdminUser"].ToString();
        string AdminPassWord = Application.Current.Resources["AdminPassWord"].ToString();
        string SMTPName = Application.Current.Resources["SMTPName"].ToString();
        string SMTPPort = Application.Current.Resources["SMTPPort"].ToString();
        string AppName = Resources.Resource.AppName;
        string PasswordRecovery = Resources.Resource.PasswordRecovery;
        string PasswordYourNew = Resources.Resource.PasswordYourNew;
        string PasswordDontForget = Resources.Resource.PasswordDontForget;
        string subject = Resources.Resource.AppName + " - " + Resources.Resource.PasswordRecovery;
        string yournewp = Resources.Resource.PasswordYourNew;
        string dontforg = Resources.Resource.PasswordDontForget;
        #endregion
        #region Attributes
        bool _isEnabled;
        string currentPassword;
        string newPassword;
        string confirmPassword;
        string email;
        #endregion

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }


        public string CurrentPassword
        {
            get
            {
                return currentPassword;
            }
            set
            {
                if (currentPassword != value)
                {
                    currentPassword = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(CurrentPassword)));
                }
            }
        }

        public string NewPassword
        {
            get
            {
                return newPassword;
            }
            set
            {
                if (newPassword != value)
                {
                    newPassword = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(NewPassword)));
                }
            }
        }
        public string ConfirmPassword
        {
            get
            {
                return confirmPassword;
            }
            set
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ConfirmPassword)));
                }
            }
        }

        public ChangePasswordPageViewModel()
        {
            instance = this;
            apiService = new ApiService();

            IsEnabled = true;
            email = Settings.UserEmail.ToString();
            SaveChangeBtn = new Command(SaveChangeBtnConsumer);
        }

        private async void SaveChangeBtnConsumer(object obj)
        {
            if (string.IsNullOrEmpty(CurrentPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.PasswordValidation,
                        Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(NewPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.PasswordValidation,
                        Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(ConfirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.PasswordValidation,
                        Languages.Accept);
                return;
            }
            if (!(ConfirmPassword).Equals(NewPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.PasswordDiferent,
                        Languages.Accept);
                return;
            }

            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.PushAsync(loadingPage);
            IsEnabled = false;

            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                IsEnabled = true;
                await PopupNavigation.RemovePageAsync(loadingPage);
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.TurnOnInternet,
                    Languages.Accept);
                return;
            }
            string passencript = Constants.EncriptaClave(CurrentPassword);
            var existe = await CheckIfExistUser(email, passencript);
            if (existe == null)
            {
                IsEnabled = true;
                await PopupNavigation.RemovePageAsync(loadingPage);
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordValidation,
                    Languages.Accept);
                return;
            }
            int id = existe.UsuarioID;
            //var resultado = await apiService.CambiaClave(id, email, NewPassword);


            MailModel enviomail = new MailModel()
            {
                id = id,
                email = email,
                newPassword = NewPassword,
                AdminPassWord = AdminPassWord,
                AdminUser = AdminUser,
                SMTPName = SMTPName,
                SMTPPort = SMTPPort,
                AppName = AppName,
                PasswordRecovery = PasswordRecovery,
                PasswordYourNew = PasswordYourNew,
                PasswordDontForget = PasswordDontForget,
                subject = subject,
                yournewp = yournewp,
                dontforg = dontforg,
                to = email
            };
            var resultado = await usuarioBL.CambiaClave(enviomail);
            if (resultado.respuesta)
            {

                await Application.Current.MainPage.DisplayAlert(
                    Languages.Success,
                    Languages.SuccessRegister,
                    Languages.Accept);
                //App.Current.MainPage = new LoginPage();
            }
            await PopupNavigation.RemovePageAsync(loadingPage);

            IsEnabled = true;
        }
        private async Task<Usuarios> CheckIfExistUser(string emailaddress, string passencript)
        {

            try
            {
                var usuarios = await usuarioBL.Listar();
                if (usuarios.Any())
                {
                    //contador = usuarios.Count();
                    var existe = usuarios.Where(x => x.Usuario.ToUpper().Equals(emailaddress.ToUpper()) 
                    && x.Token.Equals(passencript)).FirstOrDefault();
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
        #region Sigleton
        static ChangePasswordPageViewModel instance;

        public static ChangePasswordPageViewModel GetInstance()
        {
            return instance;
        }
        #endregion
    }
}
