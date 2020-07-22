using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AxesoConsumer.Droid.Renderers;
using AxesoConsumer.Renderers;

[assembly:ExportRenderer(typeof(BorderlessPicker),typeof(BorderlessPickerAndroid))]
namespace AxesoConsumer.Droid.Renderers
{
    public class BorderlessPickerAndroid : PickerRenderer
    {
        public BorderlessPickerAndroid(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
            }
        }
    }
}