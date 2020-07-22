using Axeso_DA;
using Axeso_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_BL
{
    public class CulturaBL
    {
        // este es otro comentario en otro archivo
        private CulturaDA culturaDAL = new CulturaDA();
        public async Task<List<Cultura>> Listar()
        {
            return await culturaDAL.GetCulturas();
        }
        public async Task<Cultura> GetById(int id)
        {
            return await culturaDAL.Get(id);
        }
    }
}
