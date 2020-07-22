using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace AxesoConsumer.Controls
{
    public class CustomMapTwo : Map
    {
        public List<CustomPinTwo> CustomPins { get; set; }
        public CustomCircle Circle { get; set; }
    }
}
