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

using ScriptNotepad.UtilityClasses.ErrorHandling;
using System;
using System.IO;
using System.Security;

namespace ScriptNotepad.IOPermission
{
    /// <summary>
    /// A class to check for file permissions; i.e. the file access requires elevation.
    /// </summary>
    public  class FileIOPermission: ErrorHandlingBase
    {
        /// <summary>
        /// Checks if the access to a given file with given permissions requires elevation.
        /// </summary>
        /// <param name="fileName">Name of the file which access permissions to check.</param>
        /// <param name="fileMode">The mode of how to try to open the file.</param>
        /// <param name="fileAccess">The file access mode of how to try to open the file.</param>
        /// <param name="fileShare">The file share mode of how to try to open the file.</param>
        /// <returns>A named tuple containing a value whether a elevation to access the file is required and a flag indicating whether the file is corrupted (I/O error).</returns>
        public static (bool ElevationRequied, bool FileCorrupted) FileRequiresElevation(string fileName, FileMode fileMode, FileAccess fileAccess, FileShare fileShare)
        {
            try
            {
                using (FileStream filestream = new FileStream(fileName, fileMode, fileAccess, fileShare))
                {
                    // nothing to  see here..
                }
                return (false, false);
            }
            // catch the exception and determine the result based on the type of the exception.
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);

                if (ex.GetType() == typeof(UnauthorizedAccessException) ||
                    ex.GetType() == typeof(SecurityException))
                {
                    return (true, false);
                }
                else if (ex.GetType() == typeof(IOException))
                {
                    return (false, true);
                }
                else
                {
                    return (true, false);
                }
            }
        }

        /// <summary>
        /// Checks if the access to a given file with given permissions requires elevation.
        /// <note type="note">The file share <see cref="FileShare"/> is set to ReadWrite.</note>
        /// </summary>
        /// <param name="fileName">Name of the file which access permissions to check.</param>
        /// <param name="fileMode">The mode of how to try to open the file.</param>
        /// <param name="fileAccess">The file access mode of how to try to open the file.</param>
        /// <returns>A named tuple containing a value whether a elevation to access the file is required and a flag indicating whether the file is corrupted (I/O error).</returns>
        public static (bool ElevationRequied, bool FileCorrupted) FileRequiresElevation(string fileName, FileMode fileMode, FileAccess fileAccess)
        {
            // call the "mother" method..
            return FileRequiresElevation(fileName, fileMode, fileAccess, FileShare.ReadWrite);
        }

        /// <summary>
        /// Checks if the access to a given file with given permissions requires elevation.
        /// <note type="note">The file access <see cref="FileAccess"/> is set to ReadWrite.</note>
        /// <note type="note">The file share <see cref="FileShare"/> is set to ReadWrite.</note>
        /// </summary>
        /// <param name="fileName">Name of the file which access permissions to check.</param>
        /// <param name="fileMode">The mode of how to try to open the file.</param>
        /// <returns>A named tuple containing a value whether a elevation to access the file is required and a flag indicating whether the file is corrupted (I/O error).</returns>
        public static (bool ElevationRequied, bool FileCorrupted) FileRequiresElevation(string fileName, FileMode fileMode)
        {
            // call the "mother" method..
            return FileRequiresElevation(fileName, fileMode, FileAccess.ReadWrite, FileShare.ReadWrite);
        }

        /// <summary>
        /// Checks if the access to a given file requires elevation.
        /// <note type="note">The file mode <see cref="FileMode"/> is set to Open.</note>
        /// <note type="note">The file access <see cref="FileAccess"/> is set to ReadWrite.</note>
        /// <note type="note">The file share <see cref="FileShare"/> is set to ReadWrite.</note>
        /// </summary>
        /// <param name="fileName">Name of the file which access permissions to check.</param>
        /// <returns>A named tuple containing a value whether a elevation to access the file is required and a flag indicating whether the file is corrupted (I/O error).</returns>
        public static (bool ElevationRequied, bool FileCorrupted) FileRequiresElevation(string fileName)
        {
            // call the "mother" method..
            return FileRequiresElevation(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
        }
    }
}
