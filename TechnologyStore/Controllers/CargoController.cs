using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyStore.Models;
using Newtonsoft.Json;
using TechnologyStore.Utility;

namespace TechnologyStore.Controllers
{
    public class CargoController : Controller
    {

        TechnologyDataEntities bd = new TechnologyDataEntities();

        // GET: Cargo
        public ActionResult ListaCargos()
        {
            string url = Constante.root + "cargo/listCargo";
            var json = new WebClient().DownloadString(url);
            dynamic m = JsonConvert.DeserializeObject(json);
            List<Cargo> list = new List<Cargo>();
            foreach (var i in m)
            {
                Cargo p = new Cargo();
                p.idCargo = i[0];
                p.nomCargo = i[1];
                list.Add(p);
            }
            TempData["prod"] = null;
            //return View(bd.usp_adm_cargo_listar());
            return View(list);
        }

        public ActionResult SaveCargo()
        {
            return View(new Cargo());
        }

        [HttpPost]
        public ActionResult SaveCargo(Cargo c)
        {
            if (ModelState.IsValid)
            {
                bd.usp_adm_cargo_registrar(c.nomCargo);
                return RedirectToAction("ListaCargos", "Cargo");
            }
            return View(c);
        }



        public ActionResult UpdateCargo(int id)
        {
            Cargo c = bd.Cargo.Where(x => x.idCargo == id).ToList().First();
            TempData["prod"] = null;
            return View(c);
        }

        [HttpPost]
        public ActionResult UpdateCargo(Cargo c)
        {

            if (!ModelState.IsValid)
            {
                return View(c);
            }
            Cargo cargo = (Cargo)Session["cargo"];
            cargo.nomCargo = c.nomCargo;

            bd.usp_adm_cargo_actualizar(cargo.idCargo, cargo.nomCargo);

            Session["cargo"] = cargo;

            return RedirectToAction("ListaCargos", "Cargo");
        }
    }
}