using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AxesoConsumer.Models
{
    public class CategoriaInfo : INotifyPropertyChanged
    {
        private string categoriaName;
        private string categoriaDesc;

        public string CategoriaName
        {
            get { return categoriaName; }
            set
            {
                categoriaName = value;
                OnPropertyChanged("CategoriaName");
            }
        }

        public string CategoriaDescription
        {
            get { return categoriaDesc; }
            set
            {
                categoriaDesc = value;
                OnPropertyChanged("CategoriaDescription");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
