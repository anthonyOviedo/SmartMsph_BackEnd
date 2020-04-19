using System.Web.Http;
using System.Web.Http.Cors;
using WebApiContrib.Formatting.Jsonp;

namespace ControlClaro
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Habilita JSON por default
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            // Habilita JSONP
            var formatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter, "callback");
            config.Formatters.Insert(0, formatter);

            // Habilita CORS
            config.EnableCors(new EnableCorsAttribute("*", "*", "GET, POST, PUT, DELETE, OPTIONS"));
        }
    }
}