using Business.Models;
using Business.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Business.Utilities.Functions;

namespace ControlClaro.Controllers
{
    public class DepartmentController:ApiController
    {

        [HttpGet]
        [Route("api/Funcionario/allfuncionary/")]
        public HttpResponseMessage ListaFuncionarios( )
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);



                using (ComplainService service = new ComplainService())
                {
                    var funcionarios = service.allfuncionary();
                    data.result = new { funcionarios };
                    data.status = true;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Lista de Funcionarios");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }

        [HttpPost]
        [Route("api/department/guardar/")]
        public HttpResponseMessage SaveDenounce([FromBody] Department Department)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (DepartmentService service = new DepartmentService())
                {
                    service.saveDepartment(Department);
                    data.result = null;
                    data.status = true;
                    data.message = Department.department_Id == -1 ? "Se creó el departamento" : "Se actualizó el departamento";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Hubo un error");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }



        [HttpDelete]
        [Route("api/department/delete/{department_id}")]
        public HttpResponseMessage DeleletComplain(int department_id)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (DepartmentService service = new DepartmentService())
                {
                    service.DeleteDepartment(department_id);
                    data.result = null;
                    data.status = true;
                    data.message = "Se ha eliminado el departamento";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Eliminar departamento");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }









    }
}