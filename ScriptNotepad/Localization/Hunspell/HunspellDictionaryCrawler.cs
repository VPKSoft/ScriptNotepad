#region License
/*
MIT License

Copyright(c) 2019 Petteri Kautonen

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
using System.IO;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using ScriptNotepad.UtilityClasses.IO;

namespace ScriptNotepad.Localization.Hunspell
{
    /// <summary>
    /// A class for searching Hunspell dictionaries from a given folder.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    public class HunspellDictionaryCrawler: ErrorHandlingBase
    {
        /// <summary>
        /// Crawls the directory containing Hunspell dictionary and affix files.
        /// </summary>
        /// <param name="path">The path to search the dictionaries from.</param>
        /// <returns>List&lt;HunspellData&gt;.</returns>
        public static List<HunspellData> CrawlDirectory(string path)
        {
            // create a new instance of the DirectoryCrawler class by user given "arguments"..
            DirectoryCrawler crawler = new DirectoryCrawler(path, DirectoryCrawler.SearchTypeMatch.Regex,
                "*.dic", true);

            // search for the Hunspell dictionary files (*.dic)..
            var files = crawler.GetCrawlResult();

            // initialize a return value..
            List<HunspellData> result = new List<HunspellData>();

            // loop through the found dictionary files (*.dic)..
            foreach (var file in files)
            {
                try
                {
                    // create a new HunspellData class instance..
                    var data = HunspellData.FromDictionaryFile(file);

                    // validate that there is a affix (*.aff) pair for the found dictionary file (*.dic)..
                    if (!File.Exists(data.DictionaryFile) || !File.Exists(data.AffixFile))
                    {
                        // ..if not, do continue..
                        continue;
                    }

                    // the validation was successful, so add the data to the result..
                    result.Add(data);
                }
                catch (Exception ex)
                {
                    // log the exception..
                    ExceptionLogAction?.Invoke(ex);
                }
            }

            // return the result..
            return result;
        }
    }
}
