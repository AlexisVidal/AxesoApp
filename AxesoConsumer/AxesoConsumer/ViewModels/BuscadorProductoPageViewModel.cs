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
    public class BuscadorProductoPageViewModel : INotifyPropertyChanged
    {
        public string titulo;
        public string Titulo
        {
            get
            {
                if (string.IsNullOrEmpty(titulo))
                    return "";
                else
                    return titulo;
            }
            set
            {
                if (titulo != value)
                {
                    titulo = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Titulo)));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ShowItemsCommand { get; }
        //public Command ShowItemsCommand
        //{
        //    get
        //    {
        //        return new Command(p =>
        //        {
        //            //// option 1
        //            //var vm = (MyPageModel)p;
        //            //vm.Name; // bla

        //            // option 2
        //            var name = p.ToString();
        //        });
        //    }
        //}
        //public ICommand ShowItemsCommand { get; }
        public BuscadorProductoPageViewModel()
        {
            ShowItemsCommand = new Command(ShowItems);
        }

        private void ShowItems(object obj)
        {
            var name = obj.ToString();
        }
    }
}
