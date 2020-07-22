using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Badge.Droid;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(BadgedTabbedPageRenderer))]
namespace AxesoConsumer.Droid
{
    class MyRenderer : BadgedTabbedPageRenderer
    {
        public MyRenderer(Context context) : base(context)

        {

        }
    }
}