using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class Complain
    {
        #region Definition of Properties
        public int Complain_Id { get; set; }
        public string Description { get; set; }
        public string state { get; set; }
        public int person_Id { get; set; }
        public int User_id { get; set; }
        public string employee { get; set; }
        public int department_id { get; set; }
        public string departmentname { get; set; }
        public string fecha { get; set; }

        public string employee_name { get; set; }
        #endregion
        #region Definition of Constructors
        public Complain()
        {
            this.Complain_Id = 0;
            this.Description = string.Empty;
            this.state =string.Empty;
            this.person_Id = 0;
            this.employee = string.Empty;
            this.employee_name = string.Empty;
            this.department_id = 0;
            this.User_id = 0;
            this.departmentname = string.Empty;


        }
        #endregion

    }
}
