using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ScriptNotepad.IOPermission
{
    /// <summary>
    /// A class to check for file permissions; i.e. the file access requires elevation.
    /// </summary>
    public static class FileIOPermission
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

        /// <summary>
        /// Gets or sets the action to be used to log an exception.
        /// </summary>
        public static Action<Exception> ExceptionLogAction { get; set; } = null;
    }
}
