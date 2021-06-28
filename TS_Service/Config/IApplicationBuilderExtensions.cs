using Microsoft.AspNetCore.Builder;

namespace TS_Api.Config
{
    public static class IApplicationBuilderExtensions
    {
        public static void UseCorsConfig(this IApplicationBuilder app)
        {
            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });
        }

        public static void UseEndpointsConfig(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        public static void UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "TS API - V1");  });
        }
    }
}
