using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using EventAggregator.Blazor;

using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpClient("dapr", c =>
            {
                c.BaseAddress = new Uri("http://localhost:3500");
                c.DefaultRequestHeaders.Add("User-Agent", typeof(Program).Assembly.GetName().Name);
            });
            services.AddControllers();
            services.AddScoped<IEventAggregator, EventAggregator.Blazor.EventAggregator>();
            services.AddSingleton<IProductService, DaprProductService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler("/Error");

            app.UseHsts();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
            });
        }
    }
}
