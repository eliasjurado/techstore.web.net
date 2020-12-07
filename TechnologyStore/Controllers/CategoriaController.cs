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
    public class CategoriaController : Controller
    {

        TechnologyDataEntities bd = new TechnologyDataEntities();

        // GET: Categoria
        public ActionResult ListaCategorias()
        {
            string url = Constante.root + "categoria/listCategoria";
            var json = new WebClient().DownloadString(url);
            dynamic m = JsonConvert.DeserializeObject(json);
            List<Categoria> list = new List<Categoria>();
            foreach (var i in m)
            {
                Categoria p = new Categoria();
                p.idCategoria = i[0];
                p.nomCategoria = i[1];
                list.Add(p);
            }
            //return View(bd.usp_adm_categoria_listar());
            return View(list);
        }

        public ActionResult SaveCategoria()
        {
            return View(new Categoria());
        }

        [HttpPost]
        public ActionResult SaveCategoria(Categoria c)
        {
            if (ModelState.IsValid)
            {
                bd.usp_adm_categoria_registrar(c.nomCategoria);
                return RedirectToAction("ListaCategorias", "Categoria");
            }
            return View(c);
        }
    }
}