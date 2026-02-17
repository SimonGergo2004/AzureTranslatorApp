using Azure.Storage.Blobs;

namespace TranslatorApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();


            string translatorEndpoint = builder.Configuration["Translator:Endpoint"];
            string translatorKey = builder.Configuration["Translator:Key"];

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", translatorKey);
            builder.Services.AddSingleton(httpClient);

            string storageConnectionString = builder.Configuration.GetConnectionString("StorageAccount");
            var blobServiceClient = new BlobServiceClient(storageConnectionString);
            builder.Services.AddSingleton(blobServiceClient);

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Translation}/{action=MainPage}/{id?}");

            app.Run();
        }
    }
}
