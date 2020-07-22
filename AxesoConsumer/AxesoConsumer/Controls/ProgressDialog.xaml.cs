using AxesoConsumer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProgressDialog : Grid
    {
        public ProgressDialog()
        {
            InitializeComponent();
            Loader.Source = CommonFunctions.GetLoaderSource();
        }
    }
}