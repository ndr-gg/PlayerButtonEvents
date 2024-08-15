# PlayerButtonEvents

PlayerButtonEvents is a library that provides events for handling button presses in CS2. It allows developers to easily subscribe to and handle events triggered when a player presses or releases buttons.

## Features

- Exposes events for button presses

## Installation

- [NuGet](https://www.nuget.org/packages/PlayerButtonEvents.Api)

## Usage

[Check out the example plugin](../../PlayerButtonEvents.TestPlugin/PlayerButtonEventsTestPlugin.cs)

-or-

```csharp

public class PlayerButtonEventsTestPlugin(ButtonEventBehavior buttonEventBehavior, ButtonEvents buttonEvents)
    : BasePlugin
{
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

    /*
    ButtonEventArgs contains
        Slot => The <slot> the player takes up in the server, get the controller with `Utilities.GetPlayerFromSlot`
        Button => The *single* button this event is for, if the player pressed D, it will be PlayerButtons.Moveright
        IsPressed => Whether the button has been pressed or released, only really useful if you're doing a ButtonStateChanged hook
    */
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
```
