#region License
/*
MIT License

Copyright (c) 2020 Petteri Kautonen

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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace InstallerBaseWixSharp.Files.Localization.TabDeliLocalization
{
    /// <summary>
    /// A class for a simple localization using a text file embedded into a resource file.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public class TabDeliLocalization
    {
        /// <summary>
        /// A class to store the localization text.
        /// </summary>
        internal class LocalizationTextContainer
        {
            /// <summary>
            /// Gets or sets the name of the localized message.
            /// </summary>
            /// <value>The name of the localized message.</value>
            internal string MessageName { get; set; }

            /// <summary>
            /// Gets or sets the message.
            /// </summary>
            /// <value>The message.</value>
            internal string Message { get; set; }

            /// <summary>
            /// Gets or sets the name of the culture of the localized message.
            /// </summary>
            /// <value>The name of the culture.</value>
            internal string CultureName { get; set; }
        }


        /// <summary>
        /// A list containing messages for localization. Please do fill at least the en-US localization.
        /// </summary>
        List<LocalizationTextContainer> LocalizationTexts { get; } = 
            new List<LocalizationTextContainer>();

        /// <summary>
        /// Gets a localized message and gets a string corresponding to that message.
        /// </summary>
        /// <param name="messageName">The name of the message to get.</param>
        /// <param name="defaultMessage">The default value for the message if none were found in the <see cref="LocalizationTexts"/> with the locale of <paramref name="locale"/>.</param>
        /// <param name="locale">A locale expressed as a string.</param>
        /// <returns>A localized message with the given parameters.</returns>
        // ReSharper disable once UnusedMember.Global
        public string GetMessage(string messageName, string defaultMessage, string locale)
        {
            var value = LocalizationTexts.FirstOrDefault(f => f.CultureName == locale && f.MessageName == messageName);

            if (value != null && value.Message == null && locale.Split('-').Length == 2)
            {
                value = LocalizationTexts.FirstOrDefault(f => f.CultureName == locale.Split('-')[0] && f.MessageName == messageName);
            }
            else if (value != null && value.Message == null) // fall back to a generic culture..
            {
                value = LocalizationTexts.FirstOrDefault(f =>
                    f.CultureName.StartsWith(locale.Split('-')[0]) && f.MessageName == messageName);
            }

            return value?.Message ?? defaultMessage;
        }

        /// <summary>
        /// Gets a localized message and gets a string corresponding to that message with given arguments.
        /// </summary>
        /// <param name="messageName">The name of the message to get.</param>
        /// <param name="defaultMessage">The default value for the message if none were found in the <see cref="LocalizationTexts"/> with the locale of <paramref name="locale"/>.</param>
        /// <param name="locale">A locale expressed as a string.</param>
        /// <param name="args">An object array that contains zero or more objects to format the message.</param>
        /// <returns>A localized message with the given parameters.</returns>
        // ReSharper disable once UnusedMember.Global
        public string GetMessage(string messageName, string defaultMessage, string locale, params object[] args)
        {
            var value = LocalizationTexts.FirstOrDefault(f => f.CultureName == locale && f.MessageName == messageName);

            if (value != null && value.Message == null && locale.Split('-').Length == 2)
            {
                value = LocalizationTexts.FirstOrDefault(f => f.CultureName == locale.Split('-')[0] && f.MessageName == messageName);
            }
            else if (value != null && value.Message == null) // fall back to a generic culture..
            {
                value = LocalizationTexts.FirstOrDefault(f =>
                    f.CultureName.StartsWith(locale.Split('-')[0]) && f.MessageName == messageName);
            }

            string msg = value?.Message ?? defaultMessage;
            try
            {
                return string.Format(msg, args);
            }
            catch
            {
                return msg;
            }
        }

        /// <summary>
        /// Fills the <see cref="LocalizationTexts"/> array with a given file contents as a list of strings.
        /// </summary>
        /// <param name="fileContents"></param>
        // ReSharper disable once UnusedMember.Global
        public void GetLocalizedTexts(string fileContents)
        {
            List<string> fileLines = new List<string>();
            fileLines.AddRange(fileContents.Split(Environment.NewLine.ToArray()));

            string locale = string.Empty;

            foreach (var fileLine in fileLines)
            {
                if (fileLine.StartsWith("["))
                {
                    try
                    {
                        locale = fileLine.Trim('[', ']');
                        locale = new CultureInfo(locale).Name;
                    }
                    catch
                    {
                        locale = string.Empty;
                    }
                    continue;
                }

                if (locale == string.Empty)
                {
                    continue;
                }

                string[] delimited = fileLine.Split('\t');
                if (delimited.Length >= 2)
                {
                    if (LocalizationTexts.Exists(f => f.CultureName == locale && f.MessageName == delimited[0]))
                    {
                        continue;
                    }
                    LocalizationTexts.Add(new LocalizationTextContainer { MessageName = delimited[0], Message = delimited[1], CultureName = locale});
                }
            }
        }
    }
}

