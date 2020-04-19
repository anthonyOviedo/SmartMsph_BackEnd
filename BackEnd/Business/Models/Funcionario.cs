using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class Funcionario
    {

        public string apellido2 { get; set; }
        public string apellido1 { get; set; }
        public int Person_Id { get; set; }
        public string nombre { get; set; }
        public Funcionario()
        {
           
            this.nombre = string.Empty;
            this.apellido1 = string.Empty;
            this.apellido2 = string.Empty;
            this.Person_Id = 0;
   
        }


    }
}
