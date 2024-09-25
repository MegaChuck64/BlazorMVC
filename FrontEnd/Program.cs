using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace FrontEnd
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            var address = builder.Configuration["BackEndURL"];
            if (string.IsNullOrWhiteSpace(address))
                throw new Exception("BackEndURL missing from front end config");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(address) });

            await builder.Build().RunAsync();
        }
    }
}
