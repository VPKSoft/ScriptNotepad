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

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ScriptNotepad.UtilityClasses.ErrorHandling;

// ReSharper disable PossibleMultipleEnumeration

namespace ScriptNotepad.UtilityClasses.IO
{
    /// <summary>
    /// A class to crawl through directories to search either files or directories.
    /// </summary>
    public class DirectoryCrawler: ErrorHandlingBase
    {
        /// <summary>
        /// The type of the search for the <see cref="DirectoryCrawler"/> class.
        /// </summary>
        public enum SearchTypeMatch
        {
            /// <summary>
            /// Files matching a given mask are searched.
            /// </summary>
            FileMask,

            /// <summary>
            /// Files matching a given regular expression are searched.
            /// </summary>
            Regex,

            /// <summary>
            /// All files a searched with the mask of '*.*'.
            /// </summary>
            AllFiles,

            /// <summary>
            /// Files are searched with the mask of '*.'.
            /// </summary>
            FilesNoExtension,

            /// <summary>
            /// Only directories are searched.
            /// </summary>
            Directories,
        }

        /// <summary>
        /// A value indicating if the current search process <see cref="GetCrawlResult"/> should be canceled.
        /// </summary>
        public volatile bool Canceled = false;

        /// <summary>
        /// Gets or sets the type of the search.
        /// </summary>
        private SearchTypeMatch SearchType { get; set; }

        /// <summary>
        /// Gets or sets the search mask.
        /// </summary>
        private string SearchMask { get; set; }

        /// <summary>
        /// Gets or sets the compiled regular expression in case the search type is <see cref="SearchTypeMatch.Regex"/>.
        /// </summary>
        private Regex FileExtensionMatch { get; set; }

