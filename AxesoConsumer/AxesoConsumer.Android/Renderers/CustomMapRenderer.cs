using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Axeso_BE;
using AxesoConsumer.Controls;
using AxesoConsumer.Droid.Renderers;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using APolyline = Android.Gms.Maps.Model.Polyline;
using APolygon = Android.Gms.Maps.Model.Polygon;
using ACircle = Android.Gms.Maps.Model.Circle;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace AxesoConsumer.Droid.Renderers
{
    public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        //CustomCircle circle;
        bool isDrawn;
        //IList<Xamarin.Forms.Maps.Pin> pines;
        List<Marker> _markers;
    

        public CustomMapRenderer(Context context) : base(context)
        {

        }
        

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.Maps.Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
            }
            
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
            base.OnElementPropertyChanged(sender, e);
            if (this.Element == null || this.Control == null)
                return;
            var formsMap = (CustomMap)this.Element;
            //circle = formsMap.Circle;
            double lat = 0;
            double lon = 0;
            try
            {
                lat = this.Element.VisibleRegion.Center.Latitude;
                lon = this.Element.VisibleRegion.Center.Longitude;
            }
            catch (Exception ex)
            {
                //lat = circle.Position.Latitude;
                lat = 0;
                //lon = circle.Position.Longitude;
                lon = 0;
            }

            //var circleOptions = new CircleOptions();
            //circleOptions.InvokeCenter(new LatLng(lat,lon));
            //circleOptions.InvokeRadius(circle.Radius);
            //circleOptions.InvokeFillColor(0x6FFFFF00);
            //circleOptions.InvokeStrokeColor(0X66FF0000);
            //circleOptions.InvokeStrokeWidth(0);

            var markerOptions = new MarkerOptions();
            markerOptions.SetPosition(new LatLng(lat, lon));

            try
            {
                NativeMap.Clear();
                NativeMap.AddMarker(markerOptions);
                //NativeMap.AddCircle(circleOptions);
            }
            catch (Exception ex)
            {

            }
        }
        void AddPins(IList pins)
        {
            GoogleMap map = NativeMap;
            if (map == null)
            {
                return;
            }

            if (_markers == null)
            {
                _markers = new List<Marker>();
            }

            _markers.AddRange(pins.Cast<Pin>().Select(p =>
            {
                Pin pin = p;
                var opts = CreateMarker(pin);
                var marker = map.AddMarker(opts);

                pin.PropertyChanged += PinOnPropertyChanged; ;

                // associate pin with marker for later lookup in event handlers
                pin.MarkerId = marker.Id;
                return marker;
            }));
        }

        private void PinOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Pin pin = (Pin)sender;
            Marker marker = GetMarkerForPin(pin);

            if (marker == null)
            {
                return;
            }

            if (e.PropertyName == Pin.LabelProperty.PropertyName)
            {
                marker.Title = pin.Label;
            }
            else if (e.PropertyName == Pin.AddressProperty.PropertyName)
            {
                marker.Snippet = pin.Address;
            }
            else if (e.PropertyName == Pin.PositionProperty.PropertyName)
            {
                marker.Position = new LatLng(pin.Position.Latitude, pin.Position.Longitude);
            }
        }

        protected override void OnMapReady(Android.Gms.Maps.GoogleMap map)
        {
            base.OnMapReady(map);

            //var circleOptions = new CircleOptions();
            //try
            //{
            //    circleOptions.InvokeCenter(new LatLng(circle.Position.Latitude, circle.Position.Longitude));
            //    circleOptions.InvokeRadius(circle.Radius);
            //    circleOptions.InvokeFillColor(0x6FFFFF00);
            //    circleOptions.InvokeStrokeColor(0X66FF0000);
            //    circleOptions.InvokeStrokeWidth(0);

            //    NativeMap.AddCircle(circleOptions);
            //}
            //catch (Exception ex)
            //{

            //}
            NativeMap.MapClick += NativeMap_MapClick;
            NativeMap.MapLongClick += NativeMap_MapLongClick;
        }

        private void NativeMap_MapLongClick(object sender, GoogleMap.MapLongClickEventArgs e)
        {
            if (this.Element == null || this.Control == null)
                return;
            var formsMap = (CustomMap)this.Element;
            //circle = formsMap.Circle;

            //var circleOptions = new CircleOptions();
            //circleOptions.InvokeCenter(new LatLng(e.Point.Latitude, e.Point.Longitude));
            //circleOptions.InvokeRadius(circle.Radius);
            //circleOptions.InvokeFillColor(0x6FFFFF00);
            //circleOptions.InvokeStrokeColor(0X66FF0000);
            //circleOptions.InvokeStrokeWidth(0);

            var markerOptions = new MarkerOptions();
            markerOptions.SetPosition(new LatLng(e.Point.Latitude, e.Point.Longitude));

            try
            {
                NativeMap.Clear();
                NativeMap.AddMarker(markerOptions);
                //NativeMap.AddCircle(circleOptions);
            }
            catch (Exception ex)
            {

            }
        }

        private void NativeMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {

        }


        protected Marker GetMarkerForPin(Pin pin)
        {
            Marker targetMarker = null;

            if (_markers != null)
            {
                for (int i = 0; i < _markers.Count; i++)
                {
                    var marker = _markers[i];
                    if (marker.Id == (string)pin.MarkerId)
                    {
                        targetMarker = marker;
                        break;
                    }
                }
            }

            return targetMarker;
        }

        protected Pin GetPinForMarker(Marker marker)
        {
            Pin targetPin = null;

            for (int i = 0; i < Map.Pins.Count; i++)
            {
                var pin = Map.Pins[i];
                if ((string)pin.MarkerId == marker.Id)
                {
                    targetPin = pin;
                    break;
                }
            }

            return targetPin;
        }
    }
}