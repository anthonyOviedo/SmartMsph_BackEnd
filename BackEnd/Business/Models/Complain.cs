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
        //hkh
        public string Description { get; set; }
        public int state { get; set; }
        public int person_Id { get; set; }
        public string User_id { get; set; }
        public int employee { get; set; }
        public int department_id { get; set; }

        public int employee_department { get; set; }
        #endregion
        #region Definition of Constructors
        public Complain()
        {
            this.Complain_Id = 0;
            this.Description = string.Empty;
            this.state = 1;
            this.person_Id = 0;
            this.employee = 0;
            this.employee_department = 0;
            this.department_id = 0;
            this.User_id = string.Empty;
        }
        #endregion

    }
}
