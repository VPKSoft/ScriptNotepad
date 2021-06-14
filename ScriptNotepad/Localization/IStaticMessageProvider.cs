using VPKSoft.LangLib;

namespace ScriptNotepad.Localization
{
    /// <summary>
    /// An interface providing access to the <see cref="DBLangEngine"/> messages.
    /// </summary>
    public interface IStaticMessageProvider
    {
        /// <summary>
        /// Gets the message by the specified name localized to the current software culture setting.
        /// </summary>
        /// <param name="messageName">Name of the message.</param>
        /// <param name="defaultMessage">The default message to fall back into if a localized message was not found.</param>
        /// <returns>A localized value for the specified message name.</returns>
        string GetMessage(string messageName, string defaultMessage);
    }
}
