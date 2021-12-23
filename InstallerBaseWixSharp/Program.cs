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

// define this to use start application dialog..
#define UseRunProgramDialog
// define this to use the custom association dialog..
//#define UseAssociationDialog
// define this to use the local application data folder..
#define InstallLocalAppData
// define this to use the star(*) file registration..
#define ShellStarAssociate

using System;
using System.Diagnostics;
// ReSharper disable once RedundantUsingDirective, this depends on a compiler directive..
using System.IO;
using System.Windows.Forms;
using InstallerBaseWixSharp.Files.Dialogs;
// ReSharper disable once RedundantUsingDirective, this depends on a compiler directive..
using InstallerBaseWixSharp.Files.Localization.TabDeliLocalization;
using InstallerBaseWixSharp.Registry;
using WixSharp;
using WixSharp.Forms;
using File = WixSharp.File;
using ProcessExtensions = InstallerBaseWixSharp.Files.PInvoke.ProcessExtensions;

namespace InstallerBaseWixSharp
{
    class Program
    {
        const string AppName = "ScriptNotepad";
        internal static readonly string Executable = $"{AppName.TrimEnd('#')}.exe";
        const string  Company = "VPKSoft";
        private static readonly string InstallDirectory = $@"%ProgramFiles%\{Company}\{AppName}";
        const string  ApplicationIcon = @"..\ScriptNotepad\notepad7.ico";

