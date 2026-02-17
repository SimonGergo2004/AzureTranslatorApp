using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TranslatorApp.Models;

namespace TranslatorApp.Controllers
{
    public class TranslationController : Controller
    {
        private readonly BlobServiceClient blobServiceClient;
        private readonly HttpClient httpClient;

        public TranslationController(BlobServiceClient blobServiceClient, HttpClient httpClient)
        {
            this.blobServiceClient = blobServiceClient;
            this.httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult MainPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TranslateAndUpload([FromForm] TranslationData request)
        {
            // 1. Fordítás Azure Translator segítségével
            var body = new[] { new { Text = request.OriginalText } };
            var requestJson = JsonConvert.SerializeObject(body);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var url = $"https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&to={request.TargetLanguage}";
            var response = await httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseJson);
            request.TranslatedText = result[0].translations[0].text;

            // 2. Blob storage-ba mentés
            var containerClient = blobServiceClient.GetBlobContainerClient("forditott-fajlok");
            await containerClient.CreateIfNotExistsAsync();

            string fileName = $"{request.Id}.txt";
            var blobClient = containerClient.GetBlobClient(fileName);

            string toUpload = $"Original language:\t{request.OriginalLanguage}\n{request.OriginalText}\nTarget language:\t{request.TargetLanguage}\n{request.TranslatedText}";
            using var ms = new MemoryStream(Encoding.UTF8.GetBytes(toUpload));
            await blobClient.UploadAsync(ms, overwrite: true);


            return View("MainPage", request);
        }
    }
}
