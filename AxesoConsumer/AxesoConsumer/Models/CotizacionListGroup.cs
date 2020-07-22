using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AxesoConsumer.Models
{
    public class CotizacionListGroup : List<CotizacionList>
    {
        public string Title { get; private set; }

        private CotizacionListGroup(string title)
        {
            Title = title;
        }
        public static IList<CotizacionListGroup> All { private set; get; }
    }
}
