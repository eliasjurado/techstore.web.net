using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechnologyStore.Models;
using TechnologyStore.Utility;

namespace TechnologyStore.Controllers
{
    public class TipoUsuarioController : Controller
    {
        TechnologyDataEntities bd = new TechnologyDataEntities();


        // GET: TipoUsuario
        public ActionResult ListaTipoUsuario()
        {
            string url = Constante.root + "tipoUsuario/listTipoUsuario";
            var json = new WebClient().DownloadString(url);
            dynamic m = JsonConvert.DeserializeObject(json);
            List<Tipo_Usuario> list = new List<Tipo_Usuario>();
            foreach (var i in m)
            {
                Tipo_Usuario p = new Tipo_Usuario();
                p.idTipoUsuario = i[0];
                p.nomTipoUsuario = i[1];
                list.Add(p);
            }
            //return View(bd.usp_adm_tipousuario_listar());
            return View(list);
        }


        public ActionResult SaveTipoUsuario()
        {
            return View(new Tipo_Usuario());
        }

        [HttpPost]
        public ActionResult SaveTipoUsuario(Tipo_Usuario t)
        {
            if (ModelState.IsValid)
            {
                bd.usp_adm_tipousuario_registrar(t.nomTipoUsuario);
                return RedirectToAction("ListaTipoUsuario", "TipoUsuario");
            }
            return View(t);
        }
    }
}