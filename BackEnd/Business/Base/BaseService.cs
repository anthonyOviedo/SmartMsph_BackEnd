using Connection.Classes;
using static Business.Utilities.Declarations;
using static Business.Utilities.Functions;

namespace Business.Base
{
    public abstract class BaseService
    {
        #region Definition of Properties
        protected ConnectionManager connection { get; set; }
        #endregion
        #region Definition of Constructors
        public BaseService()
        {
            this.connection = new ConnectionManager(GetConnectionString(nameTagConnection));
            FormatRegionalOptions();
        }

        public BaseService(string connection)
        {
            this.connection = new ConnectionManager(connection);
            FormatRegionalOptions();
        }
        #endregion
    }
}