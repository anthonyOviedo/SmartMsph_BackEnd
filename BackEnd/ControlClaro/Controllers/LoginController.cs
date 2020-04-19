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
            Usuario usuario;
            string token;

            try
            {
                using (UsuarioService service = new UsuarioService())
                {
                    usuario = service.VerifyCredentials(username, password);                    
                }

                token = GenerateToken(usuario);

                data.status = true;
                data.result = new { usuario, token };
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
        [Route("api/signin/registrarse")]
        public HttpResponseMessage Guardar([FromBody] Usuario usuario)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
               
                using (UsuarioService service = new UsuarioService())
                {
                    service.Registrarse(usuario);
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