using Axeso_DA;
using Axeso_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BL
{
    public class MonedaBL
    {
        private MonedaDA monedaDAL = new MonedaDA();

        public List<Moneda> Listar()
        {
            return monedaDAL.Listar();
        }
    }
}
