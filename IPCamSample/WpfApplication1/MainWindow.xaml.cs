using NAudio.Codecs;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IpCamController _controller;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_LoadedAsync;
            this.Closing += MainWindow_Closing;
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _controller.StopProcessing();  
        }

        async void MainWindow_LoadedAsync(object sender, RoutedEventArgs e)
        {
            //Obteniendo el video
            Thread x = new Thread(() =>
            {
                _controller = new IpCamController("http://10.1.12.103", "dev", "12345678");
                _controller.ImageReady += dec_FrameReady;
                _controller.StartProcessing();
            });
            x.Start();


            string ParamList;
            WaveOut _waveOut = new WaveOut();
            //WaveFileReader reader;

            ParamList = "http://10.1.12.103/axis-cgi/audio/receive.cgi";
            NetworkCredential networkCredential = new NetworkCredential("dev", "12345678");

            WebRequest request = WebRequest.Create(ParamList);
            request.Credentials = networkCredential;
            HttpWebResponse response;
            Stream streamResponse = null;
            MemoryStream ms = new MemoryStream();
            //IWaveProvider reader;
            BufferedWaveProvider provider;
            WaveIn wi;

            //NAUDIO presenta un problema cuando corre por unos segundos, se como resolverlo pero ahora no puedo repararlo.
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                streamResponse = response.GetResponseStream();

                using (response = (HttpWebResponse)request.GetResponse())
                {
                    //Obteniendo la respuesta del request.
                    using (streamResponse = response.GetResponseStream())
                    {
                        //Verificando si se puede leer
                        if (streamResponse.CanRead)
                        {
                            // 1048576 => 1MB
                            byte[] buffer = new byte[1048576];
                            while (true)
                            {
                                //Leyendo el Stream
                                int countBytes = await streamResponse.ReadAsync(buffer, 0, buffer.Length);
                                if (countBytes <= 0)
                                {
                                    MessageBox.Show("La lectura ha sido completada.");
                                    streamResponse.Dispose();
                                }

                                byte[] decoded = new byte[buffer.Length * 2];
                                //Decodifica los bytes obtenidos
                                ALawDecoder.ALawDecode(buffer, out decoded);

                                //Tratando de reproducirlo.
                                wi = new WaveIn();
                                wi.WaveFormat = new WaveFormat(16000, 16, 2);
                                provider = new BufferedWaveProvider(wi.WaveFormat);
                                provider.DiscardOnBufferOverflow = true;
                                provider.AddSamples(buffer, 0, buffer.Length);

                                _waveOut.Init(provider);
                                _waveOut.Play();
                            }
                        }
                    }
                }

            }
            catch (Exception es)
            {
                MessageBox.Show(es.ToString(), "\nError Message");
            }
        }


        void dec_FrameReady(object sender,ImageReadyEventArsgs args)
        {
            imgStream.Source = args.Image;
        }

        private void ForceUIToUpdate()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Render, new DispatcherOperationCallback(delegate (object parameter) { frame.Continue = false; return null; }), null);
            Dispatcher.PushFrame(frame);
        }
    }
}
