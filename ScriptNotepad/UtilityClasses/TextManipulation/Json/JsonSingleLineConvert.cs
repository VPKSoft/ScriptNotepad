﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using ScriptNotepad.UtilityClasses.TextManipulation.BaseClasses;
using ScriptNotepad.UtilityClasses.TextManipulation.Interfaces;

namespace ScriptNotepad.UtilityClasses.TextManipulation.Json
{
    /// <summary>
    /// A class to convert formatted JSON to a single-line JSOn.
    /// Implements the <see cref="ITextManipulationCommand" />
    /// </summary>
    /// <seealso cref="ITextManipulationCommand" />
    public class JsonSingleLineConvert: TextManipulationCommandBase
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
                var result = JToken.Parse(value).ToString(Formatting.None);

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
