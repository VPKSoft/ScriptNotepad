#region License
/*
MIT License

Copyright(c) 2022 Petteri Kautonen

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

using VPKSoft.LangLib;

namespace ScriptNotepad.Localization;

/// <summary>
/// A class to provide static localized messages from the <see cref="DBLangEngine"/>.
/// Implements the <see cref="ScriptNotepad.Localization.IStaticMessageProvider" />
/// </summary>
/// <seealso cref="ScriptNotepad.Localization.IStaticMessageProvider" />
public class StaticMessageLocalizationProvider: IStaticMessageProvider
{
    /// <summary>
    /// Prevents a default instance of the <see cref="StaticMessageLocalizationProvider"/> class from being created.
    /// </summary>
    private StaticMessageLocalizationProvider()
    {
        Instance = this;
    }

    /// <summary>
    /// Gets or sets the of this class.
    /// </summary>
    /// <value>The instance.</value>
    public static StaticMessageLocalizationProvider Instance { get; set; } = new();

    /// <summary>
    /// Gets the message by the specified name localized to the current software culture setting.
    /// </summary>
    /// <param name="messageName">Name of the message.</param>
    /// <param name="defaultMessage">The default message to fall back into if a localized message was not found.</param>
    /// <returns>A localized value for the specified message name.</returns>
    public string GetMessage(string messageName, string defaultMessage)
    {
        return DBLangEngine.GetStatMessage(messageName, defaultMessage);
    }
}