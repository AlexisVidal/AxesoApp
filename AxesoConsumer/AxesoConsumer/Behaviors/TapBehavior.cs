using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AxesoConsumer.Behaviors
{
    public class TapBehavior : BehaviorBase<View>
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(TapBehavior), null, BindingMode.Default, propertyChanged: CommandPropertyChanged);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        private static void CommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is TapBehavior behavior)) return;
            behavior.SetTapOnView();
        }

        private void SetTapOnView()
        {
            var tap = new TapGestureRecognizer();
            tap.Tapped += (sender, args) => { Command?.Execute(null); };

            AssociatedObject.GestureRecognizers.Add(tap);
        }
    }
}
