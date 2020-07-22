
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using AxesoConsumer.iOS.Renderers;
using AxesoConsumer.Renderers;
[assembly:ExportRenderer(typeof(BorderlessEditor),typeof(BorderlessEditorRenderer))]
namespace AxesoConsumer.iOS.Renderers
{
  public  class BorderlessEditorRenderer : EditorRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control == null) return;
            Control.Layer.BorderWidth = 0;
            
        }
    }
}