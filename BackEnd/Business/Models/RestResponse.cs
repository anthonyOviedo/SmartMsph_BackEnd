namespace Business.Models
{
    public class RestResponse
    {
        #region Definition of Properties
        public bool status { get; set; }
        public string message { get; set; }
        public object result { get; set; }
        public object error { get; set; }
        #endregion
        #region Definition of Constructors
        public RestResponse()
        {
            this.status = false;
            this.message = string.Empty;
            this.result = null;
            this.error = null;
        }
        #endregion
    }
}