namespace Dashboard.Utils.Model
{
    public class Progreso_dir
    {
        public int puntos { get; set; }
        public int zonas { get; set; }
        public string rol { get; set; } = "";
        public Progreso_dir()
        {
            rol = "Colaborador";
            zonas = 1;
            puntos = 1000;
        }
    }

}
