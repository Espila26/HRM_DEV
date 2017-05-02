using HRM_DEV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRM_DEV.Controllers
{
    public class HomeController : Controller
    {

        private hrm_dbEntities db = new hrm_dbEntities();
        private Correo correo;
        

        public ActionResult Index()
        {
            var EMP = from e in db.EMPLEADOS
                      select e;

            EMP = EMP.Where(s => s.FECHA_NAC.CompareTo(System.DateTime.Today) == 0 || s.FECHA_CONTR.CompareTo(System.DateTime.Today) == 0);//Empleados que cumplen anos el dia actual

            Session["cumpleaneros"] = EMP;


            return View(EMP);
        }

        public ActionResult EnviarCorreos()
        {
            correo = new Correo();
            var ListaEmpleados = (IQueryable<EMPLEADOS>)Session["cumpleaneros"];

            foreach (var e in ListaEmpleados)
            {
                if (e.FECHA_NAC.CompareTo(@DateTime.Today) == 0)
                    correo.enviarCorreo("Cumpleaños", "Prueba de Correo HR-Manager", e.E_MAIL);
            }
            return RedirectToAction("Index");
        }

        public ActionResult EnviarCorreos2()
        {
            correo = new Correo();
            var ListaEmpleados = (IQueryable<EMPLEADOS>)Session["cumpleaneros"];
            foreach (var e in ListaEmpleados)
            {
                if (e.FECHA_CONTR.CompareTo(@DateTime.Today) == 0)
                    correo.enviarCorreo("Felicitaciones", "Prueba de Correo HR-Manager", e.E_MAIL);
            }
            return RedirectToAction("Index");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}