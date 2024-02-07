using System.Text.Json.Serialization;

namespace KaamelottSampler.Models
{
    public class Saample
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("character")]
        public string Character { get; set; }

        [JsonPropertyName("episode")]
        public string Episode { get; set; }

        [JsonPropertyName("file")]
        public string File { get; set; }

        [JsonPropertyName("imagefile")]
        public string Imagefile { get; set; }

    }
}
