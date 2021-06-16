using System;
using Newtonsoft.Json.Linq;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using ScriptNotepad.UtilityClasses.TextManipulation.BaseClasses;

namespace ScriptNotepad.UtilityClasses.TextManipulation.Json
{
    /// <summary>
    /// A class to convert single-line JSON to formatted JSON.
    /// Implements the <see cref="TextManipulationCommandBase" />
    /// </summary>
    /// <seealso cref="TextManipulationCommandBase" />
    public class JsonMultilineConvert: TextManipulationCommandBase
    {
        /// <summary>
        /// Manipulates the specified text value.
        /// </summary>
        /// <param name="value">The value to manipulate.</param>
        /// <returns>A string containing the manipulated text.</returns>
        public override string Manipulate(string value)
        {
            try
            {
                var result = JToken.Parse(value).ToString();

                return result; 
            }
            catch (Exception ex)
            {
                ErrorHandlingBase.ExceptionLogAction?.Invoke(ex);
                return value; 
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return MethodName;
        }
    }
}
