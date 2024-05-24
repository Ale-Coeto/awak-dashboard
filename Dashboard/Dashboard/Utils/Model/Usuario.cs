namespace Dashboard
{
    public class Usuario
    {
        public int ID_usuario { get; set; }
        public string Contrasenia { get; set; }
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public string Username { get; set; }
        public string RedSocial { get; set; }
        public string Bio { get; set; }
        public bool Admin { get; set; }
        public DateOnly Cumpleanios { get; set; }
        public string FechaRegistro { get; set; }

        public Usuario()
        {

        }


    }

}

