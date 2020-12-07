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
    public class EmpleadoController : Controller
    {

        TechnologyDataEntities bd = new TechnologyDataEntities();

        // GET: Empleado
        public ActionResult ListadoEmpleados()
        {
            string url = Constante.root + "empleado/listEmpleado";
            var json = new WebClient().DownloadString(url);
            dynamic m = JsonConvert.DeserializeObject(json);
            List<usp_adm_listar_empleados_Result> list = new List<usp_adm_listar_empleados_Result>();

            foreach (var i in m)
            {
                usp_adm_listar_empleados_Result p = new usp_adm_listar_empleados_Result();
                p.idEmpleado = i[0];
                p.nomEmpleado = i[1];
                p.apeEmpleado = i[2];
                p.dniEmpleado = i[3];
                p.tlfEmpleado = i[4];
                p.direcEmpleado = i[5];
                p.nomDistrito = i[6];
                p.nomCargo = i[7];
                p.emailEmpleado = i[8];
                p.passEmpleado = i[9];
                p.nomTipoUsuario = i[10];
                list.Add(p);
            }
            TempData["distrito"] = bd.Distrito.ToList();
            TempData["cargo"] = bd.Cargo.ToList();
            TempData["tipousu"] = bd.Tipo_Usuario.ToList();
            TempData["prod"] = null;
            //return View(bd.usp_adm_listar_empleados());
            return View(list);
        }


        public ActionResult SaveEmpleado()
        {
            ViewBag.distrito = new SelectList(bd.Distrito.ToList().OrderBy(x => x.nomDistrito), "idDistrito", "nomDistrito");
            ViewBag.cargo = new SelectList(bd.Cargo.ToList().OrderBy(x => x.nomCargo), "idCargo", "nomCargo");
            ViewBag.tipousuario = new SelectList(bd.Tipo_Usuario.ToList().OrderBy(x => x.nomTipoUsuario), "idTipoUsuario", "nomTipoUsuario");
            return View(new Empleado());
        }

        [HttpPost]
        public ActionResult SaveEmpleado(Empleado e)
        {
            if (ModelState.IsValid)
            {
                bd.usp_adm_registrar_empleado(e.nomEmpleado, e.apeEmpleado, e.dniEmpleado, e.tlfEmpleado, e.direcEmpleado, e.idDistrito, e.idCargo, e.emailEmpleado, e.passEmpleado, e.idTipoUsuario);
                return RedirectToAction("ListadoEmpleados", "Empleado");
            }
            ViewBag.distrito = new SelectList(bd.Distrito.ToList().OrderBy(x => x.nomDistrito), "idDistrito", "nomDistrito", e.idDistrito);
            ViewBag.cargo = new SelectList(bd.Cargo.ToList().OrderBy(x => x.nomCargo), "idCargo", "nomCargo", e.idCargo);
            ViewBag.tipousuario = new SelectList(bd.Tipo_Usuario.ToList().OrderBy(x => x.nomTipoUsuario), "idTipoUsuario", "nomTipoUsuario", e.idTipoUsuario);

            return View(e);
        }
    }
}