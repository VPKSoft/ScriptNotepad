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

using System;

namespace InstallerBaseWixSharp.Files.Localization
{
    /// <summary>
    /// An attribute for the <see cref="LocalizationDataAttribute"/> enumeration.
    /// Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class LocalizationDataAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationDataAttribute"/> class.
        /// </summary>
        /// <param name="description">The language description.</param>
        /// <param name="lcid">The Language Locale Identifier (LCID).</param>
        /// <param name="code">The culture name in the format languagecode2-country/regioncode2.</param>
        /// <param name="runningIdentifier">A running identifier for the attribute.</param>
        public LocalizationDataAttribute(string description, int lcid, string code, int runningIdentifier)
        {
            Description = description;
            LCID = lcid;
            Code = code;
            RunningIdentifier = runningIdentifier;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationDataAttribute"/> class.
        /// </summary>
        /// <param name="description">The language description.</param>
        /// <param name="lcid">The Language Locale Identifier (LCID).</param>
        /// <param name="code">The culture name in the format languagecode2-country/regioncode2.</param>
        /// <param name="localized">if set to <c>true</c> the language is localizes within this installer instance.</param>
        /// <param name="runningIdentifier">A running identifier for the attribute.</param>
        public LocalizationDataAttribute(string description, int lcid, string code, int runningIdentifier,
            bool localized) : this(description,
            lcid, code, runningIdentifier)
        {
            Localized = localized;
        }

        /// <summary>
        /// Gets or sets the language description.
        /// </summary>
        /// <value>The language description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the culture name in the format languagecode2-country/regioncode2.
        /// </summary>
        /// <value>The culture name in the format languagecode2-country/regioncode2.</value>
        public string Code { get; set; }

        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// Gets or sets the Language Locale Identifier (LCID).
        /// </summary>
        /// <value>The Language Locale Identifier (LCID).</value>
        public int LCID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LocalizationDataAttribute"/> is localized within this installer instance.
        /// </summary>
        /// <value><c>true</c> if this <see cref="LocalizationDataAttribute"/> is localized within this installer instance; otherwise, <c>false</c>.</value>
        public bool Localized { get; set; }

        /// <summary>
        /// Gets or sets the running identifier for the attribute.
        /// </summary>
        /// <value>The running identifier for the attribute.</value>
        public int RunningIdentifier { get; set; }
    }
}















