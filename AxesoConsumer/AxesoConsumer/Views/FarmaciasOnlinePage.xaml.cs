using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Axeso_BE;
using AxesoConsumer.Helpers;
using GeoCoordinatePortable;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FarmaciasOnlinePage : ContentPage
    {
        private List<UsuarioDireccion> lfarmaciasonline;
        double distancias = Settings.Distancia;
        public FarmaciasOnlinePage(List<UsuarioDireccion> lfarmaciasonline)
        {
            InitializeComponent();
            this.lfarmaciasonline = lfarmaciasonline;
        }

        //public FarmaciasOnlinePage(List<UsuarioDireccion> lfarmaciasonline)
        //{
        //    this.lfarmaciasonline = lfarmaciasonline;
        //}

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        protected async override void OnAppearing()
        {
            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);
            base.OnAppearing();
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(Settings.Latitude, Settings.Longitude),
                Label = "Ubicacion Local",
                Address = Settings.Address
            };
            var position = new Position(Settings.Latitude, Settings.Longitude);
            map.Pins.Add(pin);
            
            if (this.lfarmaciasonline.Any())
            {
                foreach (var item in this.lfarmaciasonline)
                {
                    double distanciatemp = (DistanciaEntre(Settings.Latitude, Settings.Longitude, item.Latitud, item.Longitud))*1000;
                    if (distanciatemp > distancias)
                    {
                        distancias = distanciatemp;
                    }
                    var pin2 = new Pin
                    {
                        Type = PinType.SavedPin,
                        Position = new Position(item.Latitud, item.Longitud),
                        Label = item.Nombre,
                        Address = item.Direccion
                    };
                    map.Pins.Add(pin2);
                }
            }
            map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(distancias)));
            await PopupNavigation.Instance.RemovePageAsync(loadingPage);
        }

        private double DistanciaEntre(double latitude, double longitude, double latitud, double longitud)
        {
            try
            {
                GeoCoordinate pin1 = new GeoCoordinate(latitude, longitude);
                GeoCoordinate pin2 = new GeoCoordinate(latitud, longitud);
                double distanceBetween = (pin1.GetDistanceTo(pin2)) / 1000;
                double aTruncated = Math.Truncate(distanceBetween * 100) / 100;
                return aTruncated;
            }
            catch (Exception ex)
            {
                return Settings.Distancia;
            }
        }
    }
}

