// Copyright (c) 2024 ndr.gg
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of
// the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;

namespace PlayerButtonEvents.Api;

public class ButtonEventBehavior(ButtonEvents buttonEvents)
{
    public void OnLoad(BasePlugin plugin)
    {
        plugin.Logger.LogTrace("Registering PlayerButtonEvents");

        plugin.RegisterListener<Listeners.OnTick>(OnTick);

        plugin.Logger.LogTrace("Registered PlayerButtonEvents");
    }


    public void OnUnload(BasePlugin plugin)
    {
        plugin.Logger.LogTrace("Unregistering PlayerButtonEvents");

        plugin.RemoveListener<Listeners.OnTick>(OnTick);

        plugin.Logger.LogTrace("Unregistered PlayerButtonEvents");
    }

    private readonly Dictionary<int, PlayerButtons> _buttons = new();

    private void OnTick()
    {
        foreach (var player in Utilities.GetPlayers())
        {
            if (!player.IsValid) continue;
            if (player.Handle == IntPtr.Zero) continue;
            if (player.Connected != PlayerConnectedState.PlayerConnected) continue;

            if (!_buttons.TryGetValue(player.Slot, out var lastButtons))
            {
                lastButtons = player.Buttons;
            }

            _buttons[player.Slot] = player.Buttons;

            var delta = lastButtons ^ player.Buttons;

            if ((ulong)delta == 0) continue;

            for (var i = 0; i < 64; i++)
            {
                var mask = 1ul << i;

                if (((ulong)delta & mask) == 0) continue;

                var pressed = ((ulong)player.Buttons & mask) != 0;

                buttonEvents.OnStateChanged(this,
                    new ButtonEventArgs(player.Slot, (PlayerButtons)((ulong)delta & mask), pressed));
            }
        }
    }
}