

namespace Business.Models
{
   public class Department
    {
        #region Definition of Properties
        public int departamento_Id { get; set; }
        public string nombre { get; set; }
        public int persona_id { get; set; }
       
        
        #endregion
        #region Definition of Constructors
        public Department()
        {
            this.departamento_Id = 0;
            this.nombre = string.Empty;
            this.persona_id = 0;

        }
        #endregion

    }
}
