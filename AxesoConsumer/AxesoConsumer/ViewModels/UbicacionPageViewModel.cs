using Axeso_BE;
using Axeso_BL;
using AxesoConsumer.Controls;
using AxesoConsumer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AxesoConsumer.ViewModels
{
    public class UbicacionPageViewModel
    {
        static UbicacionPageViewModel instance;
        private List<Establecimiento> _items;
        //public SearchBar searchAddress ;
        public Map MyMap;
        private ModelsBL modelBL = new ModelsBL();
        //public CustomMap MyMap;
        //public List<Establecimiento> Items
        public List<Establecimiento> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        
        public ObservableCollection<Pin> Pins { get; set; }
        public UbicacionPageViewModel()
        {
            instance = this;
            //LoadPins();
            

            //MyMap = new Map();
            //searchAddress = new SearchBar();
            //searchAddress.SearchButtonPressed += async (e, a) =>
            //{
            //    var addressQuery = searchAddress.Text;
            //    searchAddress.Text = "";
            //    searchAddress.Unfocus();

            //    var positions = (await (new Geocoder()).GetPositionsForAddressAsync(addressQuery)).ToList();
            //    if (!positions.Any())
            //        return;

            //    var position = positions.First();
            //    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position,
            //            Distance.FromMiles(0.1)));
            //    MyMap.Pins.Add(new Pin
            //    {
            //        Label = addressQuery,
            //        Position = position,
            //        Address = addressQuery
            //    });
            //};
        }



        public static UbicacionPageViewModel GetInstance()
        {
            if (instance == null)
            {
                return new UbicacionPageViewModel();
            }
            return instance;
        }
        public async Task LoadPins()
        {
            var xItems = (List<DataFarmacias>)await modelBL.GetAllDataFarmacias();
            Items = new List<Establecimiento>();
            int aumenta = 1;
            foreach (var item in xItems)
            {
                Establecimiento newx = new Establecimiento
                {
                    Id = aumenta,
                    Name = item.Razon_social,
                    Direccion = item.Direccion,
                    Latitude = item.Latitud,
                    Longitude = item.Longitud,
                    Rate = 5
                };
                aumenta++;
                Items.Add(newx);
            }
            #region old
            //Items = new List<Establecimiento>
            //{
            //    new Establecimiento
            //    {
            //        Id = 1,
            //        Name = "Farmacia Felicidad",
            //        Direccion = "Av Country",
            //        Latitude = -5.183468,
            //        Longitude = -80.631248,
            //        Rate = 5
            //    },
            //    new Establecimiento
            //    {
            //        Id = 2,
            //        Name = "Inka Farma",
            //        Direccion = "Av Country",
            //        Latitude = -5.183624,
            //        Longitude = -80.631283,
            //        Rate = 5
            //    },
            //    new Establecimiento
            //    {
            //        Id = 3,
            //        Name = "MiFarma",
            //        Direccion = "Calle Arequipa",
            //        Latitude = -5.195270,
            //        Longitude = -80.626940,
            //        Rate = 5
            //    }
            //};
            #endregion
            Pins = new ObservableCollection<Pin>();
            if (Items.Any())
            {
                foreach (var item in Items)
                {
                    var pinx = new Pin
                    {
                        Address = item.Direccion,
                        Label = item.Name,
                        Position = new Position(item.Latitude, item.Longitude),
                        Type = PinType.Place
                    };
                    pinx.MarkerClicked += Pinx_Clicked;
                    Pins.Add(pinx);
                }
                
            }
            
        }

        private void Pinx_Clicked(object sender, EventArgs e)
        {
            var pin = sender as Pin;
            if (pin == null) return;
            var viewModel = Items.FirstOrDefault(x => x.Name == pin.Label);
            if (viewModel == null) return;
            //viewModel.Command.Execute(null); // TODO We are going to implement this later ;)
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
