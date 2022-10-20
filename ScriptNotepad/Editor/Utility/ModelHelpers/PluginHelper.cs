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

using System.Reflection;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using ScriptNotepadPluginBase.PluginTemplateInterface;

namespace ScriptNotepad.Editor.Utility.ModelHelpers;

/// <summary>
/// A class to help with <see cref="Plugin"/> entities.
/// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
/// </summary>
/// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
public class PluginHelper: ErrorHandlingBase
{
    /// <summary>
    /// Gets a version string from a given <see cref="Assembly"/>.
    /// </summary>
    /// <param name="assembly">The assembly to set the version from.</param>
    internal static string VersionStringFromAssembly(Assembly assembly)
    {
        try
        {
            // return the version from the given assembly..
            return assembly.GetName().Version.ToString();
        }
        catch (Exception ex)
        {
            // log the exception..
            ExceptionLogAction?.Invoke(ex);

            // return a default value..
            return "1.0.0.0";
        }
    }

    /// <summary>
    /// Creates an instance of a <see cref="Plugin"/> class to be inserted into the database.
    /// </summary>
    /// <param name="assembly">The assembly of the plug-in.</param>
    /// <param name="plugin">The initialized plug-in.</param>
    /// <param name="fileNameFull">The full file name of the plug-in assembly.</param>
    /// <returns>A <see cref="Plugin"/> class instance based on the given arguments.</returns>
    public static Plugin FromPlugin(Assembly assembly, IScriptNotepadPlugin plugin, string fileNameFull)
    {
        try
        {
            // create a result based on the given parameters..
            var result = new Plugin
            {
                FileNameFull = fileNameFull,
                FileName = Path.GetFileName(fileNameFull),
                FilePath = Path.GetDirectoryName(fileNameFull),
                PluginName = plugin.PluginName,
                PluginDescription = plugin.PluginDescription,
                IsActive = true,
                PluginInstalled = DateTime.Now,
                PluginVersion = VersionStringFromAssembly(assembly),
            };

            // set the version for the plug-in..
            AssemblyVersion.SetPluginUpdated(result, assembly);

            // return the result..
            return result;
        }
        catch (Exception ex)
        {
            // log the exception..
            ExceptionLogAction?.Invoke(ex);
            return null;
        }
    }

    /// <summary>
    /// Updates the plug-in entry data.
    /// </summary>
    /// <param name="pluginEntry">The plug-in entry <see cref="Plugin"/>.</param>
    /// <param name="assembly">The assembly of the plug-in.</param>
    /// <param name="plugin">The initialized plug-in.</param>
    /// <param name="fileNameFull">The full file name of the plug-in assembly.</param>
    /// <returns>An updated <see cref="Plugin"/> class instance based on the given arguments.</returns>
    public static Plugin UpdateFromPlugin(Plugin pluginEntry, Assembly assembly, IScriptNotepadPlugin plugin, string fileNameFull)
    {
        try
        {
            pluginEntry.FileNameFull = fileNameFull;
            pluginEntry.FileName = Path.GetFileName(fileNameFull);
            pluginEntry.FilePath = Path.GetDirectoryName(fileNameFull);
            pluginEntry.PluginName = plugin.PluginName;
            pluginEntry.PluginDescription = plugin.PluginDescription;

            // set the version for the plug-in..
            AssemblyVersion.SetPluginUpdated(pluginEntry, assembly);

            return pluginEntry;
        }
        catch (Exception ex)
        {
            // log the exception..
            ExceptionLogAction?.Invoke(ex);
            return pluginEntry;
        }
    }

    /// <summary>
    /// Return data acquired from an assembly in case a plug-in failed to initialize.
    /// </summary>
    /// <param name="assembly">The assembly of the plug-in.</param>
    /// <param name="fileNameFull">The full file name of the plug-in assembly.</param>
    /// <returns>A <see cref="Plugin"/> class instance based on the given arguments.</returns>
    public static Plugin InvalidPlugin(Assembly assembly, string fileNameFull)
    {
        try
        {
            string description = string.Empty;
            if (assembly != null)
            {
                description = assembly.FullName;
            }

            // create a result based on the given parameters..
            var result = new Plugin
            {
                FileNameFull = fileNameFull,
                FileName = Path.GetFileName(fileNameFull),
                FilePath = Path.GetDirectoryName(fileNameFull),
                PluginName = "Unknown",
                PluginDescription = description,
                IsActive = false,
                PluginInstalled = DateTime.Now,
                LoadFailures = 1,
                PluginVersion = VersionStringFromAssembly(assembly),
            };

            // set the version for the plug-in..
            AssemblyVersion.SetPluginUpdated(result, assembly);

            // return the result..
            return result;
        }
        catch (Exception ex)
        {
            // log the exception..
            ExceptionLogAction?.Invoke(ex);
            return null;
        }
    }
}