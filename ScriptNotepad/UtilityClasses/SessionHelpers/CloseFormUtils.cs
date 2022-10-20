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
using System.Windows.Forms;

namespace ScriptNotepad.UtilityClasses.SessionHelpers;

/// <summary>
/// A utility class to close all forms of the software in case the session is ending.
/// </summary>
public static class CloseFormUtils
{
    /// <summary>
    /// Closes all the open forms except the given form to exclude <paramref name="excludeForm"/>.
    /// </summary>
    /// <param name="excludeForm">The Form class instance to excluded from being closed.</param>
    public static void CloseOpenForms(Form excludeForm)
    {
        // create a list of forms to be closed..
        List<Form> forms = new List<Form>();

        // set the contents for the lost of forms to be closed..
        foreach (Form form in Application.OpenForms)
        {
            // ..with the exception o..
            if (!form.Equals(excludeForm))
            {
                forms.Add(form);
            }
        }

        // loop through the list of forms to be closed.. 
        // ReSharper disable once ForCanBeConvertedToForeach
        for (int i = 0; i < forms.Count; i++)
        {
            // ..and close them..
            forms[i].Close();
        }
    }
}