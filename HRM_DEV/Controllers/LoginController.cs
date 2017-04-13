using HRM_DEV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRM_DEV.Controllers
{
    public class LoginController : Controller
    {

        private hrm_dbEntities db = new hrm_dbEntities();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Cuenta(string NOMBRE_USUARIO, string CONTRASEÑA)
        {
            var usuarios = from e in db.USUARIOS
                               select e;

            usuarios = usuarios.Where(s => (s.NOMBRE_USUARIO.Equals(NOMBRE_USUARIO)));

            if (usuarios != null)
            {
                if (usuarios.First().CONTRASEÑA.Equals(CONTRASEÑA))
                {
                    Session["usuarios"] = usuarios.First();
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Login", "Login");
        }
    }
}