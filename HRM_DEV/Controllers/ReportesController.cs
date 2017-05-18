using HRM_DEV.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace HRM_DEV.Controllers
{
    public class ReportesController : Controller
    {
        private hrm_dbEntities db = new hrm_dbEntities();
        // GET: Reportes
        public ActionResult GenerarReporte()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Generar(string datoEmpleado, string Estado, string desde, string hasta, string accion)
        {
            USUARIOS varUser = (USUARIOS)Session["usuarios"];

            if (Session["usuarios"] != null && varUser.ACC_ACCIONES.Equals("Si"))
            {
                var generarDesde = GenerarDesde(desde);
                var generarHasta = GenerarHasta(hasta);

                TempData["datoEmpleado"] = datoEmpleado;
                TempData["estado"] = Estado;
                TempData["desde"] = desde;
                TempData["hasta"] = hasta;
                TempData["accion"] = accion;

                if (accion.Equals("Vacaciones"))
                {
                    var Vac = from e in db.VACACIONES
                              select e;

                    Vac = Vac.Where(s => (s.EMPLEADOS.NOMBRE.Contains(datoEmpleado) || s.EMPLEADOS.APE1.Contains(datoEmpleado) || s.EMPLEADOS.APE2.Contains(datoEmpleado) || s.EMPLEADOS.CEDULA.Contains(datoEmpleado)) && (s.EMPLEADOS.ESTADO.Contains(Estado)) && (s.FECHA_CREACION >= generarDesde) && (s.FECHA_CREACION <= generarHasta));
                    return View("ReporteVacaciones", Vac);
                }

                else if (accion.Equals("Amonestaciones"))
                {

                    var Amon = from e in db.AMONESTACIONES
                               select e;

                    Amon = Amon.Where(s => (s.EMPLEADOS.NOMBRE.Contains(datoEmpleado) || s.EMPLEADOS.APE1.Contains(datoEmpleado) || s.EMPLEADOS.APE2.Contains(datoEmpleado) || s.EMPLEADOS.CEDULA.Contains(datoEmpleado)) && (s.EMPLEADOS.ESTADO.Contains(Estado)) && (s.FECHA_CREACION >= generarDesde) && (s.FECHA_CREACION <= generarHasta));
                    return View("ReporteAmonestaciones", Amon);

                }

                else if (accion.Equals("Ascensos"))
                {

                    var Asc = from e in db.ASCENSOS
                              select e;

                    Asc = Asc.Where(s => (s.EMPLEADOS.NOMBRE.Contains(datoEmpleado) || s.EMPLEADOS.APE1.Contains(datoEmpleado) || s.EMPLEADOS.APE2.Contains(datoEmpleado) || s.EMPLEADOS.CEDULA.Contains(datoEmpleado)) && (s.EMPLEADOS.ESTADO.Contains(Estado)) && (s.FECHA_CREACION >= generarDesde) && (s.FECHA_CREACION <= generarHasta));
                    return View("ReporteAscensos", Asc);

                }

                else if (accion.Equals("Permisos"))
                {

                    var Per = from e in db.PERMISOS
                              select e;

                    Per = Per.Where(s => (s.EMPLEADOS.NOMBRE.Contains(datoEmpleado) || s.EMPLEADOS.APE1.Contains(datoEmpleado) || s.EMPLEADOS.APE2.Contains(datoEmpleado) || s.EMPLEADOS.CEDULA.Contains(datoEmpleado)) && (s.EMPLEADOS.ESTADO.Contains(Estado)) && (s.FECHA_CREACION >= generarDesde) && (s.FECHA_CREACION <= generarHasta));
                    return View("ReportePermisos", Per);

                }
                else if (accion.Equals("Suspensiones"))
                {

                    var Sus = from e in db.SUSPENSIONES
                              select e;

                    Sus = Sus.Where(s => (s.EMPLEADOS.NOMBRE.Contains(datoEmpleado) || s.EMPLEADOS.APE1.Contains(datoEmpleado) || s.EMPLEADOS.APE2.Contains(datoEmpleado) || s.EMPLEADOS.CEDULA.Contains(datoEmpleado)) && (s.EMPLEADOS.ESTADO.Contains(Estado)) && (s.FECHA_CREACION >= generarDesde) && (s.FECHA_CREACION <= generarHasta));
                    return View("ReporteSuspensiones", Sus);

                }

                else
                {

                    var Emp = from e in db.EMPLEADOS
                              select e;

                    Emp = Emp.Where(s => (s.NOMBRE.Contains(datoEmpleado) || s.APE1.Contains(datoEmpleado) || s.APE2.Contains(datoEmpleado) || s.CEDULA.Contains(datoEmpleado)) && (s.ESTADO.Contains(Estado)) && (s.FECHA_CONTR >= generarDesde) && (s.FECHA_CONTR <= generarHasta));
                    return View("ReporteEmpleados", Emp);

                }
            }

            TempData["Error"] = "¡No tiene acceso al modulo selccionado!";
            return RedirectToAction("Index", "Home");
        }

        public void ExportarExcel()
        {

            string gridData = "";

            TempData.Keep("Accion");
            TempData.Keep("desde");
            TempData.Keep("hasta");
            TempData.Keep("datoEmpleado");
            TempData.Keep("estado");

            switch (TempData["Accion"].ToString())
            {
                case "Vacaciones":
                    gridData = gridVacaciones();
                    break;
                case "Amonestaciones":
                    gridData = gridAmonestaciones();
                    break;
                case "Ascensos":
                    gridData = gridAscensos();
                    break;
                case "Permisos":
                    gridData = gridPermisos();
                    break;
                case "Suspensiones":
                    gridData = gridSuspensiones();
                    break;
                default:
                    gridData = gridEmpleados();
                    break;
            }

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=ReporteVacaciones.xls");
            Response.ContentType = "application/excel";
            Response.Write(gridData);
            Response.End();
        }

        public FileStreamResult ExportarPDF()
        {

            string gridData = "";
            string tipoReporte = "";

            TempData.Keep("Accion");
            TempData.Keep("desde");
            TempData.Keep("hasta");
            TempData.Keep("datoEmpleado");
            TempData.Keep("estado");

            switch (TempData["Accion"].ToString())
            {
                case "Vacaciones":
                    gridData = gridVacaciones();
                    tipoReporte = "vacaciones";
                    break;
                case "Amonestaciones":
                    gridData = gridAmonestaciones();
                    tipoReporte = "amonestaciones";
                    break;
                case "Ascensos":
                    gridData = gridAscensos();
                    tipoReporte = "ascensos";
                    break;
                case "Permisos":
                    gridData = gridPermisos();
                    tipoReporte = "permisos";
                    break;
                case "Suspensiones":
                    gridData = gridSuspensiones();
                    tipoReporte = "suspensiones";
                    break;
                default:
                    gridData = gridEmpleados();
                    tipoReporte = "empleados";
                    break;
            }
            string exportData = String.Format("<html><head>{0}</head><body><p>Reporte de " + tipoReporte + "</p>{1}</body></html>", "<style>table{ text-align: center; border-collapse: collapse; width: 100%; height:100%; background: #fff overflow: auto; border: 1px solid #000000; font: normal 12px/150% Arial, Helvetica, sans-serif;} th{background - color: #5C5C5C;font - weight: bold;color: White !important;    background-color:#5C5C5C;color:#FFFFFF; padding: 5px; border: 1px solid black; border - right: 1px solid #A3A3A3; padding: 10px;} td{ border: 1px solid #000000; padding: 10px;} p{font-family:sans-serif;font-weight:bold;font-size:25px;position:absolute;left:50%;}</style>", gridData);
            var bytes = System.Text.Encoding.UTF8.GetBytes(exportData);

            using (var input = new MemoryStream(bytes))
            {
                var output = new MemoryStream();
                var document = new Document(PageSize.A4, 50, 50, 50, 50);
                var writer = PdfWriter.GetInstance(document, output);
                writer.CloseStream = false;
                document.Open();

                var xmlWorker = iTextSharp.tool.xml.XMLWorkerHelper.GetInstance();
                xmlWorker.ParseXHtml(writer, document, input, System.Text.Encoding.UTF8);
                document.Close();
                output.Position = 0;
                return new FileStreamResult(output, "application/pdf");
            }
        }

        public string gridVacaciones()
        {
            var generarDesde = GenerarDesde(TempData["desde"].ToString());
            var generarHasta = GenerarHasta(TempData["hasta"].ToString());

            string datoEmpleado = TempData["datoEmpleado"].ToString();
            string Estado = TempData["estado"].ToString();

            var Vacaciones = from e in db.VACACIONES
                      select e;

            Vacaciones = Vacaciones.Where(s => (s.EMPLEADOS.NOMBRE.Contains(datoEmpleado) || s.EMPLEADOS.APE1.Contains(datoEmpleado) || s.EMPLEADOS.APE2.Contains(datoEmpleado) || s.EMPLEADOS.CEDULA.Contains(datoEmpleado)) && (s.EMPLEADOS.ESTADO.Contains(Estado)) && (s.FECHA_CREACION >= generarDesde) && (s.FECHA_CREACION <= generarHasta));

            WebGrid grid = new WebGrid(source: Vacaciones, canPage: false, canSort: false);

            string gridData = grid.GetHtml(
            columns: grid.Columns(
                            grid.Column("ID_SOLICITUD", "Número de Solicitud: "),
                            grid.Column("ID_EMPLEADO", "Número de Empleado: "),
                            grid.Column("INICIO", "Fecha de Inicio: "),
                            grid.Column("FINAL", "Fecha de Ingreso: "),
                            grid.Column("CANT_DIAS", "Cantidad de Dias: "),
                            grid.Column("AUTORIZACION", "Autorización: ")
                            )).ToString();
            return gridData;
        }

        public string gridAmonestaciones()
        {
            var generarDesde = GenerarDesde(TempData["desde"].ToString());
            var generarHasta = GenerarHasta(TempData["hasta"].ToString());

            string datoEmpleado = TempData["datoEmpleado"].ToString();
            string Estado = TempData["estado"].ToString();

            var Amonestaciones = from e in db.AMONESTACIONES
                       select e;

            Amonestaciones = Amonestaciones.Where(s => (s.EMPLEADOS.NOMBRE.Contains(datoEmpleado) || s.EMPLEADOS.APE1.Contains(datoEmpleado) || s.EMPLEADOS.APE2.Contains(datoEmpleado) || s.EMPLEADOS.CEDULA.Contains(datoEmpleado)) && (s.EMPLEADOS.ESTADO.Contains(Estado)) && (s.FECHA_CREACION >= generarDesde) && (s.FECHA_CREACION <= generarHasta));

            WebGrid grid = new WebGrid(source: Amonestaciones, canPage: false, canSort: false);

            string gridData = grid.GetHtml(
            columns: grid.Columns(
                     grid.Column("ID_AMONESTACION", "Número de Solicitud"),
                     grid.Column("ID_EMPLEADO", "Empleado"),
                     grid.Column("FECHA_INICIO", "Fecha de Inicio"),
                     grid.Column("FECHA_FINAL", "Fecha de Ingreso"),
                     grid.Column("DESCRIPCION", "Motivo:"),
                     grid.Column("GOCE_SALARIO", "Repercución de la Amonestación"),
                     grid.Column("VERB_ESC", "Tipo de Amonestacion"),
                     grid.Column("AUTORIZACION", "Autorización")
                            )).ToString();
            return gridData;
        }

        public string gridAscensos()
        {

            var generarDesde = GenerarDesde(TempData["desde"].ToString());
            var generarHasta = GenerarHasta(TempData["hasta"].ToString());

            string datoEmpleado = TempData["datoEmpleado"].ToString();
            string Estado = TempData["estado"].ToString();

            var Ascensos = from e in db.ASCENSOS
                      select e;

            Ascensos = Ascensos.Where(s => (s.EMPLEADOS.NOMBRE.Contains(datoEmpleado) || s.EMPLEADOS.APE1.Contains(datoEmpleado) || s.EMPLEADOS.APE2.Contains(datoEmpleado) || s.EMPLEADOS.CEDULA.Contains(datoEmpleado)) && (s.EMPLEADOS.ESTADO.Contains(Estado)) && (s.FECHA_CREACION >= generarDesde) && (s.FECHA_CREACION <= generarHasta));

            WebGrid grid = new WebGrid(source: Ascensos, canPage: false, canSort: false);

            string gridData = grid.GetHtml(
            columns: grid.Columns(
                     grid.Column("ID_AMONESTACION", "Número de Solicitud"),
                     grid.Column("ID_EMPLEADO", "Empleado"),
                     grid.Column("FECHA_INICIO", "Fecha de Inicio"),
                     grid.Column("FECHA_FINAL", "Fecha de Ingreso"),
                     grid.Column("DESCRIPCION", "Motivo:"),
                     grid.Column("GOCE_SALARIO", "Repercución de la Amonestación"),
                     grid.Column("VERB_ESC", "Tipo de Amonestacion"),
                     grid.Column("AUTORIZACION", "Autorización")
                            )).ToString();
            return gridData;
        }

        public string gridPermisos()
        {

            var generarDesde = GenerarDesde(TempData["desde"].ToString());
            var generarHasta = GenerarHasta(TempData["hasta"].ToString());

            string datoEmpleado = TempData["datoEmpleado"].ToString();
            string Estado = TempData["estado"].ToString();

            var Permisos = from e in db.PERMISOS
                      select e;

            Permisos = Permisos.Where(s => (s.EMPLEADOS.NOMBRE.Contains(datoEmpleado) || s.EMPLEADOS.APE1.Contains(datoEmpleado) || s.EMPLEADOS.APE2.Contains(datoEmpleado) || s.EMPLEADOS.CEDULA.Contains(datoEmpleado)) && (s.EMPLEADOS.ESTADO.Contains(Estado)) && (s.FECHA_CREACION >= generarDesde) && (s.FECHA_CREACION <= generarHasta));

            WebGrid grid = new WebGrid(source: Permisos, canPage: false, canSort: false);

            string gridData = grid.GetHtml(
            columns: grid.Columns(
                     grid.Column("ID_PERMISO", "Número de Solicitud"),
                     grid.Column("ID_EMPLEADO", "Empleado"),
                     grid.Column("INICIO", "Fecha de Inicio"),
                     grid.Column("FINAL", "Fecha de Ingreso"),
                     grid.Column("GOCE_SALARIO", "Tipo de Permiso"),
                     grid.Column("CANT_HORAS", "Cantidad de Horas"),
                     grid.Column("CANT_DIAS", "Cantidad de Días"),
                     grid.Column("AUTORIZACION", "Autorización")
                            )).ToString();
            return gridData;
        }

        public string gridSuspensiones()
        {

            var generarDesde = GenerarDesde(TempData["desde"].ToString());
            var generarHasta = GenerarHasta(TempData["hasta"].ToString());

            string datoEmpleado = TempData["datoEmpleado"].ToString();
            string Estado = TempData["estado"].ToString();

            var Suspensiones = from e in db.SUSPENSIONES
                      select e;

            Suspensiones = Suspensiones.Where(s => (s.EMPLEADOS.NOMBRE.Contains(datoEmpleado) || s.EMPLEADOS.APE1.Contains(datoEmpleado) || s.EMPLEADOS.APE2.Contains(datoEmpleado) || s.EMPLEADOS.CEDULA.Contains(datoEmpleado)) && (s.EMPLEADOS.ESTADO.Contains(Estado)) && (s.FECHA_CREACION >= generarDesde) && (s.FECHA_CREACION <= generarHasta));

            WebGrid grid = new WebGrid(source: Suspensiones, canPage: false, canSort: false);

            string gridData = grid.GetHtml(
            columns: grid.Columns(
                     grid.Column("ID_SUSPENSION", "Número de Solicitud"),
                     grid.Column("ID_EMPLEADO", "Empleado"),
                     grid.Column("INICIO", "Fecha de Inicio"),
                     grid.Column("FINAL", "Fecha de Ingreso"),
                     grid.Column("DESCRIPCION", "Motivo"),
                     grid.Column("GOCE_SALARIO", "Tipo de Suspensión"),
                     grid.Column("AUTORIZACION", "Autorización")
                            )).ToString();
            return gridData;
        }

        public string gridEmpleados()
        {

            var generarDesde = GenerarDesde(TempData["desde"].ToString());
            var generarHasta = GenerarHasta(TempData["hasta"].ToString());

            string datoEmpleado = TempData["datoEmpleado"].ToString();
            string Estado = TempData["estado"].ToString();

            var Emp = from e in db.EMPLEADOS
                      select e;

            Emp = Emp.Where(s => (s.NOMBRE.Contains(datoEmpleado) || s.APE1.Contains(datoEmpleado) || s.APE2.Contains(datoEmpleado) || s.CEDULA.Contains(datoEmpleado)) && (s.ESTADO.Contains(Estado)) && (s.FECHA_CONTR >= generarDesde) && (s.FECHA_CONTR <= generarHasta));

            WebGrid grid = new WebGrid(source: Emp, canPage: false, canSort: false);

            string gridData = grid.GetHtml(
            columns: grid.Columns(
                     grid.Column("CEDULA", "Cédula"),
                     grid.Column("NOMBRE", "Nombre"),
                     grid.Column("APE1", "Primer Apellido"),
                     grid.Column("APE2", "Segundo Apellido"),
                     grid.Column("TEL_MOVIL", "Teléfono Móvil"),
                     grid.Column("E_MAIL", "Correo Electrónico"),
                     grid.Column("ESTADO", "Estado")
                            )).ToString();
            return gridData;
        }

        public DateTime GenerarDesde(string desde)
        {
            var generarDesde = new DateTime();

            if (String.IsNullOrEmpty(desde))
            {

                generarDesde = new DateTime(1017, 3, 17);
            }
            else
            {

                generarDesde = DateTime.Parse(desde);
            }

            return generarDesde;
        }

        public DateTime GenerarHasta(string hasta)
        {
            var generarHasta = new DateTime();

            if (String.IsNullOrEmpty(hasta))
            {

                generarHasta = new DateTime(2099, 3, 19);
            }
            else
            {
                generarHasta = DateTime.Parse(hasta);
            }

            return generarHasta;
        }
    }
}