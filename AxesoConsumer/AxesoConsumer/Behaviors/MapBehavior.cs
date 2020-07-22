using AxesoConsumer.Controls;
using AxesoConsumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AxesoConsumer.Behaviors
{
    public class MapBehavior : BindableBehavior<Map>
    {
        private Map _map;

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<Establecimiento>), typeof(MapBehavior), null, BindingMode.Default, propertyChanged: ItemsSourceChanged);

        public IEnumerable<Establecimiento> ItemsSource
        {
            get => (IEnumerable<Establecimiento>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            //if (!(bindable is MapBehavior behavior)) return;
            //behavior.AddPins();

            var mapBehavior = bindable as MapBehavior;

            if (mapBehavior != null)
            {
                mapBehavior.AddPins();
                mapBehavior.PositionMap();
            }
        }
        protected override void OnAttachedTo(Map bindable)
        {
            base.OnAttachedTo(bindable);

            _map = bindable;
        }
        protected override void OnDetachingFrom(Map bindable)
        {
            base.OnDetachingFrom(bindable);

            _map = null;
        }
        private void PositionMap()
        {
            if (ItemsSource == null || !ItemsSource.Any()) return;

            var centerPosition = new Position(ItemsSource.Average(x => x.Latitude), ItemsSource.Average(x => x.Longitude));

            var distance = 0.5;
            _map.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromKilometers(distance)));

            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                _map.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromKilometers(distance)));
                return false;
            });
        }
        private void AddPins()
        {
            //var map = AssociatedObject;
            for (int i = _map.Pins.Count - 1; i >= 0; i--)
            {
                _map.Pins[i].Clicked -= PinOnClicked;
                _map.Pins.RemoveAt(i);
            }

            var pins = ItemsSource.Select(x =>
            {
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(x.Latitude, x.Longitude),
                    Label = x.Name
                    //,Address = x.Direccion,

                };

                pin.Clicked += PinOnClicked;
                return pin;
            }).ToArray();
            foreach (var pin in pins)
                _map.Pins.Add(pin);
        }

        private void PinOnClicked(object sender, EventArgs eventArgs)
        {
            var pin = sender as Pin;
            if (pin == null) return;
            var viewModel = ItemsSource.FirstOrDefault(x => x.Name == pin.Label);
            if (viewModel == null) return;
            //viewModel.Command.Execute(null); // TODO We are going to implement this later ;)
        }
    }
}
