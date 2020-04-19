using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
   public class Ticket
    {
        
        public string Ticketcol { get; set; }
        public int Ticket_id { get; set; }
        public int Department_id { get; set; }
        
       

        #region Definition of Constructors
        public Ticket()
        {
            this.Ticket_id = 0;
            this.Department_id = 0;
            this.Ticketcol = string.Empty;
      
        }
        #endregion
    
}
}
