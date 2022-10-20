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

using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.UtilityClasses.Clipboard;

/// <summary>
/// A simple class to help to set clipboard text contents.
/// </summary>
public class ClipboardTextHelper: ErrorHandlingBase
{
    /// <summary>
    /// Set the Clipboard's contents to a given text.
    /// </summary>
    /// <param name="text">The text to set the clipboard's contents to.</param>
    /// <returns>True if the operation was successful; otherwise false.</returns>
    public static bool ClipboardSetText(string text)
    {
        try
        {
            System.Windows.Forms.Clipboard.SetDataObject(text, true, 20, 150);
            return true;
        }
        catch (Exception ex)
        {
            // clipboard operation may fail..
            ExceptionLogAction?.Invoke(ex);
            return false;
        }
    }
}