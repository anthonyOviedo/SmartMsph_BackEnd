using Business.Models;
using Business.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Business.Utilities.Functions;

namespace ControlClaro.Controllers
{
    public class ComplainController : ApiController
    {
 /*       //revisar
        [HttpGet]
        [Route("api/queja/lista")]
        public HttpResponseMessage ListaDepartamentos()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

             

                using (ComplainService service = new ComplainService())
                {
                    var departamentos = service.TodosDespartamentos();
                    data.result = new { departamentos };
                    data.status = true;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Lista de Departamentos");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }

        //revisar
        [HttpGet]
        [Route("api/Funcionario/listaFuncionarios/{depSeleccion}")]
        public HttpResponseMessage ListaFuncionarios( string depSeleccion)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);



                using (ComplainService service = new ComplainService())
                {
                    var funcionarios = service.TodosFuncionarios(depSeleccion);
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
*/
       
        [HttpPost]
        [Route("api/queja/GuardarQueja/{queja}")]
        public HttpResponseMessage PostFile(Complain queja)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (ComplainService service = new ComplainService())
                {
                    service.GuardarQueja(queja.Description,queja.state, queja.person_Id, Int32.Parse(queja.User_id), queja.employee.ToString(), queja.employee.ToString(), queja.department_id);
                    data.result = null;
                    data.status = true;
                    data.message = "Se creo la queja";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "hubo un error");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }

        //to do 

        //delete / cancel

        //update

        //list

        //list


    }
}