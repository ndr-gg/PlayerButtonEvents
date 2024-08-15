using CounterStrikeSharp.API;

namespace PlayerButtonEvents.Api;

public class ButtonEventArgs(int slot, PlayerButtons button, bool isPressed)
{
    public int Slot = slot;
    public PlayerButtons Button = button;
    public bool IsPressed = isPressed;
}