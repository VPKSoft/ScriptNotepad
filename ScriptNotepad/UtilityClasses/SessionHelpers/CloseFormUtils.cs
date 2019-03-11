using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptNotepad.UtilityClasses.SessionHelpers
{
    /// <summary>
    /// A utility class to close all forms of the software in case the session is ending.
    /// </summary>
    public static class CloseFormUtils
    {
        /// <summary>
        /// Closes all the forms of the application except the ones which type matches the given type <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type of the form to be left open.</param>
        public static void CloseOpenForms(Type type)
        {
            // create a list of forms to be closed..
            List<Form> forms = new List<Form>();

            // set the contents for the lost of forms to be closed..
            foreach (Form form in Application.OpenForms)
            {
                // ..with the exception..
                if (form.GetType() != type)
                {
                    forms.Add(form);
                }
            }

            // loop through the list of forms to be closed.. 
            for (int i = 0; i < forms.Count; i++)
            {
                // ..and close them..
                forms[i].Close();
            }
        }

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
            for (int i = 0; i < forms.Count; i++)
            {
                // ..and close them..
                forms[i].Close();
            }
        }
    }
}
