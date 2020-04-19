using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
   public class Persona
    {
        #region Definition of Properties
        public long persona_Id { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string strId { get; set; }



        #endregion
        #region Definition of Constructors
        public Persona()
        {
            this.persona_Id = 0;
            this.nombre = string.Empty;
            this.apellido1 = string.Empty;
            this.apellido1 = string.Empty;
            this.correo = string.Empty;
            this.telefono = string.Empty;
            this.strId = string.Empty;
        }
        #endregion
    }
}
