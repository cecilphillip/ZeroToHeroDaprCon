namespace ContosoCrafts.CheckoutProcessor

// left semicolons to be more C#-ish
open Microsoft.AspNetCore.Builder;
open Microsoft.Extensions.Configuration;
open Microsoft.Extensions.DependencyInjection;

type Startup(configuration: IConfiguration) =

    member this.Configuration = configuration;

    member this.ConfigureServices(services: IServiceCollection) =
        services.AddControllers();

    member this.Configure(app: IApplicationBuilder) =
        // warning here for not using a proper ignore
        app.UseRouting();
        app.UseEndpoints(fun endpoints ->
            // no ignore here would be an error for a lambda function
            endpoints.MapControllers()
            |> ignore<ControllerActionEndpointConventionBuilder>
        );