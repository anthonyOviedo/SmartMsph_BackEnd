namespace Business.Models
{
    public class RestError
    {
        #region Definition of Properties
        public long code { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        #endregion
        #region Definition of Constructors
        public RestError()
        {
            this.code = 0;
            this.title = string.Empty;
            this.message = string.Empty;
            this.data = null;
        }
        #endregion
    }
}