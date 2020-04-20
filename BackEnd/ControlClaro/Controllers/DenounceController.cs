using Business.Models;
using Business.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Business.Utilities.Functions;
namespace ControlClaro.Controllers
{
    public class DenounceController : ApiController
    {
        #region Definition of Services
        [HttpGet]
        [Route("api/denounce/tikets/{Department_id}/{Ticketcol}")]
        public HttpResponseMessage obtainTicket(int Department_id,string Ticketcol)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);



                using (DenounceService service = new DenounceService())
                {
                    var tike = service.obtainticket(Department_id,Ticketcol);
                    data.result = new { tike };
                    data.status = true;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "tikete");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }

        [HttpPost]
        [Route("api/denounce/newDenounce/{Denuncia}")]
        public HttpResponseMessage add(Denounce denounce)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (DenounceService service = new DenounceService())
                {
                    service.add(denounce);
                    data.result = null;
                    data.status = true;
                    data.message = "Se creo la Denuncia";
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
        [Route("api/denounce/delete/{denounceId}")]
        public HttpResponseMessage delete(string denounceId)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (DenounceService service = new DenounceService())
                {
                    service.delete(denounceId);
                    data.result = null;
                    data.status = true;
                    data.message = "La queja se eliminó correctamente";
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
        [Route("api/denounce/update")]
        public HttpResponseMessage update([FromBody] Denounce denounce)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (DenounceService service = new DenounceService())
                {
                    service.update(denounce);
                    data.result = null;
                    data.status = true;
                    data.message = "Actualizacion de Quejas";
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


        [HttpGet]
        [Route("api/denounce/list")]
        public HttpResponseMessage list()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                using (DenounceService service = new DenounceService())
                {
                    var complains = service.list();
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

        //todo
        //search
        #endregion
    }
}