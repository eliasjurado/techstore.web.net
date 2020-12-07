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
    public class DistritoController : Controller
    {

        TechnologyDataEntities bd = new TechnologyDataEntities();

        // GET: Distrito
        public ActionResult ListaDistritos()
        {
            string url = Constante.root + "distrito/listDistrito";
            var json = new WebClient().DownloadString(url);
            dynamic m = JsonConvert.DeserializeObject(json);
            List<Distrito> list = new List<Distrito>();
            foreach (var i in m)
            {
                Distrito p = new Distrito();
                p.idDistrito = i[0];
                p.nomDistrito = i[1];
                list.Add(p);
            }
            //return View(bd.usp_adm_distrito_listar());
            return View(list);
        }


        public ActionResult SaveDistrito()
        {
            return View(new Distrito());
        }

        [HttpPost]
        public ActionResult SaveDistrito(Distrito d)
        {
            if (ModelState.IsValid)
            {
                bd.usp_adm_distrito_registrar(d.nomDistrito);
                return RedirectToAction("ListaDistritos", "Distrito");
            }
            return View(d);
        }
    }
}