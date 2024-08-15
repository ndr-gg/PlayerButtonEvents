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