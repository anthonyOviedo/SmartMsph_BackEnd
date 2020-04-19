using Business.Models;
using Business.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Business.Utilities.Functions;

namespace ControlClaro.Controllers
{
    public class DepartmentController : ApiController
    {
        #region Definition of Services

        [HttpGet]   
        [Route("api/department/list")]
        public HttpResponseMessage list()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                using (DepartmentService service = new DepartmentService())
                {
                    var departments = service.list();
                    data.result = new { departments };
                    data.status = true;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Lista de departamentos");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }

        [HttpPost]
        [Route("api/department/add")]
        public HttpResponseMessage add([FromBody] Department department)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using ( DepartmentService service = new DepartmentService())
                {
                    service.add(department);
                    data.result = null;
                    data.status = true;
                    data.message = "Department creado";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Registro de Departamentos");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }


        [HttpDelete]
        [Route("api/department/delete/{departmentId}")]
        public HttpResponseMessage delete(string departmentId)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (DepartmentService service = new DepartmentService())
                {
                    service.delete(departmentId);
                    data.result = null;
                    data.status = true;
                    data.message = "El Department seleccionado se eliminó correctamente";
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
        [Route("api/department/update")]
        public HttpResponseMessage update([FromBody] Department Department)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (DepartmentService service = new DepartmentService())
                {
                    service.update(Department);
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

        //to do 

        //search


        #endregion
    }
}
