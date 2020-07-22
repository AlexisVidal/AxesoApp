using Axeso_BL;
using AxesoConsumer.Controls;
using AxesoConsumer.Helpers;
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
    public partial class UbicacionNuevaPage : ContentPage
    {
        double latitude = 0;
        double longitude = 0;
        double distancia = 1000;
        double distanciaadd = 50;
        string direccion = "";
        ModelsBL modelb = new ModelsBL();
        List<CustomPinTwo> listcustompins = new List<CustomPinTwo>();
        public UbicacionNuevaPage()
        {
            InitializeComponent();
            if (Settings.Distancia == 0)
            {
                distancia = 2000;
            }
            else
            {
                distancia = Settings.Distancia;
            }

            latitude = Settings.Latitude;
            direccion = Settings.Address;
            longitude = Settings.Longitude;
            CustomPinTwo pin = new CustomPinTwo
            {
                Type = PinType.Place,
                Position = new Position(latitude, longitude),
                Label = "Ubicacion Actual",
                Address = direccion,
                Name = "Ubicacion",
                Url = ""
            };
            listcustompins.Add(pin);
            customMap.Pins.Add(pin);
            var datafarmas = Task.Run(async () => await modelb.GetAllDataFarmacias()).Result;
            if (datafarmas.Any())
            {
                foreach (var item in datafarmas)
                {
                    CustomPinTwo pin2 = new CustomPinTwo
                    {
                        Type = PinType.Place,
                        Position = new Position(item.Latitud, item.Longitud),
                        Label = "Farmacia ",
                        Address = item.direccion,
                        Name = item.Razon_social,
                        Url = ""
                    };
                    listcustompins.Add(pin2);
                    customMap.Pins.Add(pin2);
                }
            }
            customMap.CustomPins = listcustompins;
            var position = new Position(latitude, longitude);
            customMap.Circle = new CustomCircle
            {
                Position = position,
                Radius = distancia
            };
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latitude, longitude), Distance.FromMiles(1.0)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }
    }
}