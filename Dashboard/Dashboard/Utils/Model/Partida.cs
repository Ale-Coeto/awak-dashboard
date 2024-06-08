namespace Dashboard.Utils.Model
{
    public class Partida
    {
        private int idUsuario;
        private int idMinijuego;
        private int puntaje;
        private int tiempo;

        public int IdUsuario { get { return idUsuario; } set { idUsuario = value; } }
        public int IdMinijuego { get {  return idMinijuego; } set { idMinijuego = value; } }
        public int Puntaje { get {  return puntaje; } set {  puntaje = value; } }
        public int Tiempo { get { return tiempo; } set { tiempo = value; } }
    }
}
