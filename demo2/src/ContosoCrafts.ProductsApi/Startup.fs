namespace ContosoCrafts.ProductsApi

open System.Text.Json;
open Microsoft.AspNetCore.Builder;
open Microsoft.Extensions.Configuration;
open Microsoft.Extensions.DependencyInjection;
open MongoDB.Driver;

open ContosoCrafts.ProductsApi.Services;

type Startup(configuration: IConfiguration) =

    member this.Configuration = configuration

    member this.ConfigureServices(services:IServiceCollection ) =
        // compiler warning:
        // AddControllers returns a value that you aren't catching or ignoring explicitly
        services.AddControllers()
                .AddJsonOptions(fun option ->
                    option.JsonSerializerOptions.IgnoreNullValues <- true;
                    option.JsonSerializerOptions.PropertyNamingPolicy <- JsonNamingPolicy.CamelCase;
                );
        // compiler warning:
        // AddSingleton returns a value that you aren't catching or ignoring explicitly
        services.AddSingleton<IMongoClient>(fun provider ->
            let config = provider.GetService<IConfiguration>();
            #if FSHARP6
            new MongoClient(config["MONGO_CONNECTION"]) :> IMongoClient
            #else
            new MongoClient(config.["MONGO_CONNECTION"]) :> IMongoClient
            #endif
        );
        services.AddTransient<IProductService, ProductService>();

    member this.Configure(app: IApplicationBuilder) =
        app.UseRouting();
        app.UseEndpoints(fun endpoints ->
            // compiler will error here without ignore, delegates won't implicitly ignore
            endpoints.MapControllers()
            |> ignore
        );
