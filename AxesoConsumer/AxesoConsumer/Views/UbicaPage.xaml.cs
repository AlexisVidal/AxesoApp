using AxesoConsumer.Helpers;
using AxesoConsumer.Services;
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
    public partial class UbicaPage : ContentPage
    {
        GeolocatorService geolocatorService;
        double latitude = 0;
        double longitude = 0;

        string direccion = "";
        Geocoder geoCoder;
        public UbicaPage()
        {
            try
            {
                InitializeComponent();
                geoCoder = new Geocoder();
                latitude = Settings.Latitude;
                longitude = Settings.Longitude;
                direccion = Settings.Address;
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(latitude, longitude),
                    Label = "Ubicacion Actual",
                    Address = direccion
                };


                var position = new Position(latitude, longitude);
                MyMap.Pins.Add(pin);
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(1.0)));
            }
            catch (Exception ex)
            {

            }
        }

        async void MyMap_MapClicked(object sender, MapClickedEventArgs e)
        {
            try
            {
                MyMap.Pins.Clear();
            }
            catch (Exception ex)
            {

            }
            latitude = e.Position.Latitude;
            longitude = e.Position.Longitude;
            string direx = await GetAddress(latitude, longitude);
            direccion = direx;
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(latitude, longitude),
                Label = "Ubicacion Actual",
                Address = direccion
            };

            var position = new Position(latitude, longitude);
            MyMap.Pins.Add(pin);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(1.0)));
        }

        async Task<string> GetAddress(double latitude, double longitude)
        {
            if (latitude != 0 && longitude != 0)
            {
                Position positionc = new Position(latitude, longitude);
                IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(positionc);
                string address = possibleAddresses.FirstOrDefault();
                Settings.Address = address;
                return address;
            }
            else
            {
                return "";
            }
        }
    }
}