using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Controls;
using AxesoConsumer.Data;
using AxesoConsumer.Helpers;
using AxesoConsumer.Interfaces;
using AxesoConsumer.Models;
using AxesoConsumer.Repositories;
using AxesoConsumer.Services;
using AxesoConsumer.ViewModels;
using GalaSoft.MvvmLight;
using GeoCoordinatePortable;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Rg.Plugins.Popup.Services;
using SearchPlaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class UbicacionPage : ContentPage
    {
        GeolocatorService geolocatorService;
        double latitude = 0;
        double longitude = 0;
        double distancia = 1000;
        double distanciaadd = 50;
        string direccion = "";
        private readonly IPlaceService Service;
        List<SolicitudLite> lsolicitudes = new List<SolicitudLite>();
        ProductoLiteRepository repoproducto;
        SolicitudLiteRepository reposolicitud;
        SolicitudDataFarmaciasLiteRepository reposolicituddata;
        string location;

        Geocoder geoCoder;
        public List<Establecimiento> Items;
        public List<DataFarmacias> dataFarmacias;
        public IEnumerable<DataFarmacias> farmacias;
        ModelsBL modelbl = new ModelsBL();
        //UbicacionPageViewModel instancia = new UbicacionPageViewModel();
        public UbicacionPage()
        {
            geoCoder = new Geocoder();
            try
            {
                InitializeComponent();
                this.reposolicitud = new SolicitudLiteRepository();
                this.reposolicituddata = new SolicitudDataFarmaciasLiteRepository();
                this.repoproducto = new ProductoLiteRepository();
                this.BindingContext = new UbicacionPageViewModel();
                farmacias = Task.Run(async () => await modelbl.GetAllDataFarmacias()).Result;
                dataFarmacias = new List<DataFarmacias>();
                //if (farmacias.Any())
                //{
                //    dataFarmacias = (List<DataFarmacias>)farmacias;
                //}
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
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(latitude, longitude),
                    Label = "Ubicacion Actual",
                    Address = direccion
                };
                location = $"{pin.Position.Latitude}|{pin.Position.Longitude}";
                var position = new Position(latitude, longitude);
                customMap.Circle = new CustomCircle
                {
                    Position = position,
                    Radius = distancia
                };

                customMap.Pins.Add(pin);
                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(distancia + distanciaadd)));
                searchAddress.Text = direccion;

                //var a = Task.Run(async () => await LoadEstablecimientos());
            }
            catch (Exception w)
            {

            }
            // MoveMapToCurrentPosition();
        }




        /// <summary>
        /// Carga establecimientos
        /// </summary>
        /// <returns>Pins de establecimientos en el mapa</returns>
        private async Task LoadEstablecimientos()
        {
            var instancia = UbicacionPageViewModel.GetInstance();
            await instancia.LoadPins();
            try
            {
                foreach (var item in instancia.Pins)
                {
                    customMap.Pins.Add(item);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            double tempcober = 1000;
            LoadSolicitudes();
            if ((Settings.Distancia / 1000) == 0)
            {
                Settings.Distancia = tempcober;
            }
            RangeBtn.Text = (Settings.Distancia / 1000) + "Km";
            //RangeBtn.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button));
            RangeBtn.FontSize = Convert.ToDouble(10);

            await LoadEstablecimientos();
            //await LoadEstablecimientos();

            CotizaBtn.Text = "0 S/.";
            CotizaBtn.FontSize = Convert.ToDouble(10);

            if (Settings.Distancia == 0)
            {
                distancia = 1000;
            }
            else
            {
                distancia = Settings.Distancia;
            }

            latitude = Settings.Latitude;
            direccion = Settings.Address;
            longitude = Settings.Longitude;
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(latitude, longitude),
                Label = "Ubicacion Actual",
                Address = direccion
            };

            var position = new Position(latitude, longitude);
            customMap.Circle = new CustomCircle
            {
                Position = position,
                Radius = distancia
            };
            
            customMap.Pins.Add(pin);
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(distancia + distanciaadd)));
            Settings.Latitude = latitude;
            Settings.Address = direccion;
            Settings.Longitude = longitude;
            Settings.Distancia = distancia;
            searchAddress.Text = direccion;
            

            //var instancia = UbicacionPageViewModel.GetInstance();
            //await instancia.LoadPins();
            //foreach (var item in instancia.Pins)
            //{
            //    customMap.Pins.Add(item);
            //}
        }

        async void LoadSolicitudes()
        {
            lsolicitudes = this.reposolicitud.BuscarSolicitudLitesByUser(Settings.UserEmail);
            //lsolicitudes = await App.Database.GetSolicitudes(Settings.UserEmail);

            if (lsolicitudes.Any())
            {
                EmpezarBtn.Text = "Continuar";
            }
        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }
        private async void searchAddress_SearchButtonPressed(object sender, EventArgs e)
        {
            var addressQuery = searchAddress.Text;
            searchAddress.Text = "";
            searchAddress.Unfocus();

            var positions = (await (new Geocoder()).GetPositionsForAddressAsync(addressQuery)).ToList();
            if (!positions.Any())
                return;

            var position = positions.First();
            customMap.Pins.Add(new Pin
            {
                Label = addressQuery,
                Position = position,
                Address = addressQuery
            });
            customMap.Circle = new CustomCircle
            {
                Position = position,
                Radius = distancia
            };

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(distancia + distanciaadd)));

            latitude = position.Latitude;
            direccion = addressQuery;
            longitude = position.Longitude;


            Settings.Latitude = latitude;
            Settings.Address = direccion;
            Settings.Longitude = longitude;
            Settings.Distancia = distancia;

            //await Task.WhenAll(GetGoogleAddressSuggestions(), GetGooglePlaceSuggestions());
        }
        //private async Task GetGoogleAddressSuggestions()
        //{
        //    PlaceSearchAutoComplete = new ObservableCollection<Prediction>();
        //    var result = await Service.GetAutoCompleteGoogleAddresses(searchAddress.Text);
        //    if (result.status == "OK")
        //    {
        //        foreach (var prediction in result.predictions)
        //        {
        //            prediction.SuggestionTypeColor = Color.Red;
        //            PlaceSearchAutoComplete.Add(prediction);
        //        }
        //    }
        //    else if (result.status == "REQUEST_DENIED")
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Warning", "Please enter API Key in PlaceService.cs", "Okay");
        //    }
        //    else if (result.status == "OVER_QUERY_LIMIT" || result.status == "OVER_DAILY_LIMIT" || result.status == "MAX_ELEMENTS_EXCEEDED")
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Warning", result.status, "Okay");
        //    }
        //    else
        //    {
        //        if (PlaceSearchAutoComplete != null)
        //            PlaceSearchAutoComplete = new ObservableCollection<Prediction>();
        //    }
        //}
        //private async Task GetGooglePlaceSuggestions()
        //{
        //    var result = await Service.GetAutoCompleteGooglePlaces(searchAddress.Text);
        //    if (result.status == "OK")
        //    {
        //        foreach (var prediction in result.predictions)
        //        {
        //            prediction.SuggestionTypeColor = Color.Black;
        //            PlaceSearchAutoComplete.Add(prediction);
        //        }
        //    }
        //    else if (result.status == "REQUEST_DENIED")
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Warning", "Please enter API Key in PlaceService.cs", "Okay");
        //    }
        //    else if (result.status == "OVER_QUERY_LIMIT" || result.status == "OVER_DAILY_LIMIT" || result.status == "MAX_ELEMENTS_EXCEEDED")
        //    {
        //        await Application.Current.MainPage.DisplayAlert("Warning", result.status, "Okay");
        //    }
        //    else
        //    {
        //        if (PlaceSearchAutoComplete != null)
        //            PlaceSearchAutoComplete = new ObservableCollection<Prediction>();
        //    }
        //}

        async void SetAddress(double latitude, double longitude)
        {
            if (latitude != 0 && longitude != 0)
            {
                Position positionc = new Position(latitude, longitude);
                IEnumerable<string> possibleAddresses = Task.Run(async () => await geoCoder.GetAddressesForPositionAsync(positionc)).Result;
                string address = possibleAddresses.FirstOrDefault();
                Settings.Address = address;
            }
        }
        async void EmpezarBtn_Clicked(object sender, EventArgs e)
        {
            List<SolicitudDataFarmaciasLite> listSolicitudFarma = new List<SolicitudDataFarmaciasLite>();
            string textbtn = EmpezarBtn.Text.ToLower();
            var positionactual = customMap.VisibleRegion;
            double distancias = Settings.Distancia;
            /*****/
            Settings.Latitude = -5.228832;
            Settings.Longitude = -80.638422;
            SetAddress(Settings.Latitude, Settings.Longitude);
            #region traefarmacias
            if (farmacias.Any())
            {
                GeoCoordinate pin1 = new GeoCoordinate(Settings.Latitude, Settings.Longitude);
                foreach (var item in farmacias)
                {
                    GeoCoordinate pin2 = new GeoCoordinate(item.Latitud, item.Longitud);
                    double distanceBetween = pin1.GetDistanceTo(pin2);
                    if (distanceBetween <= distancias)
                    {
                        dataFarmacias.Add(item);
                    }
                }

            }
            #endregion
            /********/

            if (positionactual != null && textbtn.Equals("empezar"))
            {
                Settings.Latitude = positionactual.Center.Latitude;
                Settings.Longitude = positionactual.Center.Longitude;
                SetAddress(Settings.Latitude, Settings.Longitude);
            }


            string solicitudID = Constants.GeneraPass();
            if (textbtn.Equals("empezar"))
            {
                SolicitudLite solicito = new SolicitudLite
                {
                    IdSolicitud = solicitudID,
                    Usuario = Settings.UserEmail,
                    Fecha = DateTime.Now,
                    Activo = true,
                    Latitude = Settings.Latitude,
                    Longitud = Settings.Longitude,
                    Address = Settings.Address,
                    Distancia = Settings.Distancia,
                    Enviado = false
                };
                //var resultado = await App.Database.InsertSolicitud(solicito);
                var resultado = this.reposolicitud.InsertarSolicitudLite(solicito);
                if (dataFarmacias.Any())
                {
                    foreach (var item in dataFarmacias)
                    {
                        SolicitudDataFarmaciasLite nueva = new SolicitudDataFarmaciasLite()
                        {
                            IdSolicitud = resultado.IdSolicitud,
                            DataFarmaciasID = item.ID,
                            Activo = true
                        };
                        listSolicitudFarma.Add(nueva);
                        this.reposolicituddata.InsertarSolicitudDataFarmaciasLite(nueva);
                    }
                }
                Settings.CantFarmacias = dataFarmacias.Count;
                Settings.CurrentSolicitud = resultado.IdSolicitud;
            }
            await Navigation.PushAsync(new BuscadorProductoPage());
        }

        async void RangeBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoveragePage());
        }

        private void CotizaBtn_Clicked(object sender, EventArgs e)
        {

        }


    }
}