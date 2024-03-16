using System.Text.Json.Serialization;

namespace WebApplication9.Common;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ClaimType
{
    Id
}