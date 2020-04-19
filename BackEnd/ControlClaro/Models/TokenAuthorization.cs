using System;
using System.Web.Http.Controllers;
using static Business.Utilities.Functions;

namespace ControlClaro.Models
{
    public static class TokenAuthorization
    {
        public static string VerifyAuthorization(HttpActionContext actionContext)
        {
            try
            {
                

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}