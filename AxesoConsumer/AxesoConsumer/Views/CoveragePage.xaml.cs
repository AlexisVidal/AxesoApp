using Syncfusion.SfRangeSlider.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AxesoConsumer.Helpers;

namespace AxesoConsumer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoveragePage : ContentPage
    {
        private double StepValue;
        public CoveragePage()
        {
            try
            {
                StepValue = 1.0;
                
                InitializeComponent();
                KmSlider.Value = (Settings.Distancia /1000);
            }
            catch (Exception ex)
            {

            }

        }

        private void KmSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var newStep = Math.Round(e.NewValue / StepValue);
            KmSlider.Value = newStep * StepValue;
            LabelKm.Text = KmSlider.Value.ToString() + " Km";
            Settings.Distancia = (KmSlider.Value * 1000);
        }
        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
        async void AcceptCoverage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
            
        }

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}