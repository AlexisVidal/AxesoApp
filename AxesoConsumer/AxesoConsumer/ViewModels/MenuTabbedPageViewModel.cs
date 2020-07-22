using Axeso_BE;
using AxesoConsumer.Models;
using AxesoConsumer.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AxesoConsumer.ViewModels
{
    public class MenuTabbedPageViewModel : INotifyPropertyChanged
    {
        private int _countFarma;
        public int CountFarma
        {
            get => _countFarma;
            set
            {
                _countFarma = value;
                OnPropertyChanged(nameof(CountFarma));

            }
        }

        private int _countNotification;
        public int CountNotification
        {
            get => _countNotification;
            set
            {
                _countNotification = value;
                OnPropertyChanged(nameof(CountNotification));

            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Usuarios Usuario
        {
            get;
            set;
        }
        public TokenResponse Token
        {
            get;
            set;

        }
        public MenuTabbedPageViewModel()
        {
            instance = this;
            this.Usuario = new Usuarios();
            this.CountFarma = 15;
            this.CountNotification = 5;
        }
        private static MenuTabbedPageViewModel instance;

        public event PropertyChangedEventHandler PropertyChanged;

        public static MenuTabbedPageViewModel GetInstance()
        {
            
            if (instance == null)
            {
                return new MenuTabbedPageViewModel();
            }

            return instance;
        }
    }
}