        /// <summary>
        /// Gets or sets the starting path of the search.
        /// </summary>
        private string Path { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DirectoryCrawler"/> uses recursion; i.e. searches from the subdirectories as well.
        /// </summary>
        private bool Recursion { get; set; }

        /// <summary>
        /// Creates a compiled regular expression from a given list of file extensions.
        /// </summary>
        /// <param name="extensions">The file extensions to be used with the search.</param>
        /// <returns>An instance to a <see cref="Regex"/> class.</returns>
        private static Regex FromExtensions(params string[] extensions)
        {
            // remove the useless '*.' and '.' character combinations from the given parameters..
            for (int i = 0; i < extensions.Length; i++)
            {
                extensions[i] = 
                    extensions[i].
                        Replace("*.", "").
                        Replace(".", "");

                // the '*' character requires special handling; otherwise an exception would occur..
                if (extensions[i] == "*")
                {
                    extensions[i] = "." + extensions[i];
                }
            }
            
            // create a compiled regular expression of the given parameters
            // and return it.. hint: '\.(cs|txt|xml)$'
            try
            {
                return new Regex(@"\.(" + string.Join("|", extensions) + ")$",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);

                // return a regexp matching all file endings..
                return new Regex(@"\\.*", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
        }

        /// <summary>
        /// Checks if a compiled regular expression from a given list of file extensions is valid.
        /// </summary>
        /// <param name="extensions">The file extensions to be used with the check.</param>
        /// <returns>True if the created regular expression is valid; otherwise false.</returns>
        public static bool ValidateExtensionRegexp(params string[] extensions)
        {
            // remove the useless '*.' and '.' character combinations from the given parameters..
            for (int i = 0; i < extensions.Length; i++)
            {
                extensions[i] = 
                    extensions[i].
                        Replace("*.", "").
                        Replace(".", "");

                // the '*' character requires special handling; otherwise an exception would occur..
                if (extensions[i] == "*")
                {
                    extensions[i] = "." + extensions[i];
                }
            }
            
            // create a compiled regular expression of the given parameters
            // and return it.. hint: '\.(cs|txt|xml)$'
            try
            {
                // ReSharper disable once ObjectCreationAsStatement
                new Regex(@"\.(" + string.Join("|", extensions) + ")$",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled);
                return true;
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);
                return false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryCrawler"/> class.
        /// </summary>
        /// <param name="path">The path to start the search from.</param>
        /// <param name="searchType">Type of the search.</param>
        /// <param name="searchMask">The search mask.</param>
        /// <param name="subDirectories">if set to <c>true</c> the subdirectories are searched as well.</param>
        public DirectoryCrawler(string path, SearchTypeMatch searchType, string searchMask, bool subDirectories)
        {
            // set the constructor values..
            Path = path;
            SearchType = searchType;
            SearchMask = searchMask;
            Recursion = subDirectories;
            // END: set the constructor values..

            // based on the give search type, manipulate the search mask accordingly..
            switch (SearchType)
            {
                case SearchTypeMatch.Regex:
                    FileExtensionMatch = FromExtensions(searchMask.Split(';'));
                    break;

                case SearchTypeMatch.AllFiles:
                    SearchMask = "*"; // this matches all files with or without an extension..
                    break;

                case SearchTypeMatch.FilesNoExtension:
                    SearchMask = "*."; // this matches files without an extension..
                    break;

                case SearchTypeMatch.Directories:
                    SearchMask = string.Empty; // no filtering for directories..
                    break;

                case SearchTypeMatch.FileMask:
                    SearchMask = searchMask; // the search mask can be the user given one..
                    break;
            }
        }

        /// <summary>
        /// Gets the crawl result; A list of files or directories based on the class initialization.
        /// </summary>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public IEnumerable<string> GetCrawlResult()
        {
            if (SearchType == SearchTypeMatch.Directories)
            {
                return GetDirectories(Path, Recursion);
            }

            return GetFiles(Path, Recursion);
        }

        /// <summary>
        /// A delegate for the <see cref="DirectoryCrawler.ReportProgress"/> event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="DirectoryCrawlerEventArgs"/> instance containing the event data.</param>
        public delegate void OnReportProgress(object sender, DirectoryCrawlerEventArgs e);

        /// <summary>
        /// Occurs when the <see cref="GetCrawlResult"/> is reporting progress.
        /// </summary>
        public event OnReportProgress ReportProgress;

        /// <summary>
        /// Gets the directories based on the class initialization.
        /// </summary>
        /// <param name="path">The path to start enumerating the directories from.</param>
        /// <param name="subDirectories">if set to <c>true</c> the subdirectories are also included in the result.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        private IEnumerable<string> GetDirectories(string path, bool subDirectories)
        {
            List<string> result = new List<string>();
            // a SecurityException or a UnauthorizedAccessException may intrude the enumeration
            // process any time and it will also result in exception if the directories where
            // enumerated solely depending on the Directory.EnumerateDirectories(path, ...,SearchOption.AllDirectories)..
            try 
            {
                List<string> directories = new List<string>();

                foreach (var directory in Directory.EnumerateDirectories(path))
                {
                    // the process was requested to be canceled..
                    if (Canceled)
                    {
                        return result;
                    }

                    // report the progress if subscribed..
                    ReportProgress?.Invoke(this,
                        new DirectoryCrawlerEventArgs
                            {CurrentDirectory = path, FileName = string.Empty, Directory = directory});
                    result.Add(directory);
                    directories.Add(directory);
                }

                if (subDirectories)
                {
                    // a SecurityException or a UnauthorizedAccessException may intrude the enumeration
                    // process any time and it will also result in exception if the directories where
                    // enumerated solely depending on the Directory.EnumerateDirectories(path, ...,SearchOption.AllDirectories)..
                    try
                    {
                        foreach (var directory in directories)
                        {
                            // the process was requested to be canceled..
                            if (Canceled)
                            {
                                return result;
                            }

                            result.AddRange(GetDirectories(directory, true));
                        }
                    }
                    catch (Exception ex)
                    {
                        // log the exception..
                        ExceptionLogAction?.Invoke(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);
            }

            return result;
        }

        /// <summary>
        /// Gets the files based on the class initialization.
        /// </summary>
        /// <param name="path">The path to start enumerating the files from.</param>
        /// <param name="subDirectories">if set to <c>true</c> the files in subdirectories are also included in the result.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        private IEnumerable<string> GetFiles(string path, bool subDirectories)
        {
            List<string> result = new List<string>();

            // a SecurityException or a UnauthorizedAccessException may intrude the enumeration
            // process any time and it will also result in exception if the files where enumerated
            // solely depending on the Directory.EnumerateFiles(path, ...,SearchOption.AllDirectories)..
            try
            {
                // based on the search type append to the result..
                if (SearchType == SearchTypeMatch.Regex)
                {
                    foreach (var file in Directory.EnumerateFiles(path).Where(f => FileExtensionMatch.IsMatch(f)))
                    {
                        // the process was requested to be canceled..
                        if (Canceled)
                        {
                            return result;
                        }

                        // report the progress if subscribed..
                        ReportProgress?.Invoke(this,
                            new DirectoryCrawlerEventArgs
                                {CurrentDirectory = path, FileName = file, Directory = string.Empty});

                        // add to the result..
                        result.Add(file);
                    }                    
                }
                else if (SearchType == SearchTypeMatch.AllFiles || SearchType == SearchTypeMatch.FileMask ||
                         SearchType == SearchTypeMatch.FilesNoExtension)
                {
                    foreach (var file in  Directory.EnumerateFiles(path, SearchMask))
                    {
                        // the process was requested to be canceled..
                        if (Canceled)
                        {
                            return result;
                        }

                        // report the progress if subscribed..
                        ReportProgress?.Invoke(this,
                            new DirectoryCrawlerEventArgs
                                {CurrentDirectory = path, FileName = file, Directory = string.Empty});

                        // add to the result..
                        result.Add(file);
                    }                    
                }

                if (subDirectories)
                {
                    // a SecurityException or a UnauthorizedAccessException may intrude the enumeration
                    // process any time and it will also result in exception if the directories where
                    // enumerated solely depending on the Directory.EnumerateDirectories(path, ...,SearchOption.AllDirectories)..
                    try
                    {
                        foreach (var directory in  Directory.EnumerateDirectories(path))
                        {
                            // the process was requested to be canceled..
                            if (Canceled)
                            {
                                return result;
                            }

                            // report the progress if subscribed..
                            ReportProgress?.Invoke(this,
                                new DirectoryCrawlerEventArgs
                                    {CurrentDirectory = path, FileName = string.Empty, Directory = directory});

                            // add to the result..
                            result.AddRange(GetFiles(directory, true));
                        }
                    }
                    catch (Exception ex)
                    {
                        // log the exception..
                        ExceptionLogAction?.Invoke(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);
            }

            return result;
        }
    }
}
