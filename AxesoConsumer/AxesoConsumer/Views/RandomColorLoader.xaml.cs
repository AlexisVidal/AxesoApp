using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RandomColorLoader : PopupPage
    {
        Random random = new Random();
        public RandomColorLoader()
        {
            InitializeComponent();
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                //customloader.Color =
                //Color.FromRgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                // True = Repeat again, False = Stop the timer
                return true;
            });
        }
    }
}