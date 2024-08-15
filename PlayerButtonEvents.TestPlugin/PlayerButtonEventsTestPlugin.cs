using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using PlayerButtonEvents.Api;

namespace PlayerButtonEvents.TestPlugin;

public class PlayerButtonEventsTestPlugin(ButtonEventBehavior buttonEventBehavior, ButtonEvents buttonEvents)
    : BasePlugin
{
    public override string ModuleAuthor => "nadir <ndr.gg>";
    public override string ModuleName => "PBE Test Plugin";
    public override string ModuleVersion => ThisAssembly.AssemblyVersion;

    public override void Load(bool hotReload)
    {
        buttonEventBehavior.OnLoad(this);

        buttonEvents.ButtonPressed += OnButtonPressed;
        buttonEvents.ButtonReleased += OnButtonReleased;
    }

    public override void Unload(bool hotReload)
    {
        buttonEvents.ButtonPressed -= OnButtonPressed;
        buttonEvents.ButtonReleased -= OnButtonReleased;

        buttonEventBehavior.OnUnload(this);
    }

    private void OnButtonPressed(object? sender, ButtonEventArgs e)
    {
        var controller = Utilities.GetPlayerFromSlot(e.Slot);

        Server.PrintToChatAll($"Button pressed: {e.Button} by {controller.PlayerName}");
    }

    private void OnButtonReleased(object? sender, ButtonEventArgs e)
    {
        var controller = Utilities.GetPlayerFromSlot(e.Slot);

        Server.PrintToChatAll($"Button released: {e.Button} by {controller.PlayerName}");
    }
}