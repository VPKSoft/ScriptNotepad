#region license
/*
This file is public domain.
You may freely do anything with it.
Copyright (c) VPKSoft 2019
*/
#endregion

using System.Collections.Generic;
using System.Windows.Forms;

namespace ScriptNotepad.UtilityClasses.Keyboard;

/// <summary>
/// A class for sending key presses to controls.
/// </summary>
public static class KeySendList
{
    private static readonly List<KeyValuePair<Keys, string>> KeyList = new List<KeyValuePair<Keys, string>>(new []
    {
        new KeyValuePair<Keys, string>(Keys.Back, "{BACKSPACE}"),
        new KeyValuePair<Keys, string>(Keys.Pause, "{BREAK}"),
        // ReSharper disable once StringLiteralTypo
        new KeyValuePair<Keys, string>(Keys.CapsLock, "{CAPSLOCK}"),
        new KeyValuePair<Keys, string>(Keys.Delete, "{DELETE}"),
        new KeyValuePair<Keys, string>(Keys.Down, "{DOWN}"),
        new KeyValuePair<Keys, string>(Keys.End, "{END}"),
        new KeyValuePair<Keys, string>(Keys.Return, "{ENTER}"),
        new KeyValuePair<Keys, string>(Keys.Escape, "{ESC}"),
        new KeyValuePair<Keys, string>(Keys.Help, "{HELP}"),
        new KeyValuePair<Keys, string>(Keys.Home, "{HOME}"),
        new KeyValuePair<Keys, string>(Keys.Insert, "{INSERT}"),
        new KeyValuePair<Keys, string>(Keys.Left, "{LEFT}"),
        // ReSharper disable once StringLiteralTypo
        new KeyValuePair<Keys, string>(Keys.NumLock, "{NUMLOCK}"),
        // ReSharper disable once StringLiteralTypo
        new KeyValuePair<Keys, string>(Keys.PageDown, "{PGDN}"),
        // ReSharper disable once StringLiteralTypo
        new KeyValuePair<Keys, string>(Keys.PageUp, "{PGUP}"),
        // ReSharper disable once StringLiteralTypo
        new KeyValuePair<Keys, string>(Keys.PrintScreen, "{PRTSC}"),
        new KeyValuePair<Keys, string>(Keys.Right, "{RIGHT}"),
        // ReSharper disable once StringLiteralTypo
        new KeyValuePair<Keys, string>(Keys.Scroll, "{SCROLLLOCK}"),
        new KeyValuePair<Keys, string>(Keys.Tab, "{TAB}"),
        new KeyValuePair<Keys, string>(Keys.Up, "{UP}"),
        new KeyValuePair<Keys, string>(Keys.F1, "{F1}"),
        new KeyValuePair<Keys, string>(Keys.F2, "{F2}"),
        new KeyValuePair<Keys, string>(Keys.F3, "{F3}"),
        new KeyValuePair<Keys, string>(Keys.F4, "{F4}"),
        new KeyValuePair<Keys, string>(Keys.F5, "{F5}"),
        new KeyValuePair<Keys, string>(Keys.F6, "{F6}"),
        new KeyValuePair<Keys, string>(Keys.F7, "{F7}"),
        new KeyValuePair<Keys, string>(Keys.F8, "{F8}"),
        new KeyValuePair<Keys, string>(Keys.F9, "{F9}"),
        new KeyValuePair<Keys, string>(Keys.F10, "{F10}"),
        new KeyValuePair<Keys, string>(Keys.F11, "{F11}"),
        new KeyValuePair<Keys, string>(Keys.F12, "{F12}"),
        new KeyValuePair<Keys, string>(Keys.F13, "{F13}"),
        new KeyValuePair<Keys, string>(Keys.F14, "{F14}"),
        new KeyValuePair<Keys, string>(Keys.F15, "{F15}"),
        new KeyValuePair<Keys, string>(Keys.F16, "{F16}"),
        new KeyValuePair<Keys, string>(Keys.Add, "{ADD}"),
        new KeyValuePair<Keys, string>(Keys.Subtract, "{SUBTRACT}"),
        new KeyValuePair<Keys, string>(Keys.Multiply, "{MULTIPLY}"),
        new KeyValuePair<Keys, string>(Keys.Divide, "{DIVIDE}"),
        new KeyValuePair<Keys, string>(Keys.OemMinus, "-"), // Added: 19.10.19, VPKSoft..
        new KeyValuePair<Keys, string>(Keys.OemPeriod, "."), // Added: 19.10.19, VPKSoft..
    });

    /// <summary>
    /// Determines whether the specified key is supported by the class.
    /// </summary>
    /// <param name="key">The key to check for.</param>
    /// <returns><c>true</c> if the specified key is supported by the class; otherwise, <c>false</c>.</returns>
    public static bool HasKey(Keys key)
    {
        return GetKeyString(key) != null;
    }

    /// <summary>
    /// Determines whether the specified key is supported by the class.
    /// </summary>
    /// <param name="key">The key to check for.</param>
    /// <returns><c>true</c> if the specified key is supported by the class; otherwise, <c>false</c>.</returns>
    public static bool HasKey(string key)
    {
        return GetKeyKeys(key) != null;
    }

    /// <summary>
    /// Gets a string matching the given <see cref="Keys"/>.
    /// </summary>
    /// <param name="key">The key for which to get the corresponding string for.</param>
    /// <returns>A string matching the <paramref name="key"/> if found; otherwise null.</returns>
    public static string GetKeyString(Keys key)
    {
        foreach (var k in KeyList)
        {
            if (k.Key == key)
            {
                return k.Value;
            }
        }
        return null;
    }

    /// <summary>
    /// Gets a <see cref="Keys"/> enumeration value matching the given string value.
    /// </summary>
    /// <param name="key">The key as a string value.</param>
    /// <returns>A <see cref="Keys"/> enumeration value if found; otherwise null.</returns>
    public static Keys? GetKeyKeys(string key)
    {
        foreach (var k in KeyList)
        {
            if (k.Value == key)
            {
                return k.Key;
            }
        }
        return null;
    }
}