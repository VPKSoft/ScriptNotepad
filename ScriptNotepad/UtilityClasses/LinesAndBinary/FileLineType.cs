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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptNotepad.UtilityClasses.LinesAndBinary
{
    /// <summary>
    /// An enumeration describing common and less common new line character sequences of a text file.
    /// (C): https://en.wikipedia.org/wiki/Newline
    /// </summary>
    [Flags]
    public enum FileLineTypes
    {
        /// <summary>
        /// Atari TOS, Microsoft Windows, DOS (MS-DOS, PC DOS, etc.), DEC TOPS-10, RT-11, CP/M, MP/M, OS/2, Symbian OS, Palm OS, Amstrad CPC, and most other early non-Unix and non-IBM operating systems.
        /// </summary>
        CRLF = 1, // \r\n, 0x0D/U0x000D + 0x0A/U0x000A, 13|10

        /// <summary>
        /// Multics, Unix and Unix-like systems (Linux, macOS, FreeBSD, AIX, Xenix, etc.), BeOS, Amiga, RISC OS, and others.
        /// </summary>
        LF = 2, // \n, 0x0A/U0x000A, 10

        /// <summary>
        /// Commodore 8-bit machines (C64, C128), Acorn BBC, ZX Spectrum, TRS-80, Apple II family, Oberon, the classic Mac OS, MIT Lisp Machine and OS-9.
        /// </summary>
        CR = 4, // \r, 0x0D/U0x000D, 13

        /// <summary>
        /// QNX pre-POSIX implementation (version &lt; 4).
        /// </summary>
        RS = 8, // 0x1E, 30

        /// <summary>
        /// Acorn BBC and RISC OS spooled text output.
        /// </summary>
        LFCR = 16, // \n\r, 0x0A/U0x000A + 0x0D/U0x000D, 10|13

        /// <summary>
        /// IBM mainframe systems, including z/OS (OS/390) and i5/OS (OS/400).
        /// </summary>
        NL = 32, // \025, 0x15, 21

        /// <summary>
        /// Atari 8-bit machines.
        /// </summary>
        ATASCII = 64, // \9B, 155,

        /// <summary>
        /// ZX80 and ZX81 (Home computers from Sinclair Research Ltd).
        /// </summary>
        NEWLINE = 128, // 0x76, 118

        /// <summary>
        /// An unknown line ending or nothing.
        /// </summary>
        Unknown = 256,

        /// <summary>
        /// A value indicating that mixed line endings occur in a file.
        /// </summary>
        Mixed = 512,

        /// <summary>
        /// Atari TOS, Microsoft Windows, DOS (MS-DOS, PC DOS, etc.), DEC TOPS-10, RT-11, CP/M, MP/M, OS/2, Symbian OS, Palm OS, Amstrad CPC, and most other early non-Unix and non-IBM operating systems (Unicode).
        /// </summary>
        UCRLF = 1024, // \r\n, 0x0D/U0x000D + 0x0A/U0x000A, 13|10

        /// <summary>
        /// Multics, Unix and Unix-like systems (Linux, macOS, FreeBSD, AIX, Xenix, etc.), BeOS, Amiga, RISC OS, and others (Unicode).
        /// </summary>
        ULF = 2048, // \n, 0x0A/U0x000A, 10

        /// <summary>
        /// Commodore 8-bit machines (C64, C128), Acorn BBC, ZX Spectrum, TRS-80, Apple II family, Oberon, the classic Mac OS, MIT Lisp Machine and OS-9 (Unicode).
        /// </summary>
        UCR = 4096, // \r, 0x0D/U0x000D, 13

        /// <summary>
        /// Acorn BBC and RISC OS spooled text output (Unicode).
        /// </summary>
        ULFCR = 8192, // \n\r, 0x0A/U0x000A + 0x0D/U0x000D, 10|13
    }

    /// <summary>
    /// A class for detecting a new line type of a text file.
    /// </summary>
    public static class FileLineType
    {
        #region LineEndingLocalizeableDescriptions
        /// <summary>
        /// A short description for the line ending for: Atari TOS, Microsoft Windows, DOS (MS-DOS, PC DOS, etc.), DEC TOPS-10, RT-11, CP/M, MP/M, OS/2, Symbian OS, Palm OS, Amstrad CPC, and most other early non-Unix and non-IBM operating systems.
        /// </summary>
        public static string CRLF_Description { get; set; } = "CR+LF";

        /// <summary>
        /// A short description for the line ending for: Multics, Unix and Unix-like systems (Linux, macOS, FreeBSD, AIX, Xenix, etc.), BeOS, Amiga, RISC OS, and others.
        /// </summary>
        public static string LF_Description { get; set; } = "LF";

        /// <summary>
        /// A short description for the line ending for: Commodore 8-bit machines (C64, C128), Acorn BBC, ZX Spectrum, TRS-80, Apple II family, Oberon, the classic Mac OS, MIT Lisp Machine and OS-9.
        /// </summary>
        public static string CR_Description { get; set; } = "CR";

        /// <summary>
        /// A short description for the line ending for: QNX pre-POSIX implementation (version &lt; 4).
        /// </summary>
        public static string RS_Description { get; set; } = "RS";

        /// <summary>
        /// A short description for the line ending for: Acorn BBC and RISC OS spooled text output.
        /// </summary>
        public static string LFCR_Description { get; set; } = "LF+CR";

        /// <summary>
        /// A short description for the line ending for: IBM mainframe systems, including z/OS (OS/390) and i5/OS (OS/400).
        /// </summary>
        public static string NL_Description { get; set; } = "NL";

        /// <summary>
        /// A short description for the line ending for: Atari 8-bit machines.
        /// </summary>
        public static string ATASCII_Description { get; set; } = "ATASCII";

        /// <summary>
        /// A short description for the line ending for: ZX80 and ZX81 (Home computers from Sinclair Research Ltd).
        /// </summary>
        public static string NEWLINE_Description { get; set; } = "NEWLINE";

        /// <summary>
        /// A short description for the line ending for: An unknown line ending or nothing.
        /// </summary>
        public static string Unknown_Description { get; set; } = "Unknown";

        /// <summary>
        /// A short description for mixed line endings. 
        /// </summary>
        public static string Mixed_Description { get; set; } = "Mixed";

        /// <summary>
        /// A short description for the line ending for: Atari TOS, Microsoft Windows, DOS (MS-DOS, PC DOS, etc.), DEC TOPS-10, RT-11, CP/M, MP/M, OS/2, Symbian OS, Palm OS, Amstrad CPC, and most other early non-Unix and non-IBM operating systems (Unicode).
        /// </summary>
        public static string UCRLF_Description { get; set; } = "Unicode CR+LF";

        /// <summary>
        /// A short description for the line ending for: Multics, Unix and Unix-like systems (Linux, macOS, FreeBSD, AIX, Xenix, etc.), BeOS, Amiga, RISC OS, and others (Unicode).
        /// </summary>
        public static string ULF_Description { get; set; } = "Unicode LF";

        /// <summary>
        /// A short description for the line ending for: Commodore 8-bit machines (C64, C128), Acorn BBC, ZX Spectrum, TRS-80, Apple II family, Oberon, the classic Mac OS, MIT Lisp Machine and OS-9 (Unicode).
        /// </summary>
        public static string UCR_Description { get; set; } = "Unicode CR";

        /// <summary>
        /// A short description for the line ending for: Acorn BBC and RISC OS spooled text output (Unicode).
        /// </summary>
        public static string ULFCR_Description { get; set; } = "Unicode LF+CR";
        #endregion

        #region FileLineEndingsAsString
        /// <summary>
        /// Gets the file line ending as a string representation.
        /// </summary>
        /// <param name="fileLineTypes">The <see cref="FileLineTypes"/> enumeration value.</param>
        /// <returns>A string containing the line ending characters.</returns>
        public static string GetFileLineEndingString(FileLineTypes fileLineTypes)
        {
            // a simple switch..case structure..
            switch(fileLineTypes)
            {
                case FileLineTypes.CRLF:
                    return "\r\n";

                case FileLineTypes.LF:
                    return "\n";

                case FileLineTypes.CR:
                    return "\r";

                case FileLineTypes.RS:
                    return ((char) 0x1E).ToString();

                case FileLineTypes.LFCR:
                    return "\n\r";

                case FileLineTypes.NL:
                    return ((char) 0x15).ToString();

                case FileLineTypes.ATASCII:
                    return ((char) 155).ToString();

                case FileLineTypes.NEWLINE:
                    return ((char) 0x76).ToString();

                case FileLineTypes.Unknown:
                    return Environment.NewLine;

                case FileLineTypes.Mixed:
                    return Environment.NewLine;

                case FileLineTypes.UCRLF:
                    return "\r\n";

                case FileLineTypes.ULF:
                    return "\n";

                case FileLineTypes.UCR:
                    return "\r";

                case FileLineTypes.ULFCR:
                    return "\n\r";

                default: return Environment.NewLine;
            }
        }
        #endregion

        #region EnumerationDescriptions
        /// <summary>
        /// Sets a description for a given <see cref="FileLineTypes"/> enumeration. This method is for localization purposes.
        /// </summary>
        /// <param name="fileLineTypes">A <see cref="FileLineTypes"/> value to set a description for.</param>
        /// <param name="description">The description to set for a <paramref name="fileLineTypes"/> value.</param>
        /// <returns>True if the description was successfully set; otherwise false.</returns>
        public static bool SetLineEndingDescriptionByEnumeration(FileLineTypes fileLineTypes, string description)
        {
            // a simple switch..case structure..
            switch(fileLineTypes)
            {
                case FileLineTypes.CRLF:
                    CRLF_Description = description; return true;

                case FileLineTypes.LF:
                    LF_Description = description; return true;

                case FileLineTypes.CR:
                    CR_Description = description; return true;

                case FileLineTypes.RS:
                    RS_Description = description; return true;

                case FileLineTypes.LFCR:
                    LFCR_Description = description; return true;

                case FileLineTypes.NL:
                    NL_Description = description; return true;

                case FileLineTypes.ATASCII:
                    ATASCII_Description = description; return true;

                case FileLineTypes.NEWLINE:
                    NEWLINE_Description = description; return true;

                case FileLineTypes.Unknown:
                    Unknown_Description = description; return true;

                case FileLineTypes.Mixed:
                    Mixed_Description = description; return true;

                case FileLineTypes.UCRLF:
                    UCRLF_Description = description; return true;

                case FileLineTypes.ULF:
                    ULF_Description = description; return true;

                case FileLineTypes.UCR:
                    UCR_Description = description; return true;

                case FileLineTypes.ULFCR:
                    ULFCR_Description = description; return true;

                default: return false;
            }
        }

        /// <summary>
        /// Gets the description of the FileLineTypes enumeration value using either custom description or the name of the enumeration value.
        /// </summary>
        /// <param name="fileLineTypes">A FileLineTypes enumeration value to get the description for.</param>
        /// <param name="useEnumName">A flag indicating if name of the enumeration value should be returned.</param>
        /// <returns>A description for the given <paramref name="fileLineTypes"/> enumeration value.</returns>
        public static string GetLineEndingDescriptionByEnumeration(FileLineTypes fileLineTypes, bool useEnumName)
        {
            // the name of the enumeration was requested..
            if (useEnumName)
            {
                // ..so do return it..
                return Enum.GetName(typeof(FileLineTypes), fileLineTypes);
            }

            // a simple switch..case structure..
            switch (fileLineTypes)
            {
                case FileLineTypes.CRLF:
                    return CRLF_Description;

                case FileLineTypes.LF:
                    return LF_Description;

                case FileLineTypes.CR:
                    return CR_Description;

                case FileLineTypes.RS:
                    return RS_Description;

                case FileLineTypes.LFCR:
                    return LFCR_Description;

                case FileLineTypes.NL:
                    return NL_Description;

                case FileLineTypes.ATASCII:
                    return ATASCII_Description;

                case FileLineTypes.NEWLINE:
                    return NEWLINE_Description;

                case FileLineTypes.Unknown:
                    return Unknown_Description;

                case FileLineTypes.Mixed:
                    return Mixed_Description;

                case FileLineTypes.UCRLF:
                    return UCRLF_Description;

                case FileLineTypes.ULF:
                    return ULF_Description;

                case FileLineTypes.UCR:
                    return UCR_Description;

                case FileLineTypes.ULFCR:
                    return ULFCR_Description;

                default: return Unknown_Description;
            }
        }
        #endregion

        /// <summary>
        /// A flag indicating whether the GetFileLineTypes method should return a name of the <see cref="FileLineTypes"/> enumeration value or a custom description defined by a property.
        /// </summary>
        public static bool UseEnumNames { get; set; } = false;

        /// <summary>
        /// Gets the file line types of a given memory stream.
        /// </summary>
        /// <param name="stream">The memory stream to be used to check for the line endings.</param>
        /// <param name="useLessCommon">An flag indicating if the less common line endings should also be checked.</param>
        /// <returns>A collection of line ending key value pairs with their enumeration values and their names.</returns>
        public static IEnumerable<KeyValuePair<FileLineTypes, string>> GetFileLineTypes(MemoryStream stream, bool useLessCommon)
        {
            // construct an enumeration of the "common" line ending enumerations..
            FileLineTypes fileLineTypes =
            FileLineTypes.CR |
            FileLineTypes.CRLF |
            FileLineTypes.LF |
            FileLineTypes.LFCR |
            FileLineTypes.UCR |
            FileLineTypes.UCRLF |
            FileLineTypes.ULF |
            FileLineTypes.ULFCR;

            // if the less common line endings are required..
            if (useLessCommon)
            {
                // add them to the enumeration..
                fileLineTypes |= FileLineTypes.NEWLINE | FileLineTypes.NL | FileLineTypes.RS | FileLineTypes.ATASCII;
            }

            // call the suitable overload..
            return GetFileLineTypes(stream, fileLineTypes);
        }

        /// <summary>
        /// Gets the file line types of a given memory stream.
        /// </summary>
        /// <param name="buffer">The byte array to be used to check for the line endings.</param>
        /// <param name="useLessCommon">An flag indicating if the less common line endings should also be checked.</param>
        /// <returns>A collection of line ending key value pairs with their enumeration values and their names.</returns>
        public static IEnumerable<KeyValuePair<FileLineTypes, string>> GetFileLineTypes(byte[] buffer, bool useLessCommon)
        {
            // construct an enumeration of the "common" line ending enumerations..
            FileLineTypes fileLineTypes = // the default set..
            FileLineTypes.CR |
            FileLineTypes.CRLF |
            FileLineTypes.LF |
            FileLineTypes.LFCR |
            FileLineTypes.UCR |
            FileLineTypes.UCRLF |
            FileLineTypes.ULF |
            FileLineTypes.ULFCR;

            // if the less common line endings are required..
            if (useLessCommon)
            {
                // add them to the enumeration..
                fileLineTypes |= FileLineTypes.NEWLINE | FileLineTypes.NL | FileLineTypes.RS | FileLineTypes.ATASCII;
            }

            // call the suitable overload..
            return GetFileLineTypes(buffer, fileLineTypes);
        }


        /// <summary>
        /// Gets the file line types of a text and a given encoding.
        /// </summary>
        /// <param name="contents">The text of which contents to check for the line endings.</param>
        /// <param name="encoding">The encoding of the text.</param>
        /// <param name="fileLineTypes">The types of line endings wanted to be included in the search.</param>
        /// <returns>A collection of line ending key value pairs with their enumeration values and their names.</returns>
        public static IEnumerable<KeyValuePair<FileLineTypes, string>> GetFileLineTypes(
            string contents, System.Text.Encoding encoding,
            FileLineTypes fileLineTypes = // the default set..
                FileLineTypes.CR |
                FileLineTypes.CRLF |
                FileLineTypes.LF |
                FileLineTypes.LFCR |
                FileLineTypes.UCR |
                FileLineTypes.UCRLF |
                FileLineTypes.ULF |
                FileLineTypes.ULFCR)
        {
            return GetFileLineTypes(encoding.GetBytes(contents));
        }

        /// <summary>
        /// Gets the file line types of a given byte array.
        /// </summary>
        /// <param name="buffer">The byte array to be used to check for the line endings.</param>
        /// <param name="fileLineTypes">The types of line endings wanted to be included in the search.</param>
        /// <returns>A collection of line ending key value pairs with their enumeration values and their names.</returns>
        public static IEnumerable<KeyValuePair<FileLineTypes, string>> GetFileLineTypes(byte[] buffer,
            FileLineTypes fileLineTypes = // the default set..
            FileLineTypes.CR |
            FileLineTypes.CRLF |
            FileLineTypes.LF |
            FileLineTypes.LFCR |
            FileLineTypes.UCR |
            FileLineTypes.UCRLF |
            FileLineTypes.ULF |
            FileLineTypes.ULFCR)
        {
            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                return GetFileLineTypes(memoryStream, fileLineTypes);
            }
        }

        /// <summary>
        /// Gets the file line types of a given memory stream.
        /// </summary>
        /// <param name="stream">The memory stream to be used to check for the line endings.</param>
        /// <param name="fileLineTypes">The types of line endings wanted to be included in the search.</param>
        /// <returns>A collection of line ending key value pairs with their enumeration values and their names.</returns>
        public static IEnumerable<KeyValuePair<FileLineTypes, string>> GetFileLineTypes(MemoryStream stream,
            FileLineTypes fileLineTypes = // the default set..
            FileLineTypes.CR |
            FileLineTypes.CRLF |
            FileLineTypes.LF |
            FileLineTypes.LFCR |
            FileLineTypes.UCR |
            FileLineTypes.UCRLF |
            FileLineTypes.ULF |
            FileLineTypes.ULFCR)
        {
            // do note that this is the "master" method of the overloads, so the logic is going to be weird..
            List<KeyValuePair<FileLineTypes, string>> result = new List<KeyValuePair<FileLineTypes, string>>();

            // the contents of the given memory stream is requires as a byte array..
            byte[] contentsBytes = stream.ToArray();

            // indicators if a negative or positive index for the byte array
            // search was given..
            bool eof1, eof2, eof3;

            // initialize the loop variable..
            int i = 0;

            // loop through the bytes in the array..
            while (i < contentsBytes.Length)
            {
                // comparison of CR+LF without Unicode..
                if (ContainsBytes(i, contentsBytes, new byte[] { 0x0D, 0x0A }, out eof1))
                {
                    if (eof1) // check for an overflow of the byte array..
                    {
                        // the index was outside the byte array so do
                        // increase the loop variable by one and continue..
                        i++; 
                        continue;
                    }

                    i += 2; // increase the loop variable by the amount of bytes used in the comparison..

                    // check that the result value doesn't already contain the given enumeration and string pair..
                    if (!result.Exists(f => f.Key == FileLineTypes.CRLF) && fileLineTypes.HasFlag(FileLineTypes.CRLF))
                    {
                        result.Add(
                            new KeyValuePair<FileLineTypes, string>(FileLineTypes.CRLF, GetLineEndingDescriptionByEnumeration(FileLineTypes.CRLF, UseEnumNames)));
                    }
                    continue; // match found so do continue the loop..
                }

                // comparison of LF+CR without Unicode..
                if (ContainsBytes(i, contentsBytes, new byte[] { 0x0A, 0x0D }, out eof1))
                {
                    if (eof1) // check for an overflow of the byte array..
                    {
                        // the index was outside the byte array so do
                        // increase the loop variable by one and continue..
                        i++;
                        continue;
                    }

                    i += 2; // increase the loop variable by the amount of bytes used in the comparison..

                    // check that the result value doesn't already contain the given enumeration and string pair..
                    if (!result.Exists(f => f.Key == FileLineTypes.LFCR) && fileLineTypes.HasFlag(FileLineTypes.LFCR))
                    {
                        result.Add(
                            new KeyValuePair<FileLineTypes, string>(FileLineTypes.LFCR, GetLineEndingDescriptionByEnumeration(FileLineTypes.LFCR, UseEnumNames)));
                    }

                    continue; // match found so do continue the loop..
                }

                // comparison of LF without Unicode..
                if (ContainsBytes(i, contentsBytes, new byte[] { 0x0A }, out eof1) &&
                    !ContainsBytes(i + 1, contentsBytes, new byte[] { 0x0D }, out eof2) &&
                    !ContainsBytes(i - 1, contentsBytes, new byte[] { 0x0D }, out eof3))
                {
                    if (eof1 || eof2 || eof3) // check for an overflow of the byte array..
                    {
                        // one or more of the indexes was outside the byte array so do
                        // increase the loop variable by one and continue..
                        i++;
                        continue;
                    }

                    i++; // increase the loop variable by the amount of bytes used in the comparison..

                    // check that the result value doesn't already contain the given enumeration and string pair..
                    if (!result.Exists(f => f.Key == FileLineTypes.LF) && fileLineTypes.HasFlag(FileLineTypes.LF))
                    {
                        result.Add(
                            new KeyValuePair<FileLineTypes, string>(FileLineTypes.LF, GetLineEndingDescriptionByEnumeration(FileLineTypes.LF, UseEnumNames)));
                    }

                    continue; // match found so do continue the loop..
                }

                // comparison of CR without Unicode..
                if (ContainsBytes(i, contentsBytes, new byte[] { 0x0D }, out eof1) &&
                    !ContainsBytes(i + 1, contentsBytes, new byte[] { 0x0A }, out eof2) &&
                    !ContainsBytes(i - 1, contentsBytes, new byte[] { 0x0A }, out eof3))
                {
                    if (eof1 || eof2 || eof3) // check for an overflow of the byte array..
                    {
                        // one or more of the indexes was outside the byte array so do
                        // increase the loop variable by one and continue..
                        i++;
                        continue;
                    }

                    i++; // increase the loop variable by the amount of bytes used in the comparison..

                    // check that the result value doesn't already contain the given enumeration and string pair..
                    if (!result.Exists(f => f.Key == FileLineTypes.CR) && fileLineTypes.HasFlag(FileLineTypes.CR))
                    {
                        result.Add(
                            new KeyValuePair<FileLineTypes, string>(FileLineTypes.CR, GetLineEndingDescriptionByEnumeration(FileLineTypes.CR, UseEnumNames)));
                    }
                    continue; // match found so do continue the loop..
                }

                // comparison of CR+LF with Unicode..
                if (ContainsBytes(i, contentsBytes, new byte[] { 0x00, 0x0D, 0x00, 0x0A }, out eof1))
                {
                    if (eof1) // check for an overflow of the byte array..
                    {
                        // the index was outside the byte array so do
                        // increase the loop variable by one and continue..
                        i++;
                        continue;
                    }

                    i += 4; // increase the loop variable by the amount of bytes used in the comparison..

                    // check that the result value doesn't already contain the given enumeration and string pair..
                    if (!result.Exists(f => f.Key == FileLineTypes.UCRLF) && fileLineTypes.HasFlag(FileLineTypes.UCRLF))
                    {
                        result.Add(
                            new KeyValuePair<FileLineTypes, string>(FileLineTypes.UCRLF, GetLineEndingDescriptionByEnumeration(FileLineTypes.UCRLF, UseEnumNames)));
                    }
                    continue; // match found so do continue the loop..
                }

                // comparison of LF+CR with Unicode..
                if (ContainsBytes(i, contentsBytes, new byte[] { 0x00, 0x0A, 0x00, 0x0D }, out eof1))
                {
                    if (eof1) // check for an overflow of the byte array..
                    {
                        // the index was outside the byte array so do
                        // increase the loop variable by one and continue..
                        i++;
                        continue;
                    }

                    i += 4; // increase the loop variable by the amount of bytes used in the comparison..

                    // check that the result value doesn't already contain the given enumeration and string pair..
                    if (!result.Exists(f => f.Key == FileLineTypes.ULFCR) && fileLineTypes.HasFlag(FileLineTypes.ULFCR))
                    {
                        result.Add(
                            new KeyValuePair<FileLineTypes, string>(FileLineTypes.ULFCR, GetLineEndingDescriptionByEnumeration(FileLineTypes.ULFCR, UseEnumNames)));
                    }
                    continue; // match found so do continue the loop..
                }

                // comparison of LF with Unicode..
                if (ContainsBytes(i, contentsBytes, new byte[] { 0x00, 0x0A }, out eof1) &&
                    !ContainsBytes(i + 2, contentsBytes, new byte[] { 0x00, 0x0D }, out eof2) &&
                    !ContainsBytes(i - 2, contentsBytes, new byte[] { 0x00, 0x0D }, out eof3))
                {
                    if (eof1 || eof2 || eof3) // check for an overflow of the byte array..
                    {
                        // one or more of the indexes was outside the byte array so do
                        // increase the loop variable by one and continue..
                        i++;
                        continue;
                    }

                    i += 2; // increase the loop variable by the amount of bytes used in the comparison..

                    // check that the result value doesn't already contain the given enumeration and string pair..
                    if (!result.Exists(f => f.Key == FileLineTypes.ULF) && fileLineTypes.HasFlag(FileLineTypes.ULF))
                    {
                        result.Add(
                            new KeyValuePair<FileLineTypes, string>(FileLineTypes.ULF, GetLineEndingDescriptionByEnumeration(FileLineTypes.ULF, UseEnumNames)));
                    }

                    continue; // match found so do continue the loop..
                }

                // comparison of CR with Unicode..
                if (ContainsBytes(i, contentsBytes, new byte[] { 0x00, 0x0D }, out eof1) &&
                    !ContainsBytes(i + 2, contentsBytes, new byte[] { 0x00, 0x0A }, out eof2) &&
                    !ContainsBytes(i - 2, contentsBytes, new byte[] { 0x00, 0x0A }, out eof3))
                {
                    if (eof1 || eof2 || eof3) // check for an overflow of the byte array..
                    {
                        // one or more of the indexes was outside the byte array so do
                        // increase the loop variable by one and continue..
                        i++;
                        continue;
                    }

                    i += 2; // increase the loop variable by the amount of bytes used in the comparison..

                    // check that the result value doesn't already contain the given enumeration and string pair..
                    if (!result.Exists(f => f.Key == FileLineTypes.UCR) && fileLineTypes.HasFlag(FileLineTypes.UCR))
                    {
                        result.Add(
                            new KeyValuePair<FileLineTypes, string>(FileLineTypes.UCR, GetLineEndingDescriptionByEnumeration(FileLineTypes.UCR, UseEnumNames)));
                    }
                    continue; // match found so do continue the loop..
                }

                // comparison of RS (see: FileLineTypes.RS description..)..
                if (ContainsBytes(i, contentsBytes, new byte[] { 0x1E }, out eof1))
                {
                    if (eof1) // check for an overflow of the byte array..
                    {
                        // the index was outside the byte array so do
                        // increase the loop variable by one and continue..
                        i++;
                        continue;
                    }

                    i++; // increase the loop variable by the amount of bytes used in the comparison..

                    // check that the result value doesn't already contain the given enumeration and string pair..
                    if (!result.Exists(f => f.Key == FileLineTypes.RS) && fileLineTypes.HasFlag(FileLineTypes.RS))
                    {
                        result.Add(
                            new KeyValuePair<FileLineTypes, string>(FileLineTypes.RS, GetLineEndingDescriptionByEnumeration(FileLineTypes.RS, UseEnumNames)));
                    }
                    continue; // match found so do continue the loop..
                }

                // comparison of NL (see: FileLineTypes.NL description..)..
                if (ContainsBytes(i, contentsBytes, new byte[] { 0x15 }, out eof1))
                {
                    if (eof1) // check for an overflow of the byte array..
                    {
                        // the index was outside the byte array so do
                        // increase the loop variable by one and continue..
                        i++;
                        continue;
                    }

                    i++; // increase the loop variable by the amount of bytes used in the comparison..

                    // check that the result value doesn't already contain the given enumeration and string pair..
                    if (!result.Exists(f => f.Key == FileLineTypes.NL) && fileLineTypes.HasFlag(FileLineTypes.NL))
                    {
                        result.Add(
                            new KeyValuePair<FileLineTypes, string>(FileLineTypes.NL, GetLineEndingDescriptionByEnumeration(FileLineTypes.NL, UseEnumNames)));
                    }
                    continue; // match found so do continue the loop..
                }

                // comparison of ATASCII (see: FileLineTypes.ATASCII description..)..
                if (ContainsBytes(i, contentsBytes, new byte[] { 0x9B }, out eof1))
                {
                    if (eof1) // check for an overflow of the byte array..
                    {
                        // the index was outside the byte array so do
                        // increase the loop variable by one and continue..
                        i++;
                        continue;
                    }

                    i++; // increase the loop variable by the amount of bytes used in the comparison..

                    // check that the result value doesn't already contain the given enumeration and string pair..
                    if (!result.Exists(f => f.Key == FileLineTypes.ATASCII) && fileLineTypes.HasFlag(FileLineTypes.ATASCII))
                    {
                        result.Add(
                            new KeyValuePair<FileLineTypes, string>(FileLineTypes.ATASCII, GetLineEndingDescriptionByEnumeration(FileLineTypes.ATASCII, UseEnumNames)));
                    }
                    continue; // match found so do continue the loop..
                }

                // comparison of NEWLINE (see: FileLineTypes.NEWLINE description..)..
                if (ContainsBytes(i, contentsBytes, new byte[] { 0x76 }, out eof1))
                {
                    if (eof1) // check for an overflow of the byte array..
                    {
                        // the index was outside the byte array so do
                        // increase the loop variable by one and continue..
                        i++;
                        continue;
                    }

                    i++; // increase the loop variable by the amount of bytes used in the comparison..

                    // check that the result value doesn't already contain the given enumeration and string pair..
                    if (!result.Exists(f => f.Key == FileLineTypes.NEWLINE) && fileLineTypes.HasFlag(FileLineTypes.NEWLINE))
                    {
                        result.Add(
                            new KeyValuePair<FileLineTypes, string>(FileLineTypes.NEWLINE, GetLineEndingDescriptionByEnumeration(FileLineTypes.NEWLINE, UseEnumNames)));
                    }
                    continue; // match found so do continue the loop..
                }
                i++; // the loop must not be infinite so increase the loop variable by one..
            }

            // if nothing was found assume CR+LF..
            if (result.Count == 0)
            {
                result.Add(
                    new KeyValuePair<FileLineTypes, string>(FileLineTypes.CRLF, GetLineEndingDescriptionByEnumeration(FileLineTypes.CRLF, UseEnumNames)));
            }

            // if multiple items was found, assume mixed line endings..
            if (result.Count() > 1)
            {
                result.Add(
                    new KeyValuePair<FileLineTypes, string>(FileLineTypes.Mixed, GetLineEndingDescriptionByEnumeration(FileLineTypes.Mixed, UseEnumNames)));
            }

            // return the result..
            return result;
        }

        /// <summary>
        /// Checks if the given <paramref name="index"/> is outside of the boundaries of the <paramref name="content"/> byte array.
        /// </summary>
        /// <param name="index">The index to check.</param>
        /// <param name="content">The content to check.</param>
        /// <param name="match">The match to check.</param>
        /// <returns>Returns true if the index in addition for the <paramref name="match"/> length is out if range within the <paramref name="content"/> byte array; otherwise false.</returns>
        private static bool ArrayOutOfRange(int index, byte[] content, byte[] match)
        {
            // comparison with the index..
            if (index + match.Length >= content.Length || index < 0)
            {
                return true; // out of range..
            }

            // within range..
            return false;
        }

        /// <summary>
        /// Determines whether the specified index in the given contents contains the given match byte array.
        /// </summary>
        /// <param name="index">The index where to compare the <paramref name="match"/> with the <paramref name="content"/> byte array.</param>
        /// <param name="content">The contents of the base byte array.</param>
        /// <param name="match">The match byte array which to compare with the <paramref name="content"/> byte array.</param>
        /// <param name="eof">A variable indicating if the comparison would have been outside the boundaries of the <paramref name="content"/> byte array.</param>
        /// <returns>
        ///   <c>true</c> if the specified index contains bytes; otherwise, <c>false</c>.
        /// </returns>
        private static bool ContainsBytes(int index, byte[] content, byte[] match, out bool eof)
        {
            try
            {
                // see if the given parameters are withing range..
                if (ArrayOutOfRange(index, content, match))
                {
                    // set the EOF flag value..
                    eof = true;
                    return false; // ..and return false..
                }
                else
                {
                    // check if the given match parameter is found from the
                    // content parameter's value with the given index..
                    int matchIndex = 0; // a variable to hold the indexer for the match parameter..
                    bool resultBool = true; // an assumption of true..
                    for (int i = index; i < content.Length; i++)
                    {
                        // if the indexer variable for the match parameter
                        // exceeds the boundaries of the match parameter byte array..
                        if (matchIndex >= match.Length)
                        {
                            break; // ..break the loop..
                        }

                        // append boolean algebra to the result variable..
                        resultBool &= content[i] == match[matchIndex++];
                    }
                    eof = false; // set the EOF flag value..
                    return resultBool; // return the result..
                }
            }
            catch // an exception occurred..
            {
                eof = false; // ..set the EOF flag value..
                return false; // ..and return false..
            }
        }
    }
}
