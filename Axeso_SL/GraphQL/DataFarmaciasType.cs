using Axeso_BE;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axeso_SL.GraphQL
{
    public class DataFarmaciasType : ObjectGraphType<DataFarmacias>
    {
        public DataFarmaciasType()
        {
            Field(a => a.DataFarmaciasID);
            Field(a => a.Ruc);
            Field(a => a.Razon_social);
            Field(a => a.Direccion);
            Field(a => a.PaisID);
            Field(a => a.TipoUbigeoID);
            Field(a => a.UbigeoID);
            Field(a => a.Departamento);
            Field(a => a.Provincia);
            Field(a => a.Distrito);
            Field(a => a.Latitud);
            Field(a => a.Longitud);
        }
    }
}
