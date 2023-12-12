using CasoEstudio2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CasoEstudio2.Controllers
{
    public class CasaController : Controller
    {
        CasasModel c =new CasasModel();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ConsultarCasas()
        {
            var datos = c.ConsultarCasas();
            return View(datos);
        }

        public ActionResult ConsultaPrecio(long q)
        {
            var datos = c.ConsultaPrecio(q);
            return Json(datos, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult ActualizarCasas()
        {
            ViewBag.Productos = c.LitemPrincipal();
            return View();
        }

        [HttpPost]
        public ActionResult ActualizarCasas(CasaEnt entidad)
        {
            int respuesta = c.ActualizarCasas(entidad);

            if (respuesta == 1)
            {
                return RedirectToAction("ConsultarCasas", "Casa");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido registrar su información";
                ViewBag.Productos = c.LitemPrincipal();
                return View();
            }
        }


    }
}