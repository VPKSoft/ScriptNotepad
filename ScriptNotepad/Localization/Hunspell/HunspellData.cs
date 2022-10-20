#region License
/*
MIT License

Copyright(c) 2021 Petteri Kautonen

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

using System.Globalization;
using System.Text.RegularExpressions;

namespace ScriptNotepad.Localization.Hunspell;

/// <summary>
/// A class to hold Hunspell dictionary data.
/// </summary>
public class HunspellData
{
    /// <summary>
    /// Gets or sets the Hunspell culture matching the dictionary file name.
    /// </summary>
    public CultureInfo HunspellCulture { get; set; }

    /// <summary>
    /// Gets or sets the Hunspell dictionary (*.dic) file.
    /// </summary>
    public string DictionaryFile { get; set; }

    /// <summary>
    /// Gets or sets the hunspell affix (*.aff) file.
    /// </summary>
    public string AffixFile { get; set; }

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString()
    {
        // get string data of the CultureInfo instance..

        // a "valid" culture wasn't found so resort to other method to get the string..
        if (HunspellCulture.CultureTypes.HasFlag(CultureTypes.UserCustomCulture) || HunspellCulture.Name == string.Empty)
        {
            // do a regex math for the Hunspell dictionary file to see if it's file name contains a valid xx_YY or xx-YY culture name..
            var nameRegex = Regex.Match(Path.GetFileName(DictionaryFile), "\\D{2}(_|-)\\D{2}").Value.Replace('_', '-');

            // if the regex match is empty..
            if (nameRegex == string.Empty)
            {
                // ..get the file name without path and extension..
                nameRegex = Path.GetFileNameWithoutExtension(DictionaryFile);

                // if the file name's length is >1 make the first letter to upper case..
                if (nameRegex != null && nameRegex.Length >= 2)
                {
                    nameRegex = nameRegex[0].ToString().ToUpperInvariant() + nameRegex.Substring(1);
                }
                else
                {
                    // the file name's length is one characters, so make the whole file name to upper case..
                    nameRegex = nameRegex?.ToUpperInvariant();
                }
            }

            // if the ToString() method of a CultureInfo instance returns nothing..
            if (HunspellCulture.ToString() == string.Empty)
            {
                // ..return the name gotten using regex and file name..
                return nameRegex ?? string.Empty;
            }
                
            // otherwise return the CultureInfo instance ToString() with an addition of the name gotten from the file name..
            return HunspellCulture + $" ({nameRegex})";                    
        }

        // if a "valid" culture was found, just return it's display name..
        return HunspellCulture.DisplayName;
    }

    /// <summary>
    /// Creates a new instance of <see cref="HunspellCulture"/> from the given dictionary file name.
    /// </summary>
    /// <param name="fileName">The name of the dictionary file.</param>
    /// <returns>An instance to a <see cref="HunspellCulture"/> class.</returns>
    public static HunspellData FromDictionaryFile(string fileName)
    {
        HunspellData result = new HunspellData();

        // the files are excepted to be in format i.e. "en_US.dic"..
        string cultureName = Regex.Match(Path.GetFileName(fileName), "\\D{2}(_|-)\\D{2}").Value;
        cultureName = cultureName.Replace('_', '-');

        // get a CultureInfo value for the Hunspell dictionary file..
        result.HunspellCulture = CultureInfo.GetCultureInfo(cultureName);

        // set the file..
        result.DictionaryFile = fileName;

        // set the affix file..
        result.AffixFile = Path.ChangeExtension(fileName, "aff");

        return result;
    }
}