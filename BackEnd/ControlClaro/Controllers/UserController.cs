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
        [Route("api/user/list")]
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
        [Route("api/user/delete/{usuario_Id}/{emp_Id}/{producto_Id}")]
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
                    data.message = "El user seleccionado se eliminó correctamente";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Eliminar user");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }

        [HttpPost]
        [Route("api/user/ChangePassword/{newPassword}")]
        public HttpResponseMessage ChangePassword([FromBody] User user, string newPassword)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (UserService service = new UserService())
                {
                    service.ChangePassword(user.usuario_Id, user.password, newPassword);
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
        [Route("api/user/update")]
        public HttpResponseMessage update([FromBody] User user)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (UserService service = new UserService())
                {
                    service.update(user);
                    data.result = null;
                    data.status = true;
                    data.message = "El Registro del user se completó correctamente";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Registro del user");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }

        [HttpPost]
        [Route("api/user/add")]
        public HttpResponseMessage add([FromBody] User user)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (UserService service = new UserService())
                {
                    service.add(user);
                    data.result = null;
                    data.status = true;
                    data.message = "El Registro del user se completó correctamente";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Registro del user");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }

        //todo 
            //search


        #endregion
    }
}