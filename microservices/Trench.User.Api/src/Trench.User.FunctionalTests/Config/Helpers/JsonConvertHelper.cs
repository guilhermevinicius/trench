using System.Text.Json;

namespace Trench.User.FunctionalTests.Config.Helpers;

internal static class JsonConvertHelper
{
    internal static T? DeserializeObject<T>(string dataString)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<T>(dataString, options);
    }
}