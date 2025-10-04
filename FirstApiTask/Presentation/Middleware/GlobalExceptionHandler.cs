namespace FirstApiTask.Presentation.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionHandler> logger;
        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            this.next = next;
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "error");

                context.Response.Clear();
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = ex switch
                {
                    InvalidOperationException => StatusCodes.Status409Conflict,
                    TimeoutException => StatusCodes.Status408RequestTimeout,
                    FormatException => StatusCodes.Status400BadRequest,
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    ArgumentException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };

                var message = new 
                { 
                    Error = ex.Message,
                    StatusCode = context.Response.StatusCode  
                };
                
                await context.Response.WriteAsJsonAsync(message);
            }
        }
    }
}
