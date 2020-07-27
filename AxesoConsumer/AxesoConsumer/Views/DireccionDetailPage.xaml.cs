using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Helpers;
using Rg.Plugins.Popup.Services;
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
    public partial class DireccionDetailPage : ContentPage
    {
        int direccionusuarioid = Settings.UsuarioDireccionID;
        int usuarioID = Settings.UserID;
        ModelsBL modelb = new ModelsBL();
        UsuarioDireccion usudireccion = new UsuarioDireccion();
        double latitude = 0;
        double longitude = 0;
        string direccion = "";
        string etiqueta = "";
        string departamento = "";
        int iddistrito = -1;
        Geocoder geoCoder;
        List<Distrito> distritolist = new List<Distrito>();
        public DireccionDetailPage()
        {
            InitializeComponent();
            geoCoder = new Geocoder();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);
            var distritosx = await modelb.GetAllDistritos();
            if (distritosx.Any())
            {
                distritolist = (List<Distrito>)distritosx.OrderBy(x => x.Nombre).ToList();
            }
            PickerDistrito.DisplayMemberPath = "Nombre";
            PickerDistrito.SelectedValuePath = "DistritoID";
            PickerDistrito.DataSource = distritolist;
            if (direccionusuarioid == 0)
            {
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(latitude, longitude),
                    Label = etiqueta,
                    Address = direccion
                };
                var position = new Position(latitude, longitude);
                customMap.Pins.Add(pin);
                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(1000)));

                Etiqueta.Text = etiqueta;
                Direccion.Text = direccion;
                DeleteDireccion.IsVisible = false;
                Departamento.Text = departamento;
                PickerDistrito.SelectedIndex = iddistrito;
            }
            else
            {
                var Itemsx = Task.Run(async () => await modelb.GetAllUsuarioDireccionByUsuarioID(usuarioID)).Result;
                if (Itemsx.Any())
                {
                    usudireccion = Itemsx.Where(x => x.UsuarioDireccionID == direccionusuarioid).FirstOrDefault();
                    if (usudireccion != null)
                    {
                        latitude = usudireccion.Latitud;
                        longitude = usudireccion.Longitud;
                        direccion = usudireccion.Direccion;
                        etiqueta = usudireccion.Nombre;
                        departamento = usudireccion.Departamento;
                        iddistrito = usudireccion.DistritoID;
                    }

                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(latitude, longitude),
                        Label = etiqueta,
                        Address = direccion
                    };
                    var position = new Position(latitude, longitude);
                    customMap.Pins.Add(pin);
                    customMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(1000)));

                    Etiqueta.Text = etiqueta;
                    Direccion.Text = direccion;
                    DeleteDireccion.IsVisible = true;
                    Departamento.Text = departamento;
                    PickerDistrito.SelectedValue = iddistrito;
                }
            }
           
            await PopupNavigation.Instance.RemovePageAsync(loadingPage);
        }
        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        async void DeleteDireccion_Clicked(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);
            bool resultado = true;
            try
            {
                if (direccionusuarioid == 0)
                {
                    return;
                }
                else
                {
                    var positionactual = customMap.VisibleRegion;
                    Settings.Latitude = positionactual.Center.Latitude;
                    Settings.Longitude = positionactual.Center.Longitude;
                    SetAddress(Settings.Latitude, Settings.Longitude);

                    UsuarioDireccionInputU entidadinsert = new UsuarioDireccionInputU()
                    {
                        UsuarioDireccionID = direccionusuarioid,
                        Nombre = etiqueta,
                        UsuarioID = Settings.UserID,
                        Direccion = direccion,
                        Latitud = latitude,
                        Longitud = longitude,
                        Activo = false,
                        DistritoID = iddistrito,
                        Departamento = Departamento.Text
                    };
                    var rgistrodireccion = await modelb.UpdateUsuarioDireccion(entidadinsert);
                    if (rgistrodireccion != null)
                    {
                        resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.RemovePageAsync(loadingPage);
                Settings.ErrorText = "No se pudo eliminar!";
                await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
                return;
            }
            await PopupNavigation.Instance.RemovePageAsync(loadingPage);

            Settings.SuccessText = "Eliminacion exitosa";
            await PopupNavigation.Instance.PushAsync(new SuccessPopUpPage());
            await Navigation.PopAsync();

        }

        async void AddUpdateDireccion_Clicked(object sender, EventArgs e)
        {
            if (Etiqueta.Text.Equals(""))
            {
                Settings.ErrorText = "Complete el campo de nombre!";
                await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
                return;
            }
            if (Direccion.Text.Equals(""))
            {
                Settings.ErrorText = "Complete el campo de direccion!";
                await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
                return;
            }
            if (iddistrito == -1)
            {
                Settings.ErrorText = "Seleccione distrito!";
                await PopupNavigation.Instance.PushAsync(new ErroPopUpPage());
                return;
            }
            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);
            bool resultado = true;
            try
            {
                if (direccionusuarioid == 0)
                {
                    var positionactual = customMap.VisibleRegion;
                    Settings.Latitude = positionactual.Center.Latitude;
                    Settings.Longitude = positionactual.Center.Longitude;
                    SetAddress(Settings.Latitude, Settings.Longitude);

                    UsuarioDireccionInput entidadinsert = new UsuarioDireccionInput()
                    {
                        Nombre = Etiqueta.Text,
                        UsuarioID = Settings.UserID,
                        Direccion = Direccion.Text,
                        Latitud = Settings.Latitude,
                        Longitud = Settings.Longitude,
                        Activo = true,
                        DistritoID = iddistrito,
                        Departamento = Departamento.Text

                    };
                    var rgistrodireccion = await modelb.AddUsuarioDireccion(entidadinsert);
                    if (rgistrodireccion != null)
                    {
                        resultado = true;
                    }
                }
                else
                {
                    var positionactual = customMap.VisibleRegion;
                    Settings.Latitude = positionactual.Center.Latitude;
                    Settings.Longitude = positionactual.Center.Longitude;
                    SetAddress(Settings.Latitude, Settings.Longitude);

                    UsuarioDireccionInputU entidadinsert = new UsuarioDireccionInputU()
                    {
                        UsuarioDireccionID = direccionusuarioid,
                        Nombre = Etiqueta.Text,
                        UsuarioID = Settings.UserID,
                        Direccion = Direccion.Text,
                        Latitud = Settings.Latitude,
                        Longitud = Settings.Longitude,
                        Activo = true,
                        DistritoID = iddistrito,
                        Departamento = Departamento.Text
                    };
                    var rgistrodireccion = await modelb.UpdateUsuarioDireccion(entidadinsert);
                    if (rgistrodireccion != null)
                    {
                        resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            await PopupNavigation.Instance.RemovePageAsync(loadingPage);

            //Settings.SuccessText = "Registro exitoso";
            //await PopupNavigation.Instance.PushAsync(new SuccessPopUpPage());
            await Navigation.PopAsync();
        }

        async void searchAddress_SearchButtonPressed(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);

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

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(1000)));

            latitude = position.Latitude;
            direccion = addressQuery;
            longitude = position.Longitude;


            Settings.Latitude = latitude;
            Settings.Longitude = longitude;
            Settings.Address = direccion;
            SetAddress(Settings.Latitude, Settings.Longitude);
            direccion = Settings.Address;
            Direccion.Text = direccion;
            await PopupNavigation.Instance.RemovePageAsync(loadingPage);
        }
        async void SetAddress(double latitude, double longitude)
        {
            if (latitude != 0 && longitude != 0)
            {
                try
                {
                    Position positionc = new Position(latitude, longitude);
                    IEnumerable<string> possibleAddresses = Task.Run(async () => await geoCoder.GetAddressesForPositionAsync(positionc)).Result;
                    string address = possibleAddresses.FirstOrDefault();
                    Settings.Address = address;
                }
                catch (Exception ex)
                {

                }
            }
        }

        async void getAddressBtn_Clicked(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await PopupNavigation.Instance.PushAsync(loadingPage);

            var positionactual = customMap.VisibleRegion;
            Settings.Latitude = positionactual.Center.Latitude;
            Settings.Longitude = positionactual.Center.Longitude;
            SetAddress(Settings.Latitude, Settings.Longitude);
            direccion = Settings.Address;
            Direccion.Text = direccion;

            await PopupNavigation.Instance.RemovePageAsync(loadingPage);
        }
        string distrito = "";
        private void PickerDistrito_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            iddistrito = 0;
           
            var seleccion = (Distrito)e.Value;
            if (seleccion != null)
            {
                int position = PickerDistrito.SelectedIndex;
                iddistrito = seleccion.DistritoID;
                distrito = seleccion.Nombre;
            }
        }
    }
}