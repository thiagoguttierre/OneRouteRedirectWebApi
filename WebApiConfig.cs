  public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.MapHttpAttributeRoutes();

            // Web API routes

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{service}"
            );

            config.Services.Replace(typeof(IHttpControllerSelector), new CustomSelector(config));
        }
    }
    
    public class CustomSelector : DefaultHttpControllerSelector
    {
        readonly HttpConfiguration _configuration;

        public CustomSelector(HttpConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            return new HttpControllerDescriptor(
                _configuration,
                "DefaultController",
                typeof(DefaultController));
        }
    }
