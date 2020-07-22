using Axeso_BL;
using AxesoWeb.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AxesoWeb.Controllers
{
    [SessionAuthorize]
    public class UsuarioController : Controller
    {
        private ModelsBL usuarioBL = new ModelsBL();
        // GET: Usuario
        public async Task<ActionResult> Index()
        {
            var entidad = await usuarioBL.Listar();
            return View(entidad);
        }
    }
}