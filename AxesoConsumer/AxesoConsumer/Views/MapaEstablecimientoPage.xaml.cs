using AxesoConsumer.Helpers;
using AxesoConsumer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapaEstablecimientoPage : ContentPage
    {
        public MapaEstablecimientoPage()
        {
            InitializeComponent();
            this.BindingContext = new MapaEstablecimientoViewModel();
        }
        private void PositionMap()
        {
            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(GlobalSetting.UserLatitude, GlobalSetting.UserLongitude),
                    Distance.FromMiles(1)));
        }
    }
}