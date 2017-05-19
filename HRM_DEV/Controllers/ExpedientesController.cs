using System;
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
        public ActionResult Create([Bind(Include = "ID_SOLICITUD,ID_EMPLEADO,INICIO,FINAL,CANT_DIAS,AUTORIZACION,FECHA_CREACION,DESCRIPCION")] VACACIONES vACACIONES)
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
                TempData["Success"] = "¡La accción de personal ha sido ingresada exitosamente!";
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
        public ActionResult CreateSusp([Bind(Include = "ID_SUSPENSION,ID_EMPLEADO,INICIO,FINAL,FECHA_CREACION,DESCRIPCION,GOCE_SALARIO,AUTORIZACION")] SUSPENSIONES sUSPENSIONES)
        {
            if (ModelState.IsValid)
            {
                sUSPENSIONES.FECHA_CREACION = System.DateTime.Now;
                db.SUSPENSIONES.Add(sUSPENSIONES);

                TempData["Success"] = "¡La accción de personal ha sido ingresada exitosamente!";
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
        public ActionResult CreatePerm([Bind(Include = "ID_PERMISO,ID_EMPLEADO,INICIO,FINAL,GOCE_SALARIO,CANT_HORAS,CANT_DIAS,FECHA_CREACION,AUTORIZACION,DESCRIPCION")] PERMISOS pERMISOS)
        {
            if (ModelState.IsValid)
            {
                pERMISOS.FECHA_CREACION = System.DateTime.Now;
                db.PERMISOS.Add(pERMISOS);
                TempData["Success"] = "¡La accción de personal ha sido ingresada exitosamente!";
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
                viewBagPuestos();
                return View();
            }

            TempData["Error"] = "¡No tiene acceso al modulo selccionado!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult CreateAsc([Bind(Include = "ID_ASCENSO,ID_EMPLEADO,DESCRIPCION,PUESTO_ANT,PUESTO_NVO,FECHA,FECHA_CREACION,AUTORIZACION")] ASCENSOS aSCENSOS)
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
                TempData["Success"] = "¡La accción de personal ha sido ingresada exitosamente!";
                return RedirectToAction("CreateAsc");
            }
            GetPuestoAnt();
            ViewBagEmpleado();

            viewBagPuestos();
            return View(aSCENSOS);
        }

        public void viewBagPuestos()
        {
            List<object> PUESTOS = new List<Object>();
            var PTS = db.PUESTOS;
            foreach (var i in PTS)
            {
                if (i.DEPARTAMENTOS.EMPRESAS.ESTADO.Equals("Activo"))
                {
                    i.NOMBRE = i.NOMBRE + "-" + i.DEPARTAMENTOS.NOMBRE + "-" + i.DEPARTAMENTOS.EMPRESAS.NOMBRE;
                    PUESTOS.Add(i);
                }
            }
            ViewBag.PUESTO_NVO = new SelectList(PUESTOS, "PTS_ID", "NOMBRE");
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
            ViewData["PUESTO_ANT"] = temp.PUESTOS.NOMBRE + "-" + temp.PUESTOS.DEPARTAMENTOS.NOMBRE + "-" + temp.PUESTOS.DEPARTAMENTOS.EMPRESAS.NOMBRE; 
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
        public ActionResult CreateAmon([Bind(Include = "ID_AMONESTACION,ID_EMPLEADO,FECHA_INICIO,FECHA_FINAL,FECHA_CREACION,DESCRIPCION,GOCE_SALARIO,VERB_ESC,AUTORIZACION")] AMONESTACIONES aMONESTACIONES)
        {
            if (ModelState.IsValid)
            {
                aMONESTACIONES.FECHA_CREACION = System.DateTime.Now;
                db.AMONESTACIONES.Add(aMONESTACIONES);
                db.SaveChanges();
                TempData["Success"] = "¡La accción de personal ha sido ingresada exitosamente!";
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
                empleado.NOMBRE = empleado.NOMBRE + "-" + empleado.PUESTOS.NOMBRE + "-" + empleado.PUESTOS.DEPARTAMENTOS.NOMBRE + "-" + empleado.PUESTOS.DEPARTAMENTOS.EMPRESAS.NOMBRE;
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
                    var nombre = Vacaciones.EMPLEADOS.NOMBRE.Split('-');

                    ViewBagEmpleado();
                    TempData.Keep("Empleado");

                    exportData = String.Format("<html><head>Comprobante de Vacaciones</head><body><h1>" +
                                          " Comprobante de Vacaciones</h1><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Dias Disponibles: " + ViewBag.DiasDisponibles + "</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Empleado: " + nombre[0]
                                          + " " + Vacaciones.EMPLEADOS.APE1 + " " +
                                          Vacaciones.EMPLEADOS.APE2 + "</label><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Inicio: " + Vacaciones.INICIO + " </label><br></br><br></br><br></br>" +
                                          " <label style='font-size: 23px;font-weight:bold;'>Fin: " + Vacaciones.FINAL + "</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Cantidad de dias: " + Vacaciones.CANT_DIAS + "</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Autorizacion: " + Vacaciones.AUTORIZACION + "</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Firma del autorizador(a): _____________________________</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Firma del empleado(a):    _____________________________</label>" +
                                          "</body></html>", "<style></style>");
                }
                else if (Request.Form["ComprobanteAmonestaciones"] != null)
                {
                    var amonestaciones = db.AMONESTACIONES.Find(id);
                    var nombre = amonestaciones.EMPLEADOS.NOMBRE.Split('-');

                    exportData = String.Format("<html><head>Comprobante de Amonestaciones</head><body><h1>" +
                                          " Comprobante de Amonestaciones</h1><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Empleado: " + 
                                          nombre[0]
                                          + " " + amonestaciones.EMPLEADOS.APE1 + " " +
                                          amonestaciones.EMPLEADOS.APE2 + "</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Fecha de Inicio: " + amonestaciones.FECHA_INICIO + "</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Fecha de Ingreso: " + amonestaciones.FECHA_FINAL + "</label><br></br><br></br><br></br>" + 
                                          "<label style='font-size: 23px;font-weight:bold;'>Motivo: " + amonestaciones.GOCE_SALARIO + "</label><br></br><br></br><br></br>"+
                                          "<label style='font-size: 23px;font-weight:bold;'>Tipo de Amonestacion: " + amonestaciones.VERB_ESC + "</label> <br></br><br></br><br></br>" + 
                                          "<label style='font-size: 23px;font-weight:bold;'>Autorización: " + amonestaciones.AUTORIZACION + "</label><br></br><br></br><br></br>" + 
                                          "<label style='font-size: 23px;font-weight:bold;' > Firma del autorizador(a): _____________________________ </label ><br></br><br></br><br></br> " +
                                          "<label style='font-size: 23px;font-weight:bold;'>Firma del empleado(a):    _____________________________</label>" +
                                          "</body></html>", "<style></style>");
                }
                else if (Request.Form["ComprobanteAscensos"] != null)
                {
                    var ascensos = db.ASCENSOS.Find(id);
                    var nombre = ascensos.EMPLEADOS.NOMBRE.Split('-');

                    exportData = String.Format("<html><head>Comprobante de Ascensos</head><body><h1>" +
                                          " Comprobante de Ascensos</h1><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Empleado: " + nombre[0]
                                          + " " + ascensos.EMPLEADOS.APE1 + " " +
                                          ascensos.EMPLEADOS.APE2 + " </label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Descripcion: " + ascensos.DESCRIPCION + "</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Puesto Anterior: " + ascensos.PUESTO_ANT + "</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Puesto Nuevo: " + ascensos.PUESTOS.NOMBRE + "</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Fecha de inicio: " + ascensos.FECHA + "</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Autorización: " + ascensos.AUTORIZACION + "</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;' > Firma del autorizador(a): _____________________________ </label ><br></br><br></br><br></br> " +
                                          "<label style='font-size: 23px;font-weight:bold;'>Firma del empleado(a):    _____________________________</label>" +
                                          "</body></html>", "<style></style>");
                }
                else if (Request.Form["ComprobantePermisos"] != null)
                {
                    var permisos = db.PERMISOS.Find(id);
                    var nombre = permisos.EMPLEADOS.NOMBRE.Split('-');

                    exportData = String.Format("<html><head>Comprobante de Permisos</head><body><h1>" +
                                          " Comprobante de Permisos</h1><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Empleado: " + nombre[0]
                                          + " " + permisos.EMPLEADOS.APE1 + " " +
                                          permisos.EMPLEADOS.APE2 + " </label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Fecha de Inicio: " + permisos.INICIO + "</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Fecha de Ingreso: " + permisos.FINAL + "</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Tipo de Permiso: " + permisos.GOCE_SALARIO + "</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Duración: " + permisos.CANT_DIAS + " días " + permisos.CANT_HORAS + " horas</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Autorización:" + permisos.AUTORIZACION + "</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;' > Firma del autorizador(a): _____________________________ </label ><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Firma del empleado(a):    _____________________________</label>" +
                                          "</body></html>", "<style></style>");
                }
                else if (Request.Form["ComprobanteSuspenciones"] != null)
                {
                    var suspenciones = db.SUSPENSIONES.Find(id);
                    var nombre = suspenciones.EMPLEADOS.NOMBRE.Split('-');

                    exportData = String.Format("<html><head>Comprobante de Suspenciones</head><body><h1>" +
                                          " Comprobante de Suspenciones</h1><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>" + "Empleado: " + nombre[0]
                                          + " " + suspenciones.EMPLEADOS.APE1 + " " +
                                          suspenciones.EMPLEADOS.APE2 + " </label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Fecha de Inicio: " + suspenciones.INICIO + "</label><br></br><br></br><br></br>" +
                                          " <label style='font-size: 23px;font-weight:bold;'>Fecha de Ingreso: " + suspenciones.FINAL + "</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Tipo de Suspencion: " + suspenciones.GOCE_SALARIO + "</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Motivo: " + suspenciones.DESCRIPCION + "</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;'>Autorización: " + suspenciones.AUTORIZACION + "</label><br></br><br></br><br></br>" +
                                          "<label style='font-size: 23px;font-weight:bold;' > Firma del autorizador(a): _____________________________ </label ><br></br><br></br><br></br> " +
                                          "<label style='font-size: 23px;font-weight:bold;'>Firma del empleado(a):    _____________________________</label>" +
                                          "</body></html>", "<style></style>");
                }
                return ExportarPDF(exportData);
            }
            return RedirectToAction("Index");
        }


        public bool ValidaCheckBox(string[] childChkbox)
        {
            if (childChkbox == null)
            {
                TempData["Error"] = "¡Se debe un registro para imprimir el comprobante!";
            }
            else
            {
                if (childChkbox.Count() == 1)
                {
                    return true;
                }
                else
                {
                    TempData["Error"] = "¡Solamente es posible imprimir un comprobante a la vez!";
                }
            }
            return false;
        }
    }
}