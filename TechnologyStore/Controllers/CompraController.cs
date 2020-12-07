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
    public class CompraController : Controller
    {

        TechnologyDataEntities bd = new TechnologyDataEntities();


        // GET: Compra
        public ActionResult ListaCompra()
        {
            string url = Constante.root + "compra/admListCompra";
            var json = new WebClient().DownloadString(url);
            dynamic m = JsonConvert.DeserializeObject(json);
            List<usp_adm_compra_lista_Result> list = new List<usp_adm_compra_lista_Result>();
            foreach (var i in m)
            {
                usp_adm_compra_lista_Result p = new usp_adm_compra_lista_Result();
                p.idCompra = i[0];
                p.nomCliente = i[1];
                p.apeCliente = i[2];
                long f1 = i[3];
                p.fec_ped_comp = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds((long)i[3] / 1000).ToLocalTime();
                p.fec_ent_comp = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds((long)i[4] / 1000).ToLocalTime();
                p.desProducto = i[5];
                p.est_comp = i[6];
                list.Add(p);
            }
            TempData["cliente"] = bd.Cliente.ToList();
            TempData["producto"] = bd.Producto.ToList();
            TempData["prod"] = null;
            //return View(bd.usp_adm_compra_lista());
            return View(list);
        }
    }
}