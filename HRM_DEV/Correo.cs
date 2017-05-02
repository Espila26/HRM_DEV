using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;

namespace HRM_DEV
{
    public class Correo
    {
        public Boolean enviarCorreo(string asunto, string cuerpo, string correo)
        {


            //Configuración del Mensaje
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
            mail.From = new MailAddress("conexus@co.cr", "Conexus", Encoding.UTF8);
            //Aquí ponemos el asunto del correo
            mail.Subject = asunto;
            //Aquí ponemos el mensaje que incluirá el correo
            mail.Body = cuerpo;
            //Especificamos a quien enviaremos el Email.
            mail.To.Add(correo);
            //Si queremos enviar archivos adjuntos tenemos que especificar la ruta en donde se encuentran
            //mail.Attachments.Add(new Attachment(@"C:\Users\Usuario\Desktop\plantilla.jpg"));

            //Configuracion del SMTP
            SmtpServer.Port = 587; //Puerto que utiliza para sus servicios google
                                   //Especificamos las credenciales con las que enviaremos el mail. El correo debe existir
            SmtpServer.Credentials = new System.Net.NetworkCredential("conexus.group.una@gmail.com", "conexus1234");


            if (asunto.Equals("Cumpleaños"))
            {
                //incluir html embebido
                //se definen el contenido de el mensaje
                string html = "<h2>A nombre de nuestra gran familia empresarial queremos enviarle un saludo muy especial deseándole un feliz cumpleaños. Que todos sus deseos se hagan realidad. </h2>" +
                    "<h2>'Lo que importa no es cuantos años acumulas en la vida sino cuanta vida se ha acumulado en esos años'. (Abraham Lincoln). </h2>" +
                   "<img src='cid:imagen' />";

                //se crea la vista en html
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);

                //insertamos la imagen dentro de html
                LinkedResource img = new LinkedResource("C:\\Users\\" + Environment.UserName + "\\Source\\Repos\\HRM_DEV\\HRM_DEV\\Imagenes\\plantilla.jpg", MediaTypeNames.Image.Jpeg);
                img.ContentId = "imagen";

                //se agrega la imagen a la vista
                htmlView.LinkedResources.Add(img);
                //se agrega la vista al email
                mail.AlternateViews.Add(htmlView);

                //se envia el email
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            else
            {
                //incluir html embebido
                //se definen el contenido de el mensaje
                string html = "<h2>El trabajo en equipo, las metas más altas y la perseverancia que ustedes aplican a cada paso les permitirán alcanzar sus sueños. </h2>" +
                    "<h2>Sigan adelante, siempre mirando hacia lo alto. Felicidades en su aniversario. </h2>" +
                   "<img src='cid:imagen' />";

                //se crea la vista en html
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);

                //insertamos la imagen dentro de html
                LinkedResource img = new LinkedResource("C:\\Users\\" + Environment.UserName + "\\Source\\Repos\\HRM_DEV\\HRM_DEV\\Imagenes\\aniversario2.jpg", MediaTypeNames.Image.Jpeg);
                img.ContentId = "imagen";

                //se agrega la imagen a la vista
                htmlView.LinkedResources.Add(img);
                //se agrega la vista al email
                mail.AlternateViews.Add(htmlView);

                //se envia el email
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            return true;

        }
    }
}