﻿using Business.Models;
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
        #region Definition of Services
        [HttpDelete]
        [Route("api/complain/delete/{complainId}")]
        public HttpResponseMessage delete(string complainId)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (ComplainService service = new ComplainService())
                {
                    service.delete(complainId);
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
        [Route("api/complain/update")]
        public HttpResponseMessage update([FromBody] Complain complain)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (ComplainService service = new ComplainService())
                {
                    service.update(complain);
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
        [Route("api/complain/list")]
        public HttpResponseMessage list()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                using (ComplainService service = new ComplainService())
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

        [HttpPost]
        [Route("api/complain/add")]
        public HttpResponseMessage Add(Complain complain)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();


            try
            {
                VerifyMessage(config.errorMessage);

                using (ComplainService service = new ComplainService())
                {
                    service.add(complain, config.user);
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

        //todo
        //search

        #endregion
    }
}