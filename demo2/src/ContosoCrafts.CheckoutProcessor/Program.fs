open Microsoft.AspNetCore.Hosting;
open Microsoft.Extensions.Hosting;

namespace ContosoCrafts.CheckoutProcessor

module Program =
    let createHostBuilder(string[] args) : IHostBuilder =
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

let main (args: string[]) =
    Program.CreateHostBuilder(args).Build().Run();
