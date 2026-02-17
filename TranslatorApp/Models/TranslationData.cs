using Newtonsoft.Json;

namespace TranslatorApp.Models
{
    public class TranslationData
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("originalText")]
        public string OriginalText { get; set; }

        [JsonProperty("originalLanguage")]
        public string OriginalLanguage { get; set; }

        [JsonProperty("translatedText")]
        public string TranslatedText { get; set; }

        [JsonProperty("targetLanguage")]
        public string TargetLanguage { get; set; }
    }
}
