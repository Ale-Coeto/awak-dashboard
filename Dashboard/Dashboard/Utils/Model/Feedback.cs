namespace Dashboard.Models
{
    public class Feedback
    {
        public int ID_Feedback { get; set; }
        public int ID_Usuario { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Sugerencias { get; set; }
        public string Remover { get; set; }
        public string Preguntas { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
