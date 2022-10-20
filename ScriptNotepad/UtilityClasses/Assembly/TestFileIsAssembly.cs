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

using ScriptNotepad.UtilityClasses.ErrorHandling;
using System.Reflection;

// (C): https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/assemblies-gac/how-to-determine-if-a-file-is-an-assembly
namespace ScriptNotepad.UtilityClasses.Assembly;

/// <summary>
/// A class to test if an given file is a .NET Framework assembly.
/// </summary>
public class TestFileIsAssembly: ErrorHandlingBase
{
    /// <summary>
    /// Determines whether the specified file is a .NET Framework assembly.
    /// </summary>
    /// <param name="fileName">The name of the file to test for.</param>
    /// <returns>True if the file is an assembly; otherwise false.</returns>
    public static bool IsAssembly(string fileName)
    {
        try
        {
            AssemblyName.GetAssemblyName(fileName);
            return true;
        }
        catch (FileNotFoundException ex)
        {
            // log the exception..
            ExceptionLogAction?.Invoke(ex);
            return false;
        }
        catch (BadImageFormatException ex)
        {
            // log the exception..
            ExceptionLogAction?.Invoke(ex);
            return false;
        }
        catch (FileLoadException ex)
        {
            // log the exception..
            ExceptionLogAction?.Invoke(ex);
            return false;
        }
        catch (Exception ex)
        {
            // log the exception..
            ExceptionLogAction?.Invoke(ex);
            return false;
        }
    }
}