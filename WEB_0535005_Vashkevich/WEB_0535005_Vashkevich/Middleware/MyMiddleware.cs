namespace WEB_0535005_Vashkevich.Middleware
{
    public class MyMiddleware
    {
        private RequestDelegate _next;

        public MyMiddleware(RequestDelegate next) 
        {
            _next= next;
        }

        public async Task InvokeAsync(HttpContext context, ILoggerFactory factory) 
        {
            var time = DateTime.Now;

            await _next(context);

            var statusCode = context.Response.StatusCode;
            if (statusCode != StatusCodes.Status200OK) 
            {
                var logger = factory.CreateLogger<MyMiddleware>();
                
                logger.LogInformation($"{time.ToString()} {context.Request.QueryString.ToString()} Request: {context.Request.Path} returns status code {statusCode}");
            }
        }
    }

    public static class MiddlewareExtensions 
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder app) 
        {
            return app.UseMiddleware<MyMiddleware>();
        }
    } 
}
