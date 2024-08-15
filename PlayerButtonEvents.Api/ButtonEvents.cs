namespace PlayerButtonEvents.Api;

public class ButtonEvents
{
    public event EventHandler<ButtonEventArgs>? ButtonPressed;
    public event EventHandler<ButtonEventArgs>? ButtonReleased;
    public event EventHandler<ButtonEventArgs>? ButtonStateChanged;


    internal void OnStateChanged(object? sender, ButtonEventArgs args)
    {
        ButtonStateChanged?.Invoke(sender, args);
        if (args.IsPressed) ButtonPressed?.Invoke(sender, args);
        else ButtonReleased?.Invoke(sender, args);
    }
}