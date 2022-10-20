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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace InstallerBaseWixSharp.Registry
{
    // ReSharper disable once UnusedMember.Global, this is a class to be copied and used in another program..    
    /// <summary>
    /// A class to help with the <see cref="Registry"/> and file type association(s)/registration(s).
    /// </summary>
    public class RegistryFileAssociation
    {
        /// <summary>
        /// A constant string to put in between of the application identifier and the extension.
        /// </summary>
        private const string AssocFile = "AssocFile";

        /// <summary>
        /// The <see cref="Registry"/> default icon constant.
        /// </summary>
        private const string DefaultIcon = "DefaultIcon";

        // ReSharper disable once IdentifierTypo        
        /// <summary>
        /// The <see cref="Registry"/> open with program id(s) key name.
        /// </summary>
        private const string OpenWithProgIds = "OpenWithProgIds";

        /// <summary>
        /// The <see cref="Registry"/> file extensions sub-tree.
        /// </summary>
        // ReSharper disable once IdentifierTypo
        private const string FileExts = "FileExts";

        /// <summary>
        /// Gets or sets the action which is called in case of an exception within the class code.
        /// </summary>
        /// <value>The report exception action.</value>
        public static Action<Exception> ReportExceptionAction { get; set; }

        // ReSharper disable once CommentTypo
        /// <summary>
        /// Associates/registers the specified file types (extensions) to a specified application.
        /// </summary>
        /// <param name="company">The company name of the software in question.</param>
        /// <param name="applicationName">Name of the application.</param>
        /// <param name="applicationExecutableFile">The application executable file.</param>
        /// <param name="associationList">A semi-colon delimited string of file extensions and their names (.ext;name|.ext2;name2).</param>
        /// <param name="rootDefault">if set to <c>true</c> HKCR (HKey Classes Root) default value is set for the specified application.</param>
        /// <param name="iconIndex">Index of the icon to use from the <see paramref="applicationExecutableFile"/>.</param>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        // ReSharper disable once UnusedMember.Global, this is a class to be copied and used in another program..
        public static bool AssociateFiles(string company, string applicationName, string applicationExecutableFile, string associationList,
            bool rootDefault, int iconIndex = 0)
        {
            var result = false;

            try
            {
                var appRegistryTree =string.Concat(@"SOFTWARE\", company, @"\", applicationName);

                // register the registered file types..
                using (var key = CommonCalls.OpenOrCreateKeyHKLM(appRegistryTree))
                {
                    key.SetValue("Associations", associationList);
                }

                var associationStrings = associationList.Split(':');

                foreach (var associationString in associationStrings)
                {
                    var associationData = associationString.Split('|');

                    if (associationData[0].StartsWith("(")) // format: (.m3u/.m3u8)|Music playlist files..
                    {
                        string[] innerExtensions = associationData[0].TrimStart('(').TrimEnd(')').Split('/');
                        foreach (var innerExtension in innerExtensions)
                        {
                            result |= Associate(applicationExecutableFile, innerExtension, applicationName, rootDefault,
                                associationData[1], iconIndex);
                        }
                    }
                    else // format: .txt|Text files..
                    {
                        result |= Associate(applicationExecutableFile, associationData[0], applicationName, rootDefault,
                            associationData[1], iconIndex);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                // report the exception..
                ReportExceptionAction?.Invoke(ex);
                return false;
            }
        }

        /// <summary>
        /// Removes the associations/registrations of the specified file types (extensions) from a specified application.
        /// </summary>
        /// <param name="company">The company name of the software in question.</param>
        /// <param name="applicationName">Name of the application.</param>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        // ReSharper disable once UnusedMember.Global, this is a class to be copied and used in another program..
        public static bool UnAssociateFiles(string company, string applicationName)
        {
            var result = false;

            try
            {
                int valueCount;

                var appRegistryTree = string.Concat(@"SOFTWARE\", company, @"\", applicationName);

                // register the registered file types..
                using (var key = CommonCalls.OpenOrCreateKeyHKLM(appRegistryTree))
                {
                    var registryValue = key.GetValue("Associations").ToString();
                    var associationStrings = registryValue.Split(':');

                    foreach (var associationString in associationStrings)
                    {
                        var extension = associationString.Split('|')[0];

                        if (extension.StartsWith("(")) // format: (.m3u/.m3u8)|Music playlist files..
                        {
                            string[] innerExtensions = extension.TrimStart('(').TrimEnd(')').Split('/');
                            foreach (var innerExtension in innerExtensions)
                            {
                                result |= UnAssociate(innerExtension, applicationName);
                            }
                        }
                        else // format: .txt|Text files..
                        {
                            result |= UnAssociate(extension, applicationName);
                        }
                    }

                    valueCount = key.ValueCount;
                }

                if (valueCount == 0)
                {
                    Microsoft.Win32.Registry.LocalMachine.DeleteSubKeyTree(appRegistryTree);
                }

                return result;
            }
            catch (Exception ex)
            {
                // report the exception..
                ReportExceptionAction?.Invoke(ex);
                return false;
            }
        }

        // ReSharper disable once CommentTypo
        /// <summary>
        /// Associates/registers the specified file type (extension) to a specified application.
        /// </summary>
        /// <param name="applicationExecutableFile">The application executable file.</param>
        /// <param name="extension">The extension to create the association to.</param>
        /// <param name="applicationName">The name of the application.</param>
        /// <param name="rootDefault">if set to <c>true</c> HKCR (HKey Classes Root) default value is set for the specified application.</param>
        /// <param name="associationName">Name of the association.</param>
        /// <param name="iconIndex">Index of the icon to use from the <see paramref="applicationExecutableFile"/>.</param>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        // ReSharper disable once UnusedMember.Global, this is a class to be copied and used in another program..
        public static bool Associate(string applicationExecutableFile, string extension, string applicationName,
            bool rootDefault, string associationName, int iconIndex = 0)
        {
            // lower case extensions..
            extension = extension.ToLower();
            try
            {
                var programId = string.Concat(applicationName, ".", AssocFile, extension.ToUpper());
                var programIdToast = string.Concat(programId, "_" + extension);

                // ReSharper disable once CommentTypo
                // only mark the default to the HKCR if the parameter is set..
                if (rootDefault)
                {
                    // ReSharper disable once ConvertToUsingDeclaration :: this must work with .NET v4..
                    using (var registryKey = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(extension))
                    {
                        registryKey?.SetValue("", programId);
                    }
                }

                using (var registryKey = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(string.Concat(extension, @"\", OpenWithProgIds)))
                {
                    registryKey?.SetValue(programId, new byte[0], RegistryValueKind.None);
                }

                // ReSharper disable twice CommentTypo
                // create the HKML\ProgID key..
                using (var registryKey = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(programId))
                {
                    // set the default key value..
                    registryKey?.SetValue("", associationName);
                }

                // the assumption of this class is that the executable also contains the icon for the file association
                // having index of zero..
                using (var registryKey = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(string.Concat(programId, "\\", DefaultIcon)))
                {
                    registryKey?.SetValue("", string.Concat(applicationExecutableFile, $",{iconIndex}"),
                        RegistryValueKind.ExpandString);
                }

                // the open command for the software..
                using (var registryKey =
                    Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(string.Concat(programId, @"\shell\open\command")))
                {
                    registryKey?.SetValue("", string.Concat(applicationExecutableFile, " \"%1\""),
                        RegistryValueKind.ExpandString);
                }

                // no idea what this is..
                using (var registryKey =
                    Microsoft.Win32.Registry.CurrentUser.CreateSubKey(
                        @"\SOFTWARE\Microsoft\Windows\CurrentVersion\ApplicationAssociationToasts"))
                {
                    registryKey?.SetValue(programIdToast, 0);
                }

                // this is fun..
                using (var registryKey =
                    Microsoft.Win32.Registry.CurrentUser.CreateSubKey(
                        @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\" + extension +
                        @"\OpenWithProgids"))
                {
                    registryKey?.SetValue(programId, new byte [0], RegistryValueKind.None);
                }

                // encrypted user choise..
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree(string.Concat(
                    // ReSharper disable once StringLiteralTypo
                    @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\", extension, @"\UserChoice"));

                // the HKLM part..
                using (var registryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"\SOFTWARE\Classes\" + extension))
                {
                    registryKey?.SetValue("", programId);
                }

                // again with the ProgIds..
                using (var registryKey =
                    Microsoft.Win32.Registry.CurrentUser.CreateSubKey(string.Concat(@"\SOFTWARE\Classes\", extension,
                        @"\", OpenWithProgIds)))
                {
                    registryKey?.SetValue(programId, new byte[0], RegistryValueKind.None);
                }

                // classes here and there..
                using (var registryKey =
                    Microsoft.Win32.Registry.CurrentUser.CreateSubKey(string.Concat(@"\SOFTWARE\Classes\", extension)))
                {
                    registryKey?.SetValue("", programId, RegistryValueKind.ExpandString);
                }

                #region HKEY_LOCAL_MACHINE\SOFTWARE\Classes\ProgId\
                using (var registryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"\SOFTWARE\Classes\" + programId))
                {
                    registryKey?.SetValue("", associationName);
                }

                using (var registryKey =
                    Microsoft.Win32.Registry.CurrentUser.CreateSubKey(string.Concat(@"\SOFTWARE\Classes\", programId, @"\\",
                        DefaultIcon))) 
                {
                    registryKey?.SetValue("", string.Concat(applicationExecutableFile, $"\\,{iconIndex}"),
                        RegistryValueKind.ExpandString);
                }

                using (var registryKey =
                    Microsoft.Win32.Registry.CurrentUser.CreateSubKey(string.Concat(@"\SOFTWARE\Classes\", programId,
                        @"\shell\open\command"))) 
                {
                    registryKey?.SetValue("", string.Concat(applicationExecutableFile, " \"%1\""),
                        RegistryValueKind.ExpandString);
                }
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                // report the exception..
                ReportExceptionAction?.Invoke(ex);
                return false;
            }
        }

        // ReSharper disable once UnusedMember.Global, this is a class to be copied and used in another program..
        /// <summary>
        /// Removes file association/registration from a specified file exctension and application name.
        /// </summary>
        /// <param name="extension">The extension which association/registration to remove.</param>
        /// <param name="applicationName">Name of the application.</param>
        /// <returns><c>true</c> if association was successfully removed, <c>false</c> otherwise.</returns>
        public static bool UnAssociate(string extension, string applicationName)
        {
            try
            {
                var programId = string.Concat(applicationName, ".", AssocFile, extension.ToUpper());
                var programIdToast = string.Concat(programId, "_" + extension);
                bool deleteTree;

                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree(string.Concat(@"\SOFTWARE\Classes\", programId));

                // encrypted user choice..
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree(string.Concat(
                    @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\", FileExts, @"\", extension, @"\UserChoice"));

                // the HKLM part..
                using (var registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"\SOFTWARE\Classes\" + extension, true))
                {
                    registryKey?.DeleteValue("");
                }

                // again with the ProgIds..
                using (var registryKey =
                    Microsoft.Win32.Registry.CurrentUser.OpenSubKey(string.Concat(@"\SOFTWARE\Classes\", extension,
                        @"\", OpenWithProgIds), true))
                {
                    registryKey?.DeleteValue(programId);
                }
                
                // no idea what this is..
                using (var registryKey =
                    Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                        @"\SOFTWARE\Microsoft\Windows\CurrentVersion\ApplicationAssociationToasts", true))
                {
                    registryKey?.DeleteValue(programIdToast);
                }

                // this is fun..
                using (var registryKey =
                    Microsoft.Win32.Registry.CurrentUser.OpenSubKey(string.Concat(
                        @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\", FileExts, @"\", extension,
                        @"\", OpenWithProgIds), true))
                {
                    registryKey?.DeleteValue(programId);
                }

                using (var registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(string.Concat(extension, @"\", OpenWithProgIds), true))
                {
                    registryKey?.DeleteValue(programId);
                }

                Microsoft.Win32.Registry.ClassesRoot.DeleteSubKeyTree(programId);

                using (var registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(extension, true))
                {
                    // ReSharper disable once CommentTypo
                    // only mark the default to the HKCR if the parameter is set..
                    if (registryKey?.GetValue("").ToString() == programId)
                    {
                        registryKey.DeleteValue("");
                    }

                    registryKey?.DeleteValue(programId);
                }

                // classes here and there..
                using (var registryKey =
                    Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(string.Concat(@"\SOFTWARE\Classes\", extension), true))
                {
                    if (registryKey?.GetValue("")?.ToString() == programId)
                    {
                        registryKey.DeleteValue("");
                    }

                    deleteTree = registryKey?.ValueCount == 0;
                }

                if (deleteTree)
                {
                    Microsoft.Win32.Registry.ClassesRoot.DeleteSubKeyTree(string.Concat(@"\SOFTWARE\Classes\", extension));
                }

                return true;
            }
            catch (Exception ex)
            {
                // report the exception..
                ReportExceptionAction?.Invoke(ex);
                return false;
            }
        }

        // ReSharper disable once UnusedMember.Global, this is a class to be copied and used in another program..        
        /// <summary>
        /// Calls the Windows API SHChangeNotify function with pre-determined parametes.
        /// </summary>
        public static void ShellChangeNotify()
        {
            // ReSharper disable once CommentTypo
            // credits to pinvoke.net..
            SHChangeNotify(HChangeNotifyEventID.SHCNE_ASSOCCHANGED, HChangeNotifyFlags.SHCNF_IDLIST,
                IntPtr.Zero, IntPtr.Zero);
        }

        #region PInvoke        
        /// <summary>
        /// Notifies the system of an event that an application has performed. An application should use this function if it performs an action that may affect the Shell.
        /// </summary>
        /// <param name="wEventId">escribes the event that has occurred. Typically, only one event is specified at a time. If more than one event is specified, the values contained in the dwItem1 and dwItem2 parameters must be the same, respectively, for all specified events.</param>
        /// <param name="uFlags">Flags that, when combined bitwise with SHCNF_TYPE, indicate the meaning of the dwItem1 and dwItem2 parameters.</param>
        /// <param name="dwItem1">Optional. First event-dependent value.</param>
        /// <param name="dwItem2">Optional. Second event-dependent value.</param>
        [DllImport("shell32.dll")]
        static extern void SHChangeNotify(HChangeNotifyEventID wEventId,
            HChangeNotifyFlags uFlags,
            IntPtr dwItem1,
            IntPtr dwItem2);

        // (C)::https://pinvoke.net/default.aspx/shell32/HChangeNotifyEventID.html

        /// <summary>
        /// Describes the event that has occurred.
        /// Typically, only one event is specified at a time.
        /// If more than one event is specified, the values contained
        /// in the <i>dwItem1</i> and <i>dwItem2</i>
        /// parameters must be the same, respectively, for all specified events.
        /// This parameter can be one or more of the following values.
        /// </summary>
        /// <remarks>
        /// <para><b>Windows NT/2000/XP:</b> <i>dwItem2</i> contains the index
        /// in the system image list that has changed.
        /// <i>dwItem1</i> is not used and should be <see langword="null"/>.</para>
        /// <para><b>Windows 95/98:</b> <i>dwItem1</i> contains the index
        /// in the system image list that has changed.
        /// <i>dwItem2</i> is not used and should be <see langword="null"/>.</para>
        /// </remarks>
        [Flags]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [SuppressMessage("ReSharper", "IdentifierTypo")]
        [SuppressMessage("ReSharper", "CommentTypo")]
        // ReSharper disable once CheckNamespace
        public enum HChangeNotifyEventID
        {
            /// <summary>
            /// All events have occurred.
            /// </summary>
            SHCNE_ALLEVENTS = 0x7FFFFFFF,

            /// <summary>
            /// A file type association has changed. <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/>
            /// must be specified in the <i>uFlags</i> parameter.
            /// <i>dwItem1</i> and <i>dwItem2</i> are not used and must be <see langword="null"/>.
            /// </summary>
            SHCNE_ASSOCCHANGED = 0x08000000,

            /// <summary>
            /// The attributes of an item or folder have changed.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the item or folder that has changed.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_ATTRIBUTES = 0x00000800,

            /// <summary>
            /// A nonfolder item has been created.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the item that was created.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_CREATE = 0x00000002,

            /// <summary>
            /// A nonfolder item has been deleted.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the item that was deleted.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_DELETE = 0x00000004,

            /// <summary>
            /// A drive has been added.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the root of the drive that was added.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_DRIVEADD = 0x00000100,

            /// <summary>
            /// A drive has been added and the Shell should create a new window for the drive.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the root of the drive that was added.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_DRIVEADDGUI = 0x00010000,

            /// <summary>
            /// A drive has been removed. <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the root of the drive that was removed.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_DRIVEREMOVED = 0x00000080,

            /// <summary>
            /// Not currently used.
            /// </summary>
            SHCNE_EXTENDED_EVENT = 0x04000000,

            /// <summary>
            /// The amount of free space on a drive has changed.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the root of the drive on which the free space changed.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_FREESPACE = 0x00040000,

            /// <summary>
            /// Storage media has been inserted into a drive.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the root of the drive that contains the new media.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_MEDIAINSERTED = 0x00000020,

            /// <summary>
            /// Storage media has been removed from a drive.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the root of the drive from which the media was removed.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_MEDIAREMOVED = 0x00000040,

            /// <summary>
            /// A folder has been created. <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/>
            /// or <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the folder that was created.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_MKDIR = 0x00000008,

            /// <summary>
            /// A folder on the local computer is being shared via the network.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the folder that is being shared.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_NETSHARE = 0x00000200,

            /// <summary>
            /// A folder on the local computer is no longer being shared via the network.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the folder that is no longer being shared.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_NETUNSHARE = 0x00000400,

            /// <summary>
            /// The name of a folder has changed.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the previous pointer to an item identifier list (PIDL) or name of the folder.
            /// <i>dwItem2</i> contains the new PIDL or name of the folder.
            /// </summary>
            SHCNE_RENAMEFOLDER = 0x00020000,

            /// <summary>
            /// The name of a nonfolder item has changed.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the previous PIDL or name of the item.
            /// <i>dwItem2</i> contains the new PIDL or name of the item.
            /// </summary>
            SHCNE_RENAMEITEM = 0x00000001,

            /// <summary>
            /// A folder has been removed.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the folder that was removed.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_RMDIR = 0x00000010,

            /// <summary>
            /// The computer has disconnected from a server.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the server from which the computer was disconnected.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// </summary>
            SHCNE_SERVERDISCONNECT = 0x00004000,

            /// <summary>
            /// The contents of an existing folder have changed,
            /// but the folder still exists and has not been renamed.
            /// <see cref="HChangeNotifyFlags.SHCNF_IDLIST"/> or
            /// <see cref="HChangeNotifyFlags.SHCNF_PATH"/> must be specified in <i>uFlags</i>.
            /// <i>dwItem1</i> contains the folder that has changed.
            /// <i>dwItem2</i> is not used and should be <see langword="null"/>.
            /// If a folder has been created, deleted, or renamed, use SHCNE_MKDIR, SHCNE_RMDIR, or
            /// SHCNE_RENAMEFOLDER, respectively, instead.
            /// </summary>
            SHCNE_UPDATEDIR = 0x00001000,

            /// <summary>
            /// An image in the system image list has changed.
            /// <see cref="HChangeNotifyFlags.SHCNF_DWORD"/> must be specified in <i>uFlags</i>.
            /// </summary>
            SHCNE_UPDATEIMAGE = 0x00008000,

        }

        // (C)::https://pinvoke.net/default.aspx/shell32/HChangeNotifyFlags.html

        /// <summary>
        /// Flags that indicate the meaning of the <i>dwItem1</i> and <i>dwItem2</i> parameters.
        /// The uFlags parameter must be one of the following values.
        /// </summary>
        [Flags]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "IdentifierTypo")]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [SuppressMessage("ReSharper", "CommentTypo")]
        // ReSharper disable once CheckNamespace
        public enum HChangeNotifyFlags
        {
            /// <summary>
            /// The <i>dwItem1</i> and <i>dwItem2</i> parameters are DWORD values.
            /// </summary>
            SHCNF_DWORD = 0x0003,

            /// <summary>
            /// <i>dwItem1</i> and <i>dwItem2</i> are the addresses of ITEMIDLIST structures that
            /// represent the item(s) affected by the change.
            /// Each ITEMIDLIST must be relative to the desktop folder.
            /// </summary>
            SHCNF_IDLIST = 0x0000,

            /// <summary>
            /// <i>dwItem1</i> and <i>dwItem2</i> are the addresses of null-terminated strings of
            /// maximum length MAX_PATH that contain the full path names
            /// of the items affected by the change.
            /// </summary>
            SHCNF_PATHA = 0x0001,

            /// <summary>
            /// <i>dwItem1</i> and <i>dwItem2</i> are the addresses of null-terminated strings of
            /// maximum length MAX_PATH that contain the full path names
            /// of the items affected by the change.
            /// </summary>
            SHCNF_PATHW = 0x0005,

            /// <summary>
            /// <i>dwItem1</i> and <i>dwItem2</i> are the addresses of null-terminated strings of
            /// maximum length MAX_PATH that contain the full path names
            /// of the items affected by the change.
            /// </summary>
            SHCNF_PATH = 0x0005,

            /// <summary>
            /// <i>dwItem1</i> and <i>dwItem2</i> are the addresses of null-terminated strings that
            /// represent the friendly names of the printer(s) affected by the change.
            /// </summary>
            SHCNF_PRINTERA = 0x0002,

            /// <summary>
            /// <i>dwItem1</i> and <i>dwItem2</i> are the addresses of null-terminated strings that
            /// represent the friendly names of the printer(s) affected by the change.
            /// </summary>
            SHCNF_PRINTERW = 0x0006,

            /// <summary>
            /// The function should not return until the notification
            /// has been delivered to all affected components.
            /// As this flag modifies other data-type flags, it cannot by used by itself.
            /// </summary>
            SHCNF_FLUSH = 0x1000,

            /// <summary>
            /// The function should begin delivering notifications to all affected components
            /// but should return as soon as the notification process has begun.
            /// As this flag modifies other data-type flags, it cannot by used by itself.
            /// </summary>
            SHCNF_FLUSHNOWAIT = 0x2000,
        }
        #endregion
    }
}

