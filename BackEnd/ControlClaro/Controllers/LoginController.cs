using Business.Models;
using Business.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Business.Utilities.Functions;

namespace ControlClaro.Controllers
{
    public class LoginController : ApiController
    {
        #region Definition of Services
        [HttpPost]
        [Route("api/login/verify/{username}/{password}")]
        public HttpResponseMessage VerifyCredentials(string username, string password)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            RestResponse data = new RestResponse();
            User user;
            string token;

            try
            {
                using (UserService service = new UserService())
                {
                    user = service.VerifyCredentials(username, password);                    
                }

                token = GenerateToken(user);

                data.status = true;
                data.result = new { user, token };
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Credenciales inválidos");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }

        [HttpPost]
        [Route("api/signin/sigup")]
        public HttpResponseMessage Guardar([FromBody] User user)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
               
                using (UserService service = new UserService())
                {
                    service.signUp(user);
                    data.result = null;
                    data.status = true;
                    data.message = "Registro exitoso";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Registro");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }



        #endregion
    }
}