using ScriptNotepad.UtilityClasses.TextManipulation.Interfaces;

namespace ScriptNotepad.UtilityClasses.TextManipulation.BaseClasses
{
    /// <summary>
    /// Class TextManipulationCallbackBase.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.TextManipulation.Interfaces.ITextManipulationCallback" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.TextManipulation.Interfaces.ITextManipulationCallback" />
    public class TextManipulationCallbackBase: ITextManipulationCallback
    {
        /// <summary>
        /// Gets or sets the callback action.
        /// </summary>
        /// <value>The callback action.</value>
        public Action CallbackAction { get; set; }

        /// <summary>
        /// Gets or sets the name of the method manipulating the text.
        /// </summary>
        /// <value>The name of the method manipulating the text.</value>
        public string MethodName { get; set; }

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
