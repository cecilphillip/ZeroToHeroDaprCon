// left semicolons to be more C#-ish
open Microsoft.AspNetCore.Builder;
open Microsoft.Extensions.Configuration;
open Microsoft.Extensions.DependencyInjection;

namespace ContosoCrafts.CheckoutProcessor

public type Startup(configuration: IConfiguration) =

    member Configuration = configuration;

    member this.ConfigureServices(services: IServiceCollection) =
        services.AddControllers();

    member this.Configure(app: IApplicationBuilder) =
        app.UseRouting();
        app.UseEndpoints(fun endpoints ->
            endpoints.MapControllers();
        );