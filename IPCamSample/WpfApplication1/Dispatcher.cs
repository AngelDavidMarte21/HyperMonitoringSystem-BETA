using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication1
{
    class DispatcherMessage
    {
        public static void sendEmail(string message, string email)
        {

        }

        public static void sendSMS(string message, string contact)
        {

        }


        public static void sendEmailTo(string body, string subject, string contact)
        {
            try
            {
                MailMessage mail = new MailMessage();

                //SmtpClient SmtpServer = new SmtpClient("smtp.mail.yahoo.com"); //Yahoo
                //SmtpClient SmtpServer = new SmtpClient("smtp.live.com"); //Hotmail
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com"); //Gmail

                mail.From = new MailAddress(Credentials.account);
                mail.To.Add(contact);
                mail.Subject = subject;
                mail.Body = body;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(Credentials.account, Credentials.password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                MessageBox.Show("El correo ha sido enviado.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void forwardAll(string message)
        {
            NetworkStream serverStream = default(NetworkStream);
            TcpClient clientSocket = new TcpClient();
            
            foreach(var data in Data.connections)
            {
                clientSocket.Connect(data.ip, data.port);
                serverStream = clientSocket.GetStream();

                byte[] outStream = Encoding.ASCII.GetBytes(message);

                if (serverStream != null)
                {
                    try
                    {
                        serverStream.Write(outStream, 0, outStream.Length);
                        serverStream.Flush();
                    }
                    catch (SocketException error)
                    {
                        System.Windows.MessageBox.Show("No se pudo enviar el mensaje");
                        System.Windows.MessageBox.Show(error.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("No hay conexion.");
                }
            }
            
        }

        public static void forwardTo(string ipToForward, int port, string message)
        {
            NetworkStream serverStream = default(NetworkStream);
            TcpClient clientSocket = new TcpClient();
            clientSocket.Connect(ipToForward, port);
            serverStream = clientSocket.GetStream();

            byte[] outStream = Encoding.ASCII.GetBytes(message);

            if (serverStream != null)
            {
                try
                {
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();
                }
                catch (SocketException error)
                {
                    System.Windows.MessageBox.Show("No se pudo enviar el mensaje");
                    System.Windows.MessageBox.Show(error.ToString());
                }
            }
            else
            {
                MessageBox.Show("No hay conexion.");
            }
        }
    }
}
