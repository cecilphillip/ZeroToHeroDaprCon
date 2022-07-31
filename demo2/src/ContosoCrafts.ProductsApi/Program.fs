module ContosoCrafts.ProductsApi.Program

open Microsoft.AspNetCore.Hosting;
open Microsoft.Extensions.Hosting;


// public static IHostBuilder createHostBuilder(string[] args) =>
let createHostBuilder (args: string[]): IHostBuilder =
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(fun webBuilder ->
            webBuilder.UseStartup<Startup>()
            |> ignore
        );

[<EntryPoint>]
let main(args: string[]) =
    createHostBuilder(args).Build().Run();
    0
