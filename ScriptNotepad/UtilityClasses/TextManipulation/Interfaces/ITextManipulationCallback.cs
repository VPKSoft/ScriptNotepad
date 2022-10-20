namespace ScriptNotepad.UtilityClasses.TextManipulation.Interfaces;

interface ITextManipulationCallback: IMethodName
{
    /// <summary>
    /// Gets or sets the callback action.
    /// </summary>
    /// <value>The callback action.</value>
    Action CallbackAction { get; set; }
}