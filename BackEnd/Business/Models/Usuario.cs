namespace Business.Models
{
    public class Usuario
    {
        #region Definition of Properties
        public string usuario_Id { get; set; }
        public string nombre { get; set; }
        public int rol { get; set; }
        public string password { get; set; }
        public Persona persona { get; set; }   
        public Department departamento { get; set; }
        #endregion
        #region Definition of Constructors
        public Usuario()
        {
            this.usuario_Id = string.Empty;
            this.nombre = string.Empty;
            this.password = string.Empty;
            this.persona = new Persona();
            this.departamento = new Department();
        }
        #endregion
    }
}