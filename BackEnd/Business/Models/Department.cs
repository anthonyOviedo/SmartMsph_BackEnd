

namespace Business.Models
{
   public class Department
    {
        #region Definition of Properties
        public int department_Id { get; set; }
        public string name { get; set; }
        public string person_id { get; set; }
       public string personname { get; set; }

        #endregion
        #region Definition of Constructors
        public Department()
        {
            this.department_Id = 0;
            this.name = string.Empty;
            this.person_id = string.Empty;
            this.personname = string.Empty;
        }
        #endregion

    }
}
