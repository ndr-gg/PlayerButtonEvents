using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;

namespace PlayerButtonEvents.Api;

public class ButtonEventBehavior(ButtonEvents buttonEvents)
{
    private Dictionary<int, PlayerButtons> _buttons = new();

    public void OnLoad(BasePlugin plugin)
    {
        plugin.Logger.LogInformation("Registering PlayerButtonEvents");

        plugin.RegisterListener<Listeners.OnTick>(OnTick);

        plugin.Logger.LogInformation("Registered PlayerButtonEvents");
    }


    public void OnUnload(BasePlugin plugin)
    {
        plugin.Logger.LogInformation("Unregistering PlayerButtonEvents");

        plugin.RemoveListener<Listeners.OnTick>(OnTick);

        plugin.Logger.LogInformation("Unregistered PlayerButtonEvents");
    }

    private void OnTick()
    {
        foreach (var player in Utilities.GetPlayers())
        {
            if (!player.IsValid) continue;
            if (player.Handle == IntPtr.Zero) continue;
            if (player.Connected != PlayerConnectedState.PlayerConnected) continue;

            PlayerButtons lastButtons;
            if (!_buttons.TryGetValue(player.Slot, out lastButtons))
            {
                lastButtons = player.Buttons;
            }

            _buttons[player.Slot] = player.Buttons;

            var delta = lastButtons ^ player.Buttons;

            if ((ulong)delta == 0) continue;

            for (var i = 0; i < 64; i++)
            {
                ulong mask = 1ul << i;

                if (((ulong)delta & mask) == 0) continue;

                var pressed = ((ulong)player.Buttons & mask) != 0;

                buttonEvents.OnStateChanged(this,
                    new ButtonEventArgs(player.Slot, (PlayerButtons)((ulong)delta & mask), pressed));
            }
        }
    }
}