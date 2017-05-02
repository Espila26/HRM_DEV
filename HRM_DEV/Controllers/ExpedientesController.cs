﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRM_DEV.Models;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HRM_DEV.Controllers
{
    public class ExpedientesController : Controller
    {
        private hrm_dbEntities db = new hrm_dbEntities();
        /*********************************************************  Vacaciones  *********************************************************************/
        /*******************************************************************************************************************************************/
        // GET: VACACIONES
        public ActionResult Index()
        {
            USUARIOS varUser = (USUARIOS)Session["usuarios"];

            if (Session["usuarios"] != null && varUser.ACC_ACCIONES.Equals("Si"))
            {
                EMPLEADOS Emp = (EMPLEADOS)TempData["Empleado"];
                TempData.Keep("Empleado");
                var vACACIONES = db.VACACIONES.Where(v => v.ID_EMPLEADO.Equals(Emp.EMP_ID));
                return View("Index", vACACIONES);
            }

            TempData["Error"] = "¡No tiene acceso al modulo selccionado!";
            return RedirectToAction("Index", "Home");

        }

        // GET: VACACIONES/Create
        public ActionResult Create()
        {
            USUARIOS varUser = (USUARIOS)Session["usuarios"];

            if (Session["usuarios"] != null && varUser.ACC_ACCIONES.Equals("Si"))
            {
                TempData.Keep("Empleado");
                ViewBagEmpleado();
                ViewBag.AUTORIZACION = new SelectList(db.PUESTOS, "PTS_ID", "ID_PUESTO");
                return View();
            }

            TempData["Error"] = "¡No tiene acceso al modulo selccionado!";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ValidateIDEmp()
        {
            USUARIOS varUser = (USUARIOS)Session["usuarios"];

            if (Session["usuarios"] != null && varUser.ACC_ACCIONES.Equals("Si"))
            {
                if (TempData["Empleado"] == null)
                {
                    TempData["Error"] = "¡Seleccione un empleado!";
                    return RedirectToAction("Expediente", "Expedientes");
                }
                else
                {
                    //TempData["Success"] = "¡Seleccione un empleado!";
                    return RedirectToAction("Index", "Expedientes");
                }
            }

            TempData["Error"] = "¡No tiene acceso al modulo selccionado!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "ID_SOLICITUD,ID_EMPLEADO,INICIO,FINAL,CANT_DIAS,AUTORIZACION,FECHA_CREACION")] VACACIONES vACACIONES)
        {
            if (ModelState.IsValid)
            {
                var empleado = db.EMPLEADOS.Find(vACACIONES.ID_EMPLEADO);
                TimeSpan ts = vACACIONES.FINAL - vACACIONES.INICIO;
                vACACIONES.CANT_DIAS = ts.Days;
                empleado.DIAS_VAC_UTILIZAD = empleado.DIAS_VAC_UTILIZAD + vACACIONES.CANT_DIAS;
                vACACIONES.FECHA_CREACION = DateTime.Now;
                db.VACACIONES.Add(vACACIONES);
                db.SaveChanges();
                ViewBagEmpleado();
                return RedirectToAction("Create");
            }
            ViewBagEmpleado();
            ViewBag.AUTORIZACION = new SelectList(db.PUESTOS, "PTS_ID", "ID_PUESTO", vACACIONES.AUTORIZACION);
            return View();
        }

        /*********************************************************  Suspenciones  *******************************************************************/
        /*******************************************************************************************************************************************/

        public ActionResult CreateSusp()
        {
            USUARIOS varUser = (USUARIOS)Session["usuarios"];

            if (Session["usuarios"] != null && varUser.ACC_ACCIONES.Equals("Si"))
            {
                TempData.Keep("Empleado");
                ViewBagEmpleado();
                return View();
            }

            TempData["Error"] = "¡No tiene acceso al modulo selccionado!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult CreateSusp([Bind(Include = "ID_SUSPENSION,ID_EMPLEADO,INICIO,FINAL,DESCRIPCION,GOCE_SALARIO,AUTORIZACION")] SUSPENSIONES sUSPENSIONES)
        {
            if (ModelState.IsValid)
            {
                sUSPENSIONES.FECHA_CREACION = System.DateTime.Now;
                db.SUSPENSIONES.Add(sUSPENSIONES);

                db.SaveChanges();
                return RedirectToAction("CreateSusp");
            }

            ViewBagEmpleado();
            return View(sUSPENSIONES);
        }

        public ActionResult IndexSusp()
        {
            USUARIOS varUser = (USUARIOS)Session["usuarios"];

            if (Session["usuarios"] != null && varUser.ACC_ACCIONES.Equals("Si"))
            {
                EMPLEADOS Emp = (EMPLEADOS)TempData["Empleado"];
                TempData.Keep("Empleado");
                var sUSPENSIONES = db.SUSPENSIONES.Where(v => v.ID_EMPLEADO.Equals(Emp.EMP_ID));
                return View("IndexSusp", sUSPENSIONES);
            }

            TempData["Error"] = "¡No tiene acceso al modulo selccionado!";
            return RedirectToAction("Index", "Home");

        }

        public ActionResult ValidateIDEmpSup()
        {
            USUARIOS varUser = (USUARIOS)Session["usuarios"];

            if (Session["usuarios"] != null && varUser.ACC_ACCIONES.Equals("Si"))
            {
                if (TempData["Empleado"] == null)
                {
                    TempData["Error"] = "¡Seleccione un empleado!";
                    return RedirectToAction("Expediente", "Expedientes");
                }
                else
                {
                    //TempData["Success"] = "¡Seleccione un empleado!";
                    return RedirectToAction("IndexSusp", "Expedientes");
                }
            }

            TempData["Error"] = "¡No tiene acceso al modulo selccionado!";
            return RedirectToAction("Index", "Home");
        }

        /**********************************************************   Permisos   ********************************************************************/
        /*******************************************************************************************************************************************/

        public ActionResult CreatePerm()
        {
            USUARIOS varUser = (USUARIOS)Session["usuarios"];

            if (Session["usuarios"] != null && varUser.ACC_ACCIONES.Equals("Si"))
            {
                TempData.Keep("Empleado");
                ViewBagEmpleado();
                return View();
            }

            TempData["Error"] = "¡No tiene acceso al modulo selccionado!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult CreatePerm([Bind(Include = "ID_PERMISO,ID_EMPLEADO,INICIO,FINAL,GOCE_SALARIO,CANT_HORAS,CANT_DIAS,AUTORIZACION")] PERMISOS pERMISOS)
        {
            if (ModelState.IsValid)
            {
                pERMISOS.FECHA_CREACION = System.DateTime.Now;
                db.PERMISOS.Add(pERMISOS);
                db.SaveChanges();
                return RedirectToAction("CreatePerm");
            }

            ViewBagEmpleado();
            return View(pERMISOS);
        }

        public ActionResult IndexPerm()
        {
            USUARIOS varUser = (USUARIOS)Session["usuarios"];

            if (Session["usuarios"] != null && varUser.ACC_ACCIONES.Equals("Si"))
            {
                EMPLEADOS Emp = (EMPLEADOS)TempData["Empleado"];
                TempData.Keep("Empleado");
                var pERMISOS = db.PERMISOS.Where(v => v.ID_EMPLEADO.Equals(Emp.EMP_ID));
                return View("IndexPerm", pERMISOS);
            }

            TempData["Error"] = "¡No tiene acceso al modulo selccionado!";
            return RedirectToAction("Index", "Home");

        }

        public ActionResult ValidateIDEmpPerm()
        {
            USUARIOS varUser = (USUARIOS)Session["usuarios"];

            if (Session["usuarios"] != null && varUser.ACC_ACCIONES.Equals("Si"))
            {
                if (TempData["Empleado"] == null)
                {
                    TempData["Error"] = "¡Seleccione un empleado!";
                    return RedirectToAction("Expediente", "Expedientes");
                }
                else
                {
                    //TempData["Success"] = "¡Seleccione un empleado!";
                    return RedirectToAction("IndexPerm", "Expedientes");
                }
            }

            TempData["Error"] = "¡No tiene acceso al modulo selccionado!";
            return RedirectToAction("Index", "Home");
        }

        /**********************************************************   Ascensos   ********************************************************************/
        /*******************************************************************************************************************************************/

        public ActionResult CreateAsc()
        {
            USUARIOS varUser = (USUARIOS)Session["usuarios"];

            if (Session["usuarios"] != null && varUser.ACC_ACCIONES.Equals("Si"))
            {
                ViewBagEmpleado();
                GetPuestoAnt();
                TempData.Keep("Empleado");
                ViewBag.PUESTO_NVO = new SelectList(db.PUESTOS, "PTS_ID", "NOMBRE");
                return View();
            }

            TempData["Error"] = "¡No tiene acceso al modulo selccionado!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult CreateAsc([Bind(Include = "ID_ASCENSO,ID_EMPLEADO,DESCRIPCION,PUESTO_ANT,PUESTO_NVO,FECHA,AUTORIZACION")] ASCENSOS aSCENSOS)
        {
            TempData.Keep("Empleado");
            if (ModelState.IsValid)
            {
                aSCENSOS.FECHA_CREACION = System.DateTime.Now;
                db.ASCENSOS.Add(aSCENSOS);
                EMPLEADOS temp = (EMPLEADOS)TempData["Empleado"];
                var Emp = db.EMPLEADOS.Find(temp.EMP_ID);
                Emp.PUESTO = aSCENSOS.PUESTO_NVO;
                db.SaveChanges();
                return RedirectToAction("CreateAsc");
            }
            GetPuestoAnt();
            ViewBagEmpleado();
            ViewBag.PUESTO_NVO = new SelectList(db.PUESTOS, "PTS_ID", "NOMBRE", aSCENSOS.PUESTO_NVO);
            return View(aSCENSOS);
        }

        public ActionResult IndexAsc()
        {
            USUARIOS varUser = (USUARIOS)Session["usuarios"];

            if (Session["usuarios"] != null && varUser.ACC_ACCIONES.Equals("Si"))
            {
                EMPLEADOS Emp = (EMPLEADOS)TempData["Empleado"];
                TempData.Keep("Empleado");
                var aSCENSOS = db.ASCENSOS.Where(v => v.ID_EMPLEADO.Equals(Emp.EMP_ID));
                return View("IndexAsc", aSCENSOS);
            }

            TempData["Error"] = "¡No tiene acceso al modulo selccionado!";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ValidateIDEmpAsc()
        {
            USUARIOS varUser = (USUARIOS)Session["usuarios"];

            if (Session["usuarios"] != null && varUser.ACC_ACCIONES.Equals("Si"))
            {
                if (TempData["Empleado"] == null)
                {
                    TempData["Error"] = "¡Seleccione un empleado!";
                    return RedirectToAction("Expediente", "Expedientes");
                }
                else
                {
                    //TempData["Success"] = "¡Seleccione un empleado!";
                    return RedirectToAction("IndexAsc", "Expedientes");
                }
            }

            TempData["Error"] = "¡No tiene acceso al modulo selccionado!";
            return RedirectToAction("Index", "Home");
        }

        public void GetPuestoAnt()
        {
            EMPLEADOS temp = (EMPLEADOS)TempData["Empleado"];
            ViewData["PUESTO_ANT"] = temp.PUESTOS.NOMBRE;
        }

        /*******************************************************  Amonestaciones   ******************************************************************/
        /*******************************************************************************************************************************************/

        public ActionResult CreateAmon()
        {
            USUARIOS varUser = (USUARIOS)Session["usuarios"];

            if (Session["usuarios"] != null && varUser.ACC_ACCIONES.Equals("Si"))
            {
                TempData.Keep("Empleado");
                ViewBagEmpleado();
                return View();
            }

            TempData["Error"] = "¡No tiene acceso al modulo selccionado!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult CreateAmon([Bind(Include = "ID_AMONESTACION,ID_EMPLEADO,FECHA_INICIO,FECHA_FINAL,DESCRIPCION,GOCE_SALARIO,VERB_ESC,AUTORIZACION")] AMONESTACIONES aMONESTACIONES)
        {
            if (ModelState.IsValid)
            {
                aMONESTACIONES.FECHA_CREACION = System.DateTime.Now;
                db.AMONESTACIONES.Add(aMONESTACIONES);
                db.SaveChanges();
                return RedirectToAction("CreateAmon");
            }

            ViewBagEmpleado();
            return View(aMONESTACIONES);
        }

        public ActionResult IndexAmon()
        {
            USUARIOS varUser = (USUARIOS)Session["usuarios"];

            if (Session["usuarios"] != null && varUser.ACC_ACCIONES.Equals("Si"))
            {
                EMPLEADOS Emp = (EMPLEADOS)TempData["Empleado"];
                TempData.Keep("Empleado");
                var aMONESTACIONES = db.AMONESTACIONES.Where(v => v.ID_EMPLEADO.Equals(Emp.EMP_ID));
                return View("IndexAmon", aMONESTACIONES);
            }

            TempData["Error"] = "¡No tiene acceso al modulo selccionado!";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ValidateIDEmpAmon()
        {
            USUARIOS varUser = (USUARIOS)Session["usuarios"];

            if (Session["usuarios"] != null && varUser.ACC_ACCIONES.Equals("Si"))
            {
                if (TempData["Empleado"] == null)
                {
                    TempData["Error"] = "¡Seleccione un empleado!";
                    return RedirectToAction("Expediente", "Expedientes");
                }
                else
                {
                    //TempData["Success"] = "¡Seleccione un empleado!";
                    return RedirectToAction("IndexAmon", "Expedientes");
                }
            }

            TempData["Error"] = "¡No tiene acceso al modulo selccionado!";
            return RedirectToAction("Index", "Home");
        }

        /*******************************************************  Datos Personales  *****************************************************************/
        /*******************************************************************************************************************************************/
        public ActionResult Expediente(string searchString)
        {
            USUARIOS varUser = (USUARIOS)Session["usuarios"];

            if (Session["usuarios"] != null && varUser.ACC_ACCIONES.Equals("Si"))
            {
                var EMP = from d in db.EMPLEADOS
                          select d;

                if (!String.IsNullOrEmpty(searchString))
                {
                    EMP = EMP.Where(s => s.CEDULA.Contains(searchString));

                }
                return View("Expediente", EMP);

            }

            TempData["Error"] = "¡No tiene acceso al modulo selccionado!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult formAction(string[] childChkbox)
        {
            if (childChkbox == null)
            {
                TempData["Error"] = "¡Se debe seleccionar al menos un empleado!";
            }
            else
            {
                if (childChkbox.Count() == 1)
                {
                    TempData["Empleado"] = db.EMPLEADOS.Find(Int32.Parse(childChkbox.First()));
                    TempData["Success"] = "¡Se ha seleccionado empleado!";
                }
                else
                {
                    TempData["Error"] = "¡Solamente es posible ver los detalles de un empleado a la vez!";
                }
            }
            return RedirectToAction("Expediente");
        }

        public void ViewBagEmpleado()
        {
            List<EMPLEADOS> Empleado = new List<EMPLEADOS>();
            if (TempData["Empleado"] != null)
            {
                var tempEmpleado = (EMPLEADOS)TempData["Empleado"];
                var empleado = db.EMPLEADOS.Find(tempEmpleado.EMP_ID);
                Empleado.Add(empleado);
                TempData["Empleado"] = empleado;
                CalcularDiasDisponibles(Empleado.First());
                ViewBag.ID_EMPLEADO = new SelectList(Empleado, "EMP_ID", "NOMBRE");
            }
            else
            {
                ViewBag.ID_EMPLEADO = new SelectList(Empleado, "EMP_ID", "NOMBRE");
            }
        }

        public void CalcularDiasDisponibles(EMPLEADOS empleado)
        {
            int annos = DateTime.Now.Year - empleado.FECHA_CONTR.Year;
            int meses = DateTime.Now.Month - empleado.FECHA_CONTR.Month;
            int resMesIncomp = 0;
            if (DateTime.Now.Day < empleado.FECHA_CONTR.Day)
            {
                resMesIncomp = 1;
            }
            int diasDisponibles = (annos * 12) + meses - empleado.DIAS_VAC_UTILIZAD - resMesIncomp;
            ViewBag.DiasDisponibles = diasDisponibles;
        }

        public FileStreamResult ExportarPDF(string exportData)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(exportData);

            using (var input = new MemoryStream(bytes))
            {
                var output = new MemoryStream();
                var document = new Document(PageSize.A4, 50, 50, 50, 50);
                var writer = PdfWriter.GetInstance(document, output);
                Image addLogo = default(Image);
                addLogo = Image.GetInstance(Server.MapPath("~/Imagenes/conexus1.png"));               
                writer.CloseStream = false;
                document.Open();
                addLogo.ScaleToFit(100, 100);
                addLogo.Alignment = Image.ALIGN_RIGHT;
                document.Add(addLogo);
                var xmlWorker = iTextSharp.tool.xml.XMLWorkerHelper.GetInstance();
                xmlWorker.ParseXHtml(writer, document, input, System.Text.Encoding.UTF8);
                document.Close();
                output.Position = 0;
                return new FileStreamResult(output, "application/pdf");
            }
        }

        public ActionResult Comprobante(string[] childChkbox)
        {
            if (ValidaCheckBox(childChkbox))
            {
                string exportData = "";
                int id = Int32.Parse(childChkbox.First());

                if (Request.Form["ComprobanteVacaciones"] != null)
                {
                    var Vacaciones = db.VACACIONES.Find(id);

                    ViewBagEmpleado();
                    TempData.Keep("Empleado");

                    exportData = String.Format("<html><head>Comprobante de Vacaciones</head><body><h1>" +
                                          " Comprobante de Vacaciones</h1><label> Dias Disponibles:" +
                                          "</label>  <p>" + ViewBag.DiasDisponibles + "</p><label>" +
                                          "Empleado: </label><p>" + Vacaciones.EMPLEADOS.NOMBRE
                                          + " " + Vacaciones.EMPLEADOS.APE1 + " " +
                                          Vacaciones.EMPLEADOS.APE2 + " </p><label>Inicio: "
                                          + "</label>" + "<p>" + Vacaciones.INICIO + "</p> " +
                                          " <label>Fin: </label>" + "<p>" + Vacaciones.FINAL +
                                          "</p><label>Cantidad de dias: </label> <p>" + Vacaciones.CANT_DIAS +
                                          "</p><label>Autorizacion: </label><p>" + Vacaciones.AUTORIZACION +
                                          "</p></body></html>", "<style>h1{position:absolute; top:5%;}</style>");
                }
                else if (Request.Form["ComprobanteAmonestaciones"] != null)
                {
                    var amonestaciones = db.AMONESTACIONES.Find(id);

                    exportData = String.Format("<html><head>Comprobante de Amonestaciones</head><body><h1>" +
                                          " Comprobante de Amonestaciones</h1><label>" +
                                          "Empleado: </label><p>" + amonestaciones.EMPLEADOS.NOMBRE
                                          + " " + amonestaciones.EMPLEADOS.APE1 + " " +
                                          amonestaciones.EMPLEADOS.APE2 + " </p><label>Fecha de Inicio: "
                                          + "</label>" + "<p>" + amonestaciones.FECHA_INICIO + "</p> " +
                                          " <label>Fecha de Ingreso: </label>" + "<p>" + amonestaciones.FECHA_FINAL +
                                          "</p><label>Motivo: </label> <p>" + amonestaciones.GOCE_SALARIO +
                                          "</p><label>Tipo de Amonestacion: </label><p>" + amonestaciones.VERB_ESC +
                                          "</p><label>Autorización: </label><p>" + amonestaciones.AUTORIZACION +
                                          "</p></body></html>", "<style></style>");
                }
                else if (Request.Form["ComprobanteAscensos"] != null)
                {
                    var ascensos = db.ASCENSOS.Find(id);

                    exportData = String.Format("<html><head>Comprobante de Ascensos</head><body><h1>" +
                                          " Comprobante de Ascensos</h1><label>" +
                                          "Empleado: </label><p>" + ascensos.EMPLEADOS.NOMBRE
                                          + " " + ascensos.EMPLEADOS.APE1 + " " +
                                          ascensos.EMPLEADOS.APE2 + " </p><label>Descripcion: "
                                          + "</label>" + "<p>" + ascensos.DESCRIPCION + "</p> " +
                                          " <label>Puesto Anterior: </label>" + "<p>" + ascensos.PUESTO_ANT +
                                          "</p><label>Puesto Nuevo: </label> <p>" + ascensos.PUESTOS.NOMBRE +
                                          "</p><label>Fecha de inicio: </label><p>" + ascensos.FECHA +
                                          "</p><label>Autorización: </label><p>" + ascensos.AUTORIZACION +
                                          "</p></body></html>", "<style></style>");
                }
                else if (Request.Form["ComprobantePermisos"] != null)
                {
                    var permisos = db.PERMISOS.Find(id);

                    exportData = String.Format("<html><head>Comprobante de Permisos</head><body><h1>" +
                                          " Comprobante de Permisos</h1><label>" +
                                          "Empleado: </label><p>" + permisos.EMPLEADOS.NOMBRE
                                          + " " + permisos.EMPLEADOS.APE1 + " " +
                                          permisos.EMPLEADOS.APE2 + " </p><label>Fecha de Inicio: "
                                          + "</label>" + "<p>" + permisos.INICIO + "</p> " +
                                          " <label>Fecha de Ingreso: </label>" + "<p>" + permisos.FINAL +
                                          "</p><label>Tipo de Permiso: </label> <p>" + permisos.GOCE_SALARIO +
                                          "</p><label>Duración: </label><p>" + permisos.CANT_DIAS + " días " +
                                          + permisos.CANT_HORAS + " horas</p><label>Autorización: </label><p>" + 
                                          permisos.AUTORIZACION + "</p></body></html>", "<style></style>");
                }
                else if (Request.Form["ComprobanteSuspenciones"] != null)
                {
                    var suspenciones = db.SUSPENSIONES.Find(id);

                    exportData = String.Format("<html><head>Comprobante de Suspenciones</head><body><h1>" +
                                          " Comprobante de Suspenciones</h1><label>" +
                                          "Empleado: </label><p>" + suspenciones.EMPLEADOS.NOMBRE
                                          + " " + suspenciones.EMPLEADOS.APE1 + " " +
                                          suspenciones.EMPLEADOS.APE2 + " </p><label>Fecha de Inicio: "
                                          + "</label>" + "<p>" + suspenciones.INICIO + "</p> " +
                                          " <label>Fecha de Ingreso: </label>" + "<p>" + suspenciones.FINAL +
                                          "</p><label>Tipo de Suspencion: </label> <p>" + suspenciones.GOCE_SALARIO +
                                          "</p><label>Motivo: </label><p>" + suspenciones.DESCRIPCION +
                                          "</p><label>Autorización: </label><p>" + suspenciones.AUTORIZACION + 
                                          "</p></body></html>", "<style></style>");
                }
                return ExportarPDF(exportData);
            }
            return RedirectToAction("Index");
        }


        public bool ValidaCheckBox(string[] childChkbox)
        {
            if (childChkbox == null)
            {
                TempData["Error"] = "¡Se debe seleccionar al menos un departamento!";
            }
            else
            {
                if (childChkbox.Count() == 1)
                {
                    return true;
                }
                else
                {
                    TempData["Error"] = "¡Solamente es posible ver detalles de un departamento a la vez!";
                }
            }
            return false;
        }
    }
}