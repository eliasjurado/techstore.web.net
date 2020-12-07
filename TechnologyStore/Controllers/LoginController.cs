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
    public class LoginController : Controller
    {

        TechnologyDataEntities bd = new TechnologyDataEntities();

        // GET: Login
        public ActionResult LoginCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginCliente(Cliente c)
        {
            try
            {

                if (Session["cliente"] == null)
                {
                    Cliente cli = new Cliente();
                    Session["cliente"] = cli;
                }

                string url = Constante.root + "cliente/login/"+c.emailCliente+"/"+c.passCliente;
                var json = new WebClient().DownloadString(url);
                dynamic m = JsonConvert.DeserializeObject(json);
                Cliente obj = new Cliente();
                obj.idCliente= m.idcliente;
                obj.nomCliente = m.nomcliente;
                obj.apeCliente = m.apecliente;
                obj.dniCliente = m.dnicliente;
                obj.tlfCliente = m.tlfcliente;
                obj.direcCliente = m.direccliente;
                obj.idDistrito = m.iddistrito;
                obj.emailCliente = m.emailcliente;
                obj.passCliente = m.passcliente;
                obj.idTipoUsuario = m.idtipousuario;

                Session["cliente"] = obj;

                if (TempData["prod"] == null)
                {
                    return RedirectToAction("ListadoProductos", "Producto");
                }

                return RedirectToAction("DetalleProducto", "Producto", new { codProd = TempData["prod"] });
            }
            catch
            {
                return View(c);
            }
            
        }



        /*
         Login de Administrador
         */


        public ActionResult LoginAdministrador()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAdministrador(Empleado e)
        {
            try
            {
                if (Session["administrador"] == null)
                {
                    Empleado emp = new Empleado();
                    Session["administrador"] = emp;
                }

                //Empleado a = bd.Empleado.Where(x => x.emailEmpleado == e.emailEmpleado && x.passEmpleado == e.passEmpleado).First();
                string url = Constante.root + "empleado/login/" + e.emailEmpleado + "/" + e.passEmpleado;
                var json = new WebClient().DownloadString(url);
                dynamic m = JsonConvert.DeserializeObject(json);
                Empleado obj = new Empleado();
                obj.idEmpleado = m.idempleado;
                obj.nomEmpleado = m.nomempleado;
                obj.apeEmpleado = m.apeempleado;
                obj.dniEmpleado = m.dniempleado;
                obj.tlfEmpleado = m.tlfempleado;
                obj.direcEmpleado = m.direcempleado;
                obj.idDistrito = m.iddistrito;
                obj.idCargo = m.idcargo;
                obj.emailEmpleado = m.emailempleado;
                obj.passEmpleado = m.passempleado;
                obj.idTipoUsuario = m.idtipousuario;

                Session["administrador"] = obj;
                
                if (TempData["prod"] == null)
                {
                    Session["adminName"] = obj.nomEmpleado;
                    Session["adminApe"] = obj.apeEmpleado;
                    return RedirectToAction("PageAdministrador", "Administrador");
                }

                return RedirectToAction("PageAdministrador", "Administrador", new { codProd = TempData["prod"] });
            }
            catch
            {
                return View(e);
            }
        }




        public ActionResult CerrarSesionAdmin()
        {
            if (Session["administrador"] != null)
            {
                Session["administrador"] = null;
                TempData["prod"] = null;
                return RedirectToAction("LoginAdministrador", "Login");
            }
            return RedirectToAction("LoginAdministrador", "Login");
        }
    }
}