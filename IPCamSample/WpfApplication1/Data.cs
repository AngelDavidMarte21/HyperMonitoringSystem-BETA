using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class EventosDG
    {
        public string panel { get; set; }
        public string zone { get; set; }
        public string events { get; set; }
        public string type { get; set; }
        public string time { get; set; }
        public string dp { get; set; }
        public string area { get; set; }
        public string evento { get; set; }
    }


    class Data
    {
        public class Conn
        {
            public string ip { get; set; }
            public int port { get; set; }
        }

        public class PIP
        {
            public string panel { get; set; }
            public int port { get; set; }
            public string ip { get; set; }
        }

        public static List<Conn> connections = new List<Conn>();
        public static List<PIP> lstClient = new List<PIP>();
    }

    class Credentials
    {

        public static string account = "*********@gmail.com";
        public static string password = "******";
    }

    class getSupportedVersions
    {
        public string context { get; set; }
        public string method { get; set; }
    }
}
