using System.Diagnostics.Eventing.Reader;
using System.Security.Policy;
using Org.BouncyCastle.Asn1.Cms;

namespace Dashboard
{
    public class Progreso_Zona  
    {
        public int ID_Usuario { get; set; }
        public int ID_Zona { get; set; }
        public string Nombre { get; set; } = "";

        public bool MinijuegoCompletado { get; set; }
        public int Puntaje { get; set; }
        public TimeOnly Tiempo { get; set; }

        public bool JefeVencido { get; set; }


    }

}
