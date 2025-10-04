namespace FirstApiTask.Presentation.Middleware
{
    public static class ApplicationBuilder
    {
        public static IApplicationBuilder UseAppBuilder(this IApplicationBuilder app, IWebHostEnvironment evm)
        {
            app.UseMiddleware<GlobalExceptionHandler>();

            if (evm.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            return app;
        }
    }
}
