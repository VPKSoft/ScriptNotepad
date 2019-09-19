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
using System.Drawing;
using System.Windows.Forms;
using VPKSoft.LangLib;

namespace ScriptNotepad.DialogForms
{
    /// <summary>
    /// A dialog to query one or two numbers from the user.
    /// Implements the <see cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormDialogQueryNumber : DBLangEngineWinforms
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogQueryNumber"/> class.
        /// </summary>
        public FormDialogQueryNumber()
        {
            InitializeComponent();

            DBLangEngine.DBName = "lang.sqlite"; // Do the VPKSoft.LangLib == translation..

            if (Utils.ShouldLocalize() != null)
            {
                DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
                return; // After localization don't do anything more..
            }

            // initialize the language/localization database..
            DBLangEngine.InitializeLanguage("ScriptNotepad.Localization.Messages");
        }

        /// <summary>
        /// Displays the dialog with given range values and initial values.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="startValue">The start value for the first <see cref="NumericUpDown"/>.</param>
        /// <param name="startValueMin">The minimum value for the first <see cref="NumericUpDown"/>.</param>
        /// <param name="startValueMax">The maximum value for the first <see cref="NumericUpDown"/>.</param>
        /// <param name="endValue">The start value for the second <see cref="NumericUpDown"/>.</param>
        /// <param name="endValueMin">The minimum value for the second <see cref="NumericUpDown"/>.</param>
        /// <param name="endValueMax">The maximum value for the second <see cref="NumericUpDown"/>.</param>
        /// <param name="title">The title to of the dialog.</param>
        /// <param name="valueDescription">A text for the label to describe the value requested from the user.</param>
        /// <returns>System.ValueTuple&lt;T, T&gt;.</returns>
        public static (T, T) Execute<T>(int startValue, int startValueMin, int startValueMax, int endValue,
            int endValueMin,
            int endValueMax, string title, string valueDescription)
        {
            // create a new dialog and set the parameter values for it..
            FormDialogQueryNumber dialog = new FormDialogQueryNumber
            {
                nudValueStart = {Maximum = startValueMax, Minimum = startValueMin, Value = startValue},
                nudValueEnd = {Maximum = endValueMax, Minimum = endValueMin, Value = endValue},
                Text = title,
                lbFunctionDescription = {Text = valueDescription}
            };

            // if only one value is requested, disable the other NumericUpDown..
            if (endValue == -1 && endValueMin == -1 && endValueMax == -1)
            {
                dialog.nudValueStart.Size = new Size(dialog.nudValueEnd.Right - dialog.nudValueStart.Left,
                    dialog.nudValueStart.Height);
                dialog.lbDelimiter.Visible = false;
                dialog.nudValueEnd.Visible = false;
            }

            // if the user accepted the input, return the values..
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var v1 = (T) Convert.ChangeType(dialog.nudValueStart.Value, typeof(T));
                var v2 = (T) Convert.ChangeType(dialog.nudValueEnd.Value, typeof(T));
                return (v1, v2);
            }

            // user didn't accept the input so return default..
            return default;
        }

        /// <summary>
        /// Displays the dialog with given range values and initial values.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="startValueMin">The minimum value for the first <see cref="NumericUpDown"/>.</param>
        /// <param name="startValueMax">The maximum value for the first <see cref="NumericUpDown"/>.</param>
        /// <param name="endValueMin">The minimum value for the second <see cref="NumericUpDown"/>.</param>
        /// <param name="endValueMax">The maximum value for the second <see cref="NumericUpDown"/>.</param>
        /// <param name="title">The title to of the dialog.</param>
        /// <param name="valueDescription">A text for the label to describe the value requested from the user.</param>
        /// <returns>System.ValueTuple&lt;T, T&gt;.</returns>
        public static (T, T) Execute<T>(int startValueMin, int startValueMax,
            int endValueMin,
            int endValueMax, string title, string valueDescription)
        {
            return Execute<T>(startValueMin, startValueMin, startValueMax, endValueMin, endValueMin, endValueMax, title,
                valueDescription);
        }

        /// <summary>
        /// Displays the dialog with given range values and initial values.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="startValueMax">The maximum value for the first <see cref="NumericUpDown"/>.</param>
        /// <param name="endValueMax">The maximum value for the second <see cref="NumericUpDown"/>.</param>
        /// <param name="title">The title to of the dialog.</param>
        /// <param name="valueDescription">A text for the label to describe the value requested from the user.</param>
        /// <returns>System.ValueTuple&lt;T, T&gt;.</returns>
        public static (T, T) ExecuteStartValueZero<T>(int startValueMax,            
            int endValueMax, string title, string valueDescription)
        {
            return Execute<T>(0, 0, startValueMax, 0, 0, endValueMax, title,
                valueDescription);
        }

        /// <summary>
        /// Displays the dialog with given range values and initial values.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="startValue">The start value for the first <see cref="NumericUpDown"/>.</param>
        /// <param name="startValueMin">The minimum value for the first <see cref="NumericUpDown"/>.</param>
        /// <param name="startValueMax">The maximum value for the first <see cref="NumericUpDown"/>.</param>
        /// <param name="title">The title to of the dialog.</param>
        /// <param name="valueDescription">A text for the label to describe the value requested from the user.</param>
        /// <returns>T.</returns>
        public static T Execute<T>(int startValue, int startValueMin, int startValueMax,
            string title, string valueDescription)
        {
            return Execute<T>(startValue, startValueMin, startValueMax, -1, -1, -1, title,
                valueDescription).Item1;
        }

        /// <summary>
        /// Displays the dialog with given range values and initial values.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="startValueMin">The minimum value for the first <see cref="NumericUpDown"/>.</param>
        /// <param name="startValueMax">The maximum value for the first <see cref="NumericUpDown"/>.</param>
        /// <param name="title">The title to of the dialog.</param>
        /// <param name="valueDescription">A text for the label to describe the value requested from the user.</param>
        /// <returns>T.</returns>
        public static T Execute<T>(int startValueMin, int startValueMax,
            string title, string valueDescription)
        {
            return Execute<T>(startValueMin, startValueMin, startValueMax, -1, -1, -1, title,
                valueDescription).Item1;
        }

        /// <summary>
        /// Displays the dialog with given range values and initial values.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="startValueMax">The maximum value for the first <see cref="NumericUpDown"/>.</param>
        /// <param name="title">The title to of the dialog.</param>
        /// <param name="valueDescription">A text for the label to describe the value requested from the user.</param>
        /// <returns>T.</returns>
        public static T Execute<T>(int startValueMax,
            string title, string valueDescription)
        {
            return Execute<T>(0, 0, startValueMax, -1, -1, -1, title,
                valueDescription).Item1;
        }
    }
}
