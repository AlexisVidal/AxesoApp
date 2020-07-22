using AxesoConsumer.Helpers;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoveragePopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        private double StepValue;
        public CoveragePopUpPage()
        {
            try
            {
                StepValue = 1.0;
                InitializeComponent();
                KmSliderPop.Value = 2;
            }
            catch (Exception ex)
            {

            }
        }

        private void KmSliderPop_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            try
            {
                if (e.OldValue > 0)
                {
                    var newStep = Math.Round(e.NewValue / StepValue);
                    KmSliderPop.Value = newStep * StepValue;

                    LabelKmPop.Text = KmSliderPop.Value.ToString() + " Km";
                    Settings.Distancia = (KmSliderPop.Value * 1000);
                }
            }
            catch (Exception ex)
            {

            }
        }

        async void AcceptCoveragePop_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }
    }
}