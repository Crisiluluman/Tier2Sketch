using System.Text.Json.Serialization;

namespace Tier2Sketch
{
    public class DummyObject
    {
        [JsonPropertyName("name")]
        public string name { get; set; }
        
        [JsonPropertyName("age")]
        public int age { get; set; }
        
        [JsonPropertyName("salary")]
        public double salaray { get; set; }

    }
}