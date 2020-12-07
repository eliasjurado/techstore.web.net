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
    public class ProductoController : Controller
    {

        TechnologyDataEntities bd = new TechnologyDataEntities();

        // GET: Cliente
        public ActionResult ListadoProductos()
        {

            string url = Constante.root + "producto/listProducto";
            var json = new WebClient().DownloadString(url);
            dynamic m = JsonConvert.DeserializeObject(json);
            List<usp_listar_productos_Result> list= new List<usp_listar_productos_Result>();

            foreach (var i in m)
            {
                usp_listar_productos_Result p = new usp_listar_productos_Result();
                p.idProducto = i[0];
                p.desProducto = i[1];
                p.precioProducto = i[2];
                p.idCategoria = i[3];
                p.nomCategoria = i[4];
                p.stock_act = i[5];
                list.Add(p);
            }
            TempData["categorias"] = bd.Categoria.ToList();
            TempData["prod"] = null;
            //var info = bd.usp_listar_productos();
            return View(list);
        }

        public ActionResult DetalleProducto(int idProducto)
        {
            TempData["prod"] = idProducto;
            return View(bd.Producto.Where(x => x.idProducto == idProducto).First());
        }

        public ActionResult ElegirProducto(int id, int cant = 1)
        {
            if (Session["cliente"] == null)
            {
                return RedirectToAction("LoginCliente", "Login");
            }

            Cliente cli = (Cliente)Session["cliente"];
            Producto p = bd.Producto.Where(x => x.idProducto == id).First();

            if (Session["carrito"] == null)
            {
                List<Compra_Detalle> carrito = new List<Compra_Detalle>();
                Session["carrito"] = carrito;
            }

            Compra_Detalle cd = new Compra_Detalle();
            cd.idCompra = 0;
            cd.idProducto = p.idProducto;
            cd.Producto = new Producto();
            cd.Producto.Categoria = new Categoria();
            cd.Producto.desProducto = p.desProducto;
            cd.Producto.precioProducto = p.precioProducto;
            cd.Producto.Categoria = bd.Categoria.Where(x => x.idCategoria == p.idCategoria).ToList().First();
            cd.cantidad = cant;

            List<Compra_Detalle> sesion = (List<Compra_Detalle>)Session["carrito"];
            sesion.Add(cd);

            Session["carrito"] = sesion;

            TempData["prod"] = null;
            return RedirectToAction("ListadoProductos");
        }

        public ActionResult QuitarProducto(int id)
        {
            List<Compra_Detalle> cd = (List<Compra_Detalle>)Session["carrito"];
            cd.Remove(cd.Where(x => x.idProducto == id).ToList().First());

            Session["carrito"] = cd;

            TempData["prod"] = null;
            return RedirectToAction("ListadoCarrito");
        }

        public ActionResult ListadoCarrito()
        {
            return View((List<Compra_Detalle>)Session["carrito"]);
        }


        public ActionResult RealizarCompra()
        {
            string url = Constante.root + "/compra/saveCompraCab/";

            if (Session["cliente"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            Cliente cli = (Cliente)Session["cliente"];

            

            bd.usp_transaccion_compra_cabecera(0, cli.idCliente, DateTime.Now, DateTime.Now.AddDays(7), 0, 0);

            List<Compra_Detalle> sesion = (List<Compra_Detalle>)Session["carrito"];

            Compra_Cabecera cc = null;

            try
            {
                cc = bd.Compra_Cabecera.ToList().Last();
            }
            catch
            {
                cc.idCompra = 0;
            }

            foreach (var x in sesion)
            {
                bd.usp_insertar_compra_detalle(cc.idCompra, x.idProducto, x.cantidad);
            }

            Session["carrito"] = null;

            TempData["prod"] = null;
            return RedirectToAction("ListadoProductos");
        }

        public ActionResult CancelarCompra(int id)
        {
            bd.usp_transaccion_compra_cabecera(id, null, null, null, 2, 1);
            TempData["prod"] = null;
            return RedirectToAction("CuentaCliente", "Cliente");
        }











        //Producto - Admin


        public ActionResult ListaProductoToAdmin()
        {
            TempData["categoria"] = bd.Categoria.ToList();
            TempData["prod"] = null;
            return View(bd.usp_adm_producto_listar());
        }




        public ActionResult SaveProductoToAdmin()
        {
            ViewBag.categoria = new SelectList(bd.Categoria.ToList().OrderBy(x => x.nomCategoria), "idCategoria", "nomCategoria");
            return View(new Producto());
        }

        [HttpPost]
        public ActionResult SaveProductoToAdmin(Producto p)
        {
            if (ModelState.IsValid)
            {
                bd.usp_adm_producto_registrar(p.desProducto, p.precioProducto, p.stock_act, p.stock_min, p.idCategoria);
                return RedirectToAction("ListaProductoToAdmin", "Producto");
            }
            ViewBag.categoria = new SelectList(bd.Categoria.ToList().OrderBy(x => x.nomCategoria), "idCategoria", "nomCategoria", p.idCategoria);
            return View(p);
        }

       

    }
}