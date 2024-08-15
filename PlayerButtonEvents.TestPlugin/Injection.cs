using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.DependencyInjection;
using PlayerButtonEvents.Api;

namespace PlayerButtonEvents.TestPlugin;

public class Injection : IPluginServiceCollection<PlayerButtonEventsTestPlugin>
{
    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ButtonEventBehavior>();
        serviceCollection.AddSingleton<ButtonEvents>();
    }
}