using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
 public   class News
    {

        #region Definition of Properties
        public string newsId { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }      
        public string fileToUpload { get; set; }

        #endregion
        #region Definition of Constructors
        public News()
        {
            this.newsId = string.Empty;
            this.titulo = string.Empty;
            this.descripcion = string.Empty;
            this.fileToUpload = string.Empty;
        }
        #endregion
    }

}

