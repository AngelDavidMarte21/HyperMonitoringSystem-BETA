using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Cliente.xaml
    /// </summary>
    public partial class Cliente : Window
    {
        string readdata = null;

        int _port = 0;
        public Cliente()
        {
            InitializeComponent();
            
        }

        private void Listener(Int32 port)
        {
            TcpListener server = null;
            try
            {
                _port = port;
                // Set the TcpListener on port 1921.
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                server = new TcpListener(localAddr, port);

                // Start listening.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];

                bool msgB = false;

                // Enter the listening loop.
                while (true)
                {
                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    readdata = null;
                    //MessageBox.Show("Conexion establecida.");

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;
                    try
                    {
                        // Loop to receive all the data sent by the client.
                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            // Translate data bytes to a ASCII string.
                            readdata = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                            msg();
                        }
                    }
                    catch(System.IO.IOException)
                    {
                        if(msgB == false)
                        {
                            //MessageBox.Show("La conexion ha sido cerrada.");
                            //msgB = true;
                        }
                        
                    }
                    
                    // Shutdown and end connection
                    //client.Close();
                }
            }
            catch (SocketException e)
            {
                MessageBox.Show("SocketException: " + e);
            }
            finally
            {
                // Stop listening for new clients.
                //server.Stop();
            }
        }
    

        private void msg()
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new MethodInvoker(msg));
            }
            else
            {
                string[] split = readdata.Split(new[] { "~~" }, StringSplitOptions.None);
                if (readdata.IndexOf("HIT~Am:") >= 0)
                {
                    foreach (string superdata in split)
                    {
                        if (superdata == "~~" || superdata.IndexOf("\0") >= 0)
                        {
                            break;
                        }
                        try
                        {
                            EventosDG x = new EventosDG();
                            string[] split2 = superdata.Split(new[] { "||" }, StringSplitOptions.None);
                            if (split2[3] == "Alerta de fuego")
                            {
                                //Agregando valores a EventosDG para poder mostrarlos en el Datagrid.
                                x = new EventosDG() { events = split2[3], panel = split2[0].Substring(7), type= "Fuego" , evento="Detector de humo activado" ,zone = split2[2], time = DateTime.Now.ToString(), area = split2[1] };
                            }
                            else if (split2[3] == "Open")
                            {
                                x = new EventosDG() { events = split2[3], panel = split2[0].Substring(7), zone = split2[2], time = DateTime.Now.ToString(), area = split2[1] };
                            }
                            else if (split2[3] == "Close")
                            {
                                x = new EventosDG() { events = split2[3], panel = split2[0].Substring(7), zone = split2[2], time = DateTime.Now.ToString(), area = split2[1] };
                            }
                            else
                            {
                                MessageBox.Show("Sin coincidencias", "", MessageBoxButton.OK, MessageBoxImage.Information);
                            }

                            dg_eventos.Items.Add(x);
                            
                        }
                        catch(Exception err)
                        {
                            //MessageBox.Show(err.ToString());
                        }
                    }
                }
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int puerto = int.Parse(puertoTxt.Text);
            Thread x = new Thread(() => Listener(puerto));
            x.Start();
        }
    }
}