        static void Main()
        {
            string appVersion = "1.0.0.0";
            string OutputFile() // get the executable file name and the version from it..
            {
                try
                {
                    var info = FileVersionInfo.GetVersionInfo(@"..\ScriptNotepad\bin\net6.0-windows\" + Executable);
                    appVersion = string.Concat(info.FileMajorPart, ".", info.FileMinorPart, ".", info.FileBuildPart);
                    return string.Concat(AppName, "_", info.FileMajorPart, ".", info.FileMinorPart, ".", info.FileBuildPart);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return AppName;
                }
            }

            var project = new ManagedProject("ScriptNotepad",
                new Dir(InstallDirectory,
                    new WixSharp.Files(@"..\ScriptNotepad\bin\net6.0-windows\*.*"),
                    new File("Program.cs")),
                new Dir($@"%ProgramMenu%\{Company}\{AppName}",
                    // ReSharper disable three times StringLiteralTypo
                    new ExeFileShortcut(AppName, $"[INSTALLDIR]{Executable}", "")
                    {
                        WorkingDirectory = "[INSTALLDIR]", IconFile = ApplicationIcon
                    }),
#if InstallLocalAppData
                new Dir($@"%LocalAppDataFolder%\{AppName}",
                    new File(@"..\ScriptNotepad\Localization\SQLiteDatabase\lang.sqlite"),
                    new Dir(@"Dictionaries\en", new File(@"..\dictionaries\en\en_US.dic"),
                        new File(@"..\dictionaries\en\en_US.aff"))),
                new Dir($@"%LocalAppDataFolder%\{AppName}\Plugins",
                    new File(@"..\PluginTemplate\bin\net6.0-windows\PluginTemplate.dll")),
#endif
                new CloseApplication($"[INSTALLDIR]{Executable}", true), 
                new Property("Executable", Executable),
                new Property("AppName", AppName),
                new Property("Company", Company))
            {
                GUID = new Guid("E142C7BA-2A0E-48B1-BB4E-CDC759AEBF91"),
                ManagedUI = new ManagedUI(),
                ControlPanelInfo = 
                {
                    Manufacturer = Company, 
                    UrlInfoAbout  = "https://www.vpksoft.net", 
                    Name = $"Installer for the {AppName} application", 
                    HelpLink = "https://www.vpksoft.net", 
                },
                Platform = Platform.x64,
                OutFileName = OutputFile(),
            };

            #region Upgrade
            // the application update process..
            project.Version = Version.Parse(appVersion);
            project.MajorUpgrade = MajorUpgrade.Default;
            #endregion

            project.Package.Name = $"Installer for the {AppName} application";

            //project.ManagedUI = ManagedUI.Empty;    //no standard UI dialogs
            //project.ManagedUI = ManagedUI.Default;  //all standard UI dialogs

            //custom set of standard UI dialogs

            project.ManagedUI.InstallDialogs.Add(Dialogs.Welcome)
                                            .Add(Dialogs.Licence)
#if UseRunProgramDialog
                                            .Add<RunProgramDialog>()
#endif
#if UseAssociationDialog
                                            .Add<AssociationsDialog>()
#endif                                            
                                            .Add<ProgressDialog>()
                                            .Add(Dialogs.Exit);

            project.ManagedUI.ModifyDialogs.Add(Dialogs.MaintenanceType)
                                           .Add(Dialogs.Features)
                                           .Add(Dialogs.Progress)
                                           .Add(Dialogs.Exit);

            project.ControlPanelInfo.ProductIcon = ApplicationIcon;

            AutoElements.UACWarning = "";


            project.BannerImage = @"Files\install_top.png";
            project.BackgroundImage = @"Files\install_side.png";
            project.LicenceFile = @"Files\MIT.License.rtf";

            RegistryFileAssociation.ReportExceptionAction = delegate(Exception ex)
            {
                MessageBox.Show(ex.Message);
            };

            project.AfterInstall += delegate(SetupEventArgs args)
            {
                string locale = "en-US";
                if (args.IsInstalling)
                {
                    try
                    {
                        locale = args.Session.Property("LANGNAME");
                    }
                    catch
                    {
                            // ignored..
                    }
                }

                try
                {
                    locale = CommonCalls.GetKeyValue(Company, AppName, "LOCALE");
                    CommonCalls.DeleteValue(Company, AppName, "LOCALE");
                }
                catch
                {
                    // ignored..
                }

                var sideLocalization = new TabDeliLocalization();
                sideLocalization.GetLocalizedTexts(Properties.Resources.tabdeli_messages);

                if (args.IsUninstalling)
                {
                    RegistryFileAssociation.UnAssociateFiles(Company, AppName);
                    CommonCalls.DeleteCompanyKeyIfEmpty(Company);

                    #if ShellStarAssociate
                    try
                    {
                        var openWithMessage = sideLocalization.GetMessage("txtOpenWithShellMenu",
                            "Open with ", locale);

                        RegistryStarAssociation.UnRegisterStarAssociation(openWithMessage);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    #endif

                    #if InstallLocalAppData
                    var messageCaption = sideLocalization.GetMessage("txtDeleteLocalApplicationData",
                        "Delete application data", locale);

                    var messageText = sideLocalization.GetMessage("txtDeleteApplicationDataQuery",
                        "Delete application settings and other data?", locale);

                    if (MessageBox.Show(messageText,
                        messageCaption, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        try
                        {
                            Directory.Delete(
                                Path.Combine(Environment.GetEnvironmentVariable("LOCALAPPDATA") ?? string.Empty,
                                    AppName), true);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    #endif
                }

                if (args.IsInstalling)
                {
                    #if ShellStarAssociate
                    var messageCaption = sideLocalization.GetMessage("txtStarAssociation",
                        "Add open with menu", locale);

                    var messageText = sideLocalization.GetMessage("txtStarAssociationQuery",
                        "Add an open with this application to the file explorer menu?", locale);

                    if (MessageBox.Show(messageText,
                        messageCaption, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        try
                        {
                            var openWithMessage = sideLocalization.GetMessage("txtOpenWithShellMenu",
                                "Open with ScriptNotepad", locale);

                            RegistryStarAssociation.RegisterStarAssociation(args.Session.Property("EXENAME"), AppName,
                                openWithMessage);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }                                    
                    #endif

                    RegistryFileAssociation.AssociateFiles(Company, AppName, args.Session.Property("EXENAME"),
                        args.Session.Property("ASSOCIATIONS"), true);

                    try
                    {
                        CommonCalls.SetKeyValue(Company, AppName, "LOCALE", args.Session.Property("LANGNAME"));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            };

            project.Load += Msi_Load;
            project.BeforeInstall += Msi_BeforeInstall;
            project.AfterInstall += Msi_AfterInstall;


            //project.SourceBaseDir = "<input dir path>";
            //project.OutDir = "<output dir path>";

            ValidateAssemblyCompatibility();

            project.DefaultDeferredProperties += ",RUNEXE,PIDPARAM,ASSOCIATIONS,LANGNAME,EXENAME";

            project.Localize();

            project.BuildMsi();
        }

        static void Msi_Load(SetupEventArgs e)
        {
            //if (!e.IsUninstalling) MessageBox.Show(e.ToString(), "Load");
        }

        static void Msi_BeforeInstall(SetupEventArgs e)
        {
            //if (!e.IsUninstalling) MessageBox.Show(e.ToString(), "BeforeInstall");
        }

        static void Msi_AfterInstall(SetupEventArgs e)
        {
            // run the executable after the install with delay (wait PID to )..
            if (e.IsInstalling)
            {
                try 
                {
                    if (System.IO.File.Exists(e.Session.Property("RUNEXE")))
                    {
                        ProcessExtensions.StartProcessAsCurrentUser(e.Session.Property("RUNEXE"),
                            $"--waitPid {e.Session.Property("PIDPARAM")}");
                    }
                    else
                    {
                        Console.WriteLine(e.Session.Property("RUNEXE"));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($@"Post run failed: '{ex.Message}'...");
                }
            }
        }

        static void ValidateAssemblyCompatibility()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            if (!assembly.ImageRuntimeVersion.StartsWith("v2."))
            {
                Console.WriteLine(
                    $@"Warning: assembly '{assembly.GetName().Name}' is compiled for {assembly.ImageRuntimeVersion}" +
                    @" runtime, which may not be compatible with the CLR version hosted by MSI. " +
                    @"The incompatibility is particularly possible for the EmbeddedUI scenarios. " +
                    @"The safest way to solve the problem is to compile the assembly for v3.5 Target Framework.");
            }
        }
    }
}



