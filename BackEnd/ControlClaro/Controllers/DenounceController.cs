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
        public HttpResponseMessage add(Denounce Denuncia)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (DenounceService service = new DenounceService())
                {
                    service.add(Denuncia.Description, Denuncia.state, Denuncia.Ticket_id, Denuncia.person_Id, Denuncia.User_id, Denuncia.Department_Id, Denuncia.Photo, Denuncia.Latitud.ToString(), Denuncia.Longitud.ToString());
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

        //to do 

        //delete / cancel

        //update

        //list

        //search

    }
}