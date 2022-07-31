module ContosoCrafts.CheckoutProcessor.Program

open Microsoft.AspNetCore.Hosting;
open Microsoft.Extensions.Hosting;

let createHostBuilder (args: string[]) : IHostBuilder =
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(fun webBuilder ->
            webBuilder.UseStartup<Startup>()
            // this is necessary in F# in a lambda of the type
            // if you intend to ignore a value you must be explicit
            // this would have also worked, but I prefer to strongly type my ignores
            // |> ignore
            |> ignore<IWebHostBuilder>
        );

// this attribute may have been required
// not sure why I was getting a warning without it
[<EntryPoint>]
let main (args: string[]) =
    createHostBuilder(args).Build().Run();
    0;
