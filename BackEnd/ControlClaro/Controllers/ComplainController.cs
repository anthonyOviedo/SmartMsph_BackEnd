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
                    service.GuardarQueja(queja.Description,queja.state, queja.person_Id, queja.User_id, queja.employee_name, queja.employee, queja.department_id,queja.fecha);
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


        [HttpGet]
        [Route("api/Complain/List/{Id_User}")]
        public HttpResponseMessage ListComplainsbyId(int Id_User)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);



                using (ComplainService service = new ComplainService())
                {
                    var complains = service.ListComplainsbyId(Id_User);
                    data.result = new { complains }; 
                    data.status = true;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Lista de Quejas");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }

        [HttpPost]
        [Route("api/complain/Update/{complain}")]
        public HttpResponseMessage UpdateComplain(Complain complain)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (ComplainService service = new ComplainService())
                {
                    service.UpdateComplain(complain.Complain_Id, complain.Description,complain.employee,complain.employee_name,complain.department_id,complain.fecha);
                    data.result = null;
                    data.status = true;
                    data.message = "Se actualizo la queja";
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


        [HttpDelete]
        [Route("api/complain/delete/{Complain_id}")]
        public HttpResponseMessage DeleletComplain(int Complain_id)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (ComplainService service = new ComplainService())
                {
                    service.DeleteComplain(Complain_id);
                    data.result = null;
                    data.status = true;
                    data.message = "Se ha eliminado la queja";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Eliminar queja");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }




    }
}