using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AxesoConsumer.Controls;
using AxesoConsumer.Droid;
using AxesoConsumer.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DatePickerCtrl), typeof(DatePickerCtrlRenderer))]
namespace AxesoConsumer.Droid.Renderers
{
    [Obsolete]
    public class DatePickerCtrlRenderer : DatePickerRenderer
    {
        public DatePickerCtrl ElementV2 => Element as DatePickerCtrl;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);
            this.Control.SetTextColor(Android.Graphics.Color.Rgb(0,0,0));
            this.Control.SetBackgroundColor(Android.Graphics.Color.White);
            this.Control.SetPadding(20, 0, 0, 0);

            GradientDrawable gd = new GradientDrawable();
            gd.SetCornerRadius(25); //increase or decrease to changes the corner look  
            gd.SetColor(Android.Graphics.Color.White);
            //gd.SetStroke(3, Android.Graphics.Color.Rgb(83, 63, 149));
            gd.SetStroke(3,Android.Graphics.Color.Rgb(190,190,190));



            this.Control.SetBackgroundDrawable(gd);

            DatePickerCtrl element = Element as DatePickerCtrl;

            if (!string.IsNullOrWhiteSpace(element.Placeholder))
            {
                Control.Text = element.Placeholder;
            }

            this.Control.TextChanged += (sender, arg) => {
                var selectedDate = arg.Text.ToString();
                if (selectedDate == element.Placeholder)
                {
                    Control.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            };
        }
    }
}