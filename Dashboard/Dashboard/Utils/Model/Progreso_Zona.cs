using System.Diagnostics.Eventing.Reader;
using System.Security.Policy;
using Org.BouncyCastle.Asn1.Cms;

namespace Dashboard
{
    public class Progreso_Zona  
    {
        public int id_usuario { get; set; }
        public int id_zona { get; set; }
        public string Nombre { get; set; } = "";

        public bool MinijuegoCompletado { get; set; }
        public int Puntaje { get; set; }
        public TimeOnly Tiempo { get; set; }

        public bool JefeVencido { get; set; }


    }

}
