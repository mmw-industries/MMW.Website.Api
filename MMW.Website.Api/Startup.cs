using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MMW.Website.Api.MsGraphApi;

namespace MMW.Website.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // MsGraph Init
            Settings.Tenant = "b0662a48-e861-4389-b5b2-573693d5ab0a";
            Settings.ClientId = "13b24ea4-f124-40a1-b355-c8122114f2fa";
            Settings.ClientSecret = "LS-7Q~F9coK.nX740Zd.yWvrlmrhPgLGdbvQI";

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
                options.AddDefaultPolicy(builder =>
                    builder
                        .WithOrigins(
                            "https://mmw.industries",
                            "https://www.mmw.industries")
                        .AllowAnyHeader()
                        .AllowAnyMethod()));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}