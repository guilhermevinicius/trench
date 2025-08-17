using Trench.Notification.Api.Dtos;

namespace Trench.Notification.FunctionalTests.Config.Helpers;

internal static class JsonHelper
{
    internal static async Task<CustomResponse?> DeserializeResponse(HttpResponseMessage response)
    {
        var responseRequest = await response.Content.ReadAsStringAsync();
        return JsonConvertHelper.DeserializeObject<CustomResponse>(responseRequest);
    }
}