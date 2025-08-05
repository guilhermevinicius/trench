using System.Globalization;
using MassTransit;

namespace Pulse.Product.MessageQueue.Extensions;

public static class Extensions
{
    public static void SetCulture(this ConsumeContext context, string cultureValue)
    {
        if (context.Headers.TryGetHeader(cultureValue, out _))
            Thread.CurrentThread.CurrentUICulture =
                CultureInfo.GetCultureInfo(context.Headers.Get<string>(cultureValue));
    }

    public static void SetCultureManual(this ConsumeContext context, string cultureValue)
    {
        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultureValue);
    }

    public static string GetDistributedTracingData(this ConsumeContext context)
    {
        return context.Headers.Get<string>("Transaction") ?? "";
    }
}