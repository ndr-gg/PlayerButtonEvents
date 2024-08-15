using CounterStrikeSharp.API;

namespace PlayerButtonEvents.Api;

public class ButtonEventArgs(int slot, PlayerButtons button, bool isPressed)
{
    /// <summary>
    /// The slot the player takes up in the server, get the controller with <see cref="Utilities.GetPlayerFromSlot"/>
    /// </summary>
    public readonly int Slot = slot;

    /// <summary>
    /// The *single* button this event is for, if the player pressed D, it will be <see cref="PlayerButtons.Moveright" />
    /// </summary>
    public readonly PlayerButtons Button = button;

    /// <summary>
    /// Whether the button has been pressed or released, only really useful if you're doing a <see cref="ButtonEvents.ButtonStateChanged"/> hook
    /// </summary>
    public readonly bool IsPressed = isPressed;
}