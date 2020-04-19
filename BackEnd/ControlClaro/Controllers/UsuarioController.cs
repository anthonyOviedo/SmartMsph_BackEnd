using Business.Models;
using Business.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Business.Utilities.Functions;

namespace ControlClaro.Controllers
{
    public class UsuarioController : ApiController
    {
        #region Definition of Services
        [HttpGet]
        [Route("api/usuario/lista/{pais_Id}/{emp_Id}/{criterio}")]
        public HttpResponseMessage Lista(int pais_Id, int emp_Id, string criterio)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                if (criterio == "_TODO_")
                    criterio = "";

                using (UsuarioService service = new UsuarioService())
                {
                    var usuarios = service.Lista(pais_Id, emp_Id, criterio);
                    data.result = new { usuarios, service.cantidad };
                    data.status = true;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Lista de usuarios");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }

        
        [HttpDelete]
        [Route("api/usuario/eliminar/{usuario_Id}/{emp_Id}/{producto_Id}")]
        public HttpResponseMessage Eliminar(string usuario_Id)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (UsuarioService service = new UsuarioService())
                {
                    service.Eliminar(usuario_Id);
                    data.result = null;
                    data.status = true;
                    data.message = "El usuario seleccionado se eliminó correctamente";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Eliminar usuario");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }

        [HttpPost]
        [Route("api/usuario/change-password/{newPassword}")]
        public HttpResponseMessage ChangePassword([FromBody] Usuario usuario, string newPassword)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (UsuarioService service = new UsuarioService())
                {
                    service.ChangePassword(usuario.usuario_Id, usuario.password, newPassword);
                    data.result = null;
                    data.status = true;
                    data.message = "El cambio de contraseña se completó correctamente";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Cambio de contraseña");
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