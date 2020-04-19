using Business.Models;
using Business.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Business.Utilities.Functions;

namespace ControlClaro.Controllers
{
    public class UserController : ApiController
    {
        #region Definition of Services
        [HttpGet]
        [Route("api/usuario/list")]
        public HttpResponseMessage list()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (UserService service = new UserService())
                {
                    var usuarios = service.list();
                    data.result = new { usuarios };
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
        public HttpResponseMessage delete(string usuario_Id)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (UserService service = new UserService())
                {
                    service.delete(usuario_Id);
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
        [Route("api/usuario/cambiar-contraseña/{newPassword}")]
        public HttpResponseMessage ChangePassword([FromBody] User usuario, string newPassword)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (UserService service = new UserService())
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

        [HttpPost]
        [Route("api/usuario/actualizar")]
        public HttpResponseMessage update([FromBody] User usuario)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (UserService service = new UserService())
                {
                    service.update(usuario);
                    data.result = null;
                    data.status = true;
                    data.message = "El Registro del usuario se completó correctamente";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Registro del usuario");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }

        [HttpPost]
        [Route("api/usuario/agregar")]
        public HttpResponseMessage add([FromBody] User usuario)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (UserService service = new UserService())
                {
                    service.add(usuario);
                    data.result = null;
                    data.status = true;
                    data.message = "El Registro del usuario se completó correctamente";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Registro del usuario");
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