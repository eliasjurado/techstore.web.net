using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TechnologyStore.Models;

namespace TechnologyStore.Controllers
{
    public class ClienteController : Controller
    {

        TechnologyDataEntities db = new TechnologyDataEntities();

        // GET: Cliente


        public ActionResult RegistrarCliente()
        {
            ViewBag.ciudades = new SelectList(db.Distrito.ToList().OrderBy(x => x.nomDistrito), "idDistrito", "nomDistrito");
            return View(new Cliente());
        }

        [HttpPost]
        public ActionResult RegistrarCliente(Cliente c)
        {
            if (ModelState.IsValid)
            {
                db.usp_registrar_cliente(c.nomCliente, c.apeCliente, c.dniCliente, c.tlfCliente, c.direcCliente, c.idDistrito, c.emailCliente, c.passCliente);
                return RedirectToAction("LoginCliente", "Login");
            }
            ViewBag.ciudades = new SelectList(db.Distrito.ToList().OrderBy(x => x.nomDistrito), "idDistrito", "nomDistrito", c.idDistrito);
            return View(c);
        }

        public ActionResult CuentaCliente()
        {
            if (Session["cliente"] != null)
            {
                Cliente c = (Cliente)Session["cliente"];

                TempData["prod"] = null;
                return View(c);
            }
            else
            {
                return RedirectToAction("ListadoProductos", "Producto");
            }
        }

        public ActionResult EditarCliente(int id)
        {
            Cliente c = db.Cliente.Where(x => x.idCliente == id).ToList().First();
            ViewBag.ciudades = new SelectList(db.Distrito.ToList().OrderBy(x => x.nomDistrito), "idDistrito", "nomDistrito", c.idDistrito);
            TempData["prod"] = null;
            return View(c);
        }

        [HttpPost]
        public ActionResult EditarCliente(Cliente c)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ciudades = new SelectList(db.Distrito.ToList().OrderBy(x => x.nomDistrito), "idDistrito", "nomDistrito", c.idDistrito);
                return View(c);
            }
            Cliente cli = (Cliente)Session["cliente"];
            cli.nomCliente = c.nomCliente;
            cli.apeCliente = c.apeCliente;
            cli.idDistrito = c.idDistrito;
            cli.direcCliente = c.direcCliente;
            cli.tlfCliente = c.tlfCliente;
            cli.passCliente = c.passCliente;

            db.usp_editar_cliente(cli.idCliente, cli.nomCliente, cli.apeCliente, cli.idDistrito, cli.direcCliente, cli.tlfCliente, cli.passCliente);

            Session["cliente"] = cli;

            return RedirectToAction("CuentaCliente");
        }
        
        public ActionResult CerrarSesionCliente()
        {
            if (Session["cliente"] != null)
            {
                Session["cliente"] = null;
                Session["carrito"] = null;
                TempData["prod"] = null;
                return RedirectToAction("ListadoProductos", "Producto");
            }
            return RedirectToAction("ListadoProductos", "Producto");
        }





        //Cliente - Admin


        public ActionResult ListaClienteToAdmin()
        {
            TempData["distrito"] = db.Distrito.ToList();
            TempData["prod"] = null;
            return View(db.usp_adm_cliente_listar());
        }








    }
}