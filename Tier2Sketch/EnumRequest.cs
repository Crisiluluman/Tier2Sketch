using System.Text.Json.Serialization;

namespace Tier2Sketch
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnumRequest
    {
        //Test stuff
        GetFromTier3,
        SendToTier3
    }
}