namespace ScriptNotepad.UtilityClasses.TextManipulation
{
    /// <summary>
    /// A base class for all the simple text manipulation utility classes.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.TextManipulation.ITextManipulationCommand" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.TextManipulation.ITextManipulationCommand" />
    public abstract class TextManipulationCommandBase: ITextManipulationCommand
    {
        /// <summary>
        /// Manipulates the specified text value.
        /// </summary>
        /// <param name="value">The value to manipulate.</param>
        /// <returns>A string containing the manipulated text.</returns>
        public abstract string Manipulate(string value);

        /// <summary>
        /// Gets or sets the name of the method manipulating the text.
        /// </summary>
        /// <value>The name of the method manipulating the text.</value>
        public string MethodName { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public abstract override string ToString();
    }
}
