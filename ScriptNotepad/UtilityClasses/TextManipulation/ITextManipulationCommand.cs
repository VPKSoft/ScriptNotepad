namespace ScriptNotepad.UtilityClasses.TextManipulation
{
    /// <summary>
    /// An interface to manipulate text with classes implementing this interface.
    /// </summary>
    public interface ITextManipulationCommand
    {
        /// <summary>
        /// Manipulates the specified text value.
        /// </summary>
        /// <param name="value">The value to manipulate.</param>
        /// <returns>A string containing the manipulated text.</returns>
        string Manipulate(string value);

        /// <summary>
        /// Gets or sets the name of the method manipulating the text.
        /// </summary>
        /// <value>The name of the method manipulating the text.</value>
        string MethodName { get; set; }
    }
}
