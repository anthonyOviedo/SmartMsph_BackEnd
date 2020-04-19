namespace Business.Models
{
    public class ResponseConfig
    {
        #region Definition of Properties
        public bool isAuthenticated { get; set; }
        public string errorMessage { get; set; }
        public User usuario { get; set; }
        #endregion
        #region Definition of Constructors
        public ResponseConfig()
        {
            this.isAuthenticated = true;
            this.errorMessage = string.Empty;
            this.usuario = new User();
        }
        #endregion
    }
}