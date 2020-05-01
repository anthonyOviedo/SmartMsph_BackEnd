
using Business.Models;
using Business.Services;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Business.Utilities.Functions;




namespace ControlClaro.Controllers
{
    public class NewController : ApiController
    {
        [HttpPost]
        [Route("api/New/agregarNoticia/{Noticia}")]
        public HttpResponseMessage PostFile(News Noticia)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (NewService service = new NewService())
                {
                    service.NewInsert(Noticia.new_id,Noticia.descripcion, Noticia.fileToUpload, Noticia.titulo);
                    data.result = null;
                    data.status = true;
                    data.message = Noticia.new_id == 0 ? "Se creo la noticia":"Se actualizó corretamenta la noticia";
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
        [Route("api/New/ObtenerNoticias")]
        public HttpResponseMessage ObtenerNoticias()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);



                using (NewService service = new NewService())
                {
                    var Noticias = service.TodasLasNoticias();
                    data.result = new { Noticias };
                    data.status = true;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Lista de Noticias");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }


        [HttpDelete]
        [Route("api/news/delete/{news_id}")]
        public HttpResponseMessage DeleletComplain(int news_id)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (NewService service = new NewService())
                {
                    service.Delete(news_id);
                    data.result = null;
                    data.status = true;
                    data.message = "Se ha eliminado la noticia";
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