using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class Denounce
    {
        #region Definition of Properties
        public int Denounces_id { get; set; }
        public string Description { get; set; }
        public string state { get; set; }
        public int person_Id { get; set; }
        public int User_id { get; set; }
        public int Ticket_id { get; set; }
        public int Department_Id { get; set; }
        public string DepartmentName { get; set; }
        public string Photo { get; set; }

        public string Latitud { get; set; }
        public string Longitud { get; set; }


        #endregion
        #region Definition of Constructors
        public Denounce()
        {
            this.Denounces_id = 0;
            this.Description = string.Empty;
            this.state = string.Empty;
            this.person_Id = 0;
            this.Latitud = string.Empty;
            this.Longitud = string.Empty;
            this.Department_Id = 0;
            this.User_id = 0;
            this.Photo = string.Empty;
            this.Ticket_id = 0;
        }
        #endregion

    }
}
