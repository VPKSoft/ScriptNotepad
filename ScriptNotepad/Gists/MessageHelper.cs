#region License
/*
MIT License

Copyright(c) 2020 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using System.Windows.Forms;

#pragma warning disable CS1587 
/// <summary>
/// A name space for the MessageHelper class.
/// </summary>
#pragma warning restore CS1587 // XML comment is not placed on a valid language element
namespace VPKSoft.MessageHelper
{
    /// <summary>
    /// A class to help with Message class in such cases as an overridden Control.WndProc Method.
    /// </summary>
    public static class MessageHelper
    {
        /// <summary>
        /// Posted to a window when the cursor moves.
        /// </summary>
        public const int WM_MOUSEMOVE = 0x0200; // Posted to a window when the cursor moves.

        /// <summary>
        /// Posted when the user releases the right mouse button while the cursor is in the client area of a window.
        /// </summary>
        public const int WM_RBUTTONUP = 0x0205; // Posted to a window when the right mouse button is up.

        /// <summary>
        /// Posted when the user releases the left mouse button while the cursor is in the client area of a window.
        /// </summary>
        public const int WM_LBUTTONUP = 0x0202; // Posted to a window when the left mouse button is up.

        /// <summary>
        /// Posted to a window when the cursor leaves the client area of the window specified in a prior call to TrackMouseEvent.
        /// </summary>
        public const int WM_MOUSELEAVE = 0x02A3; // Posted to a window when the cursor leaves the client area of the window specified in a prior call to TrackMouseEvent.

        /// <summary>
        /// Sent to the window that is losing the mouse capture.
        /// </summary>
        public const int WM_CAPTURECHANGED = 0x0215; // Sent to the window that is losing the mouse capture.

        /// <summary>
        /// The CTRL key is down.
        /// </summary>
        public const int MK_CONTROL = 0x0008; // The CTRL key is down.

        /// <summary>
        /// The left mouse button is down.
        /// </summary>
        public const int MK_LBUTTON = 0x0001; // The left mouse button is down.

        /// <summary>
        /// The middle mouse button is down.
        /// </summary>
        public const int MK_MBUTTON = 0x0010; // The middle mouse button is down.

        /// <summary>
        /// The right mouse button is down.
        /// </summary>
        public const int MK_RBUTTON = 0x0002; // The right mouse button is down.

        /// <summary>
        /// The SHIFT key is down.
        /// </summary>
        public const int MK_SHIFT = 0x0004; // The SHIFT key is down.

        /// <summary>
        /// The first X button is down.
        /// </summary>
        public const int MK_XBUTTON1 = 0x0020; // The first X button is down.

        /// <summary>
        /// // The second X button is down.
        /// </summary>
        public const int MK_XBUTTON2 = 0x0040; // The second X button is down.

        /// <summary>
        /// Sent to the focus window when the mouse wheel is rotated.
        /// </summary>
        public const int WM_MOUSEWHEEL = 0x020A; // Sent to the focus window when the mouse wheel is rotated.

        /// <summary>
        /// Gets the low order word of the lParam's value.
        /// </summary>
        /// <param name="message">A message of which low order word of the lParam's value to get.</param>
        /// <returns>The low order word of the lParam's value.</returns>
        public static int LParamLoWord(this Message message)
        {
            return BitConverter.ToInt16(BitConverter.GetBytes((long)message.LParam), 0);
        }

        /// <summary>
        /// Gets the high order word of the lParam's value.
        /// </summary>
        /// <param name="message">A message of which high order word of the lParam's value to get.</param>
        /// <returns>The high order word of the lParam's value.</returns>
        public static int LParamHiWord(this Message message)
        {
            return BitConverter.ToInt16(BitConverter.GetBytes((long)message.LParam), 2);
        }

        /// <summary>
        /// Gets the low order word of the wParam's value.
        /// </summary>
        /// <param name="message">A message of which low order word of the wParam's value to get.</param>
        /// <returns>The low order word of the wParam's value.</returns>
        public static int WParamLoWord(this Message message)
        {
            return BitConverter.ToInt16(BitConverter.GetBytes((long)message.WParam), 0);
        }

        /// <summary>
        /// Gets the high order word of the wParam's value.
        /// </summary>
        /// <param name="message">A message of which high order word of the wParam's value to get.</param>
        /// <returns>The high order word of the wParam's value.</returns>
        public static int WParamHiWord(this Message message)
        {
            return BitConverter.ToInt16(BitConverter.GetBytes((long)message.WParam), 2);
        }

        /// <summary>
        /// Gets the low order word of the lParam's value unsigned.
        /// </summary>
        /// <param name="message">A message of which low order word of the lParam's value to get unsigned.</param>
        /// <returns>The low order word of the lParam's value unsigned.</returns>
        public static uint LParamLoWordUnsigned(this Message message)
        {
            return BitConverter.ToUInt16(BitConverter.GetBytes((long)message.LParam), 0);
        }

        /// <summary>
        /// Gets the high order word of the lParam's value unsigned.
        /// </summary>
        /// <param name="message">A message of which high order word of the lParam's value to get unsigned.</param>
        /// <returns>The high order word of the lParam's value unsigned.</returns>
        public static uint LParamHiWordUnsigned(this Message message)
        {
            return BitConverter.ToUInt16(BitConverter.GetBytes((long)message.LParam), 2);
        }

        /// <summary>
        /// Gets the low order word of the wParam's value unsigned.
        /// </summary>
        /// <param name="message">A message of which low order word of the wParam's value to get unsigned.</param>
        /// <returns>The low order word of the wParam's value unsigned.</returns>
        public static uint WParamLoWordUnsigned(this Message message)
        {
            return BitConverter.ToUInt16(BitConverter.GetBytes((long)message.WParam), 0);
        }

        /// <summary>
        /// Gets the high order word of the wParam's value unsigned.
        /// </summary>
        /// <param name="message">A message of which high order word of the wParam's value to get unsigned.</param>
        /// <returns>The high order word of the wParam's value unsigned.</returns>
        public static uint WParamHiWordUnsigned(this Message message)
        {
            return BitConverter.ToUInt16(BitConverter.GetBytes((long)message.WParam), 2);
        }
    }
}
