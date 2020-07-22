using Axeso_BL;
using AxesoConsumer.Helpers;
using AxesoConsumer.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace AxesoConsumer.ViewModels
{
    [Preserve]
    public class ProfilePageViewModel : INotifyPropertyChanged
    {
        
        private ModelsBL usuarioBL = new ModelsBL();
        public string nombreUsuario;
        public string emailUsuario;

        public event PropertyChangedEventHandler PropertyChanged;
        public string NombreUsuario
        {
            get
            {
                if (string.IsNullOrEmpty(nombreUsuario))
                    return "";
                else
                    return nombreUsuario;
            }
            set
            {
                if (nombreUsuario != value)
                {
                    nombreUsuario = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(NombreUsuario)));
                }
            }
        }
        public string EmailUsuario
        {
            get
            {
                if (string.IsNullOrEmpty(emailUsuario))
                    return "";
                else
                    return emailUsuario;
            }
            set
            {
                if (nombreUsuario != value)
                {
                    emailUsuario = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(EmailUsuario)));
                }
            }
        }
        public ProfilePageViewModel()
        {
            
            NombreUsuario = Settings.UserName;
            EmailUsuario = Settings.UserEmail;
        }

        
    }
}
