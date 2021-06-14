using VPKSoft.LangLib;

namespace ScriptNotepad.Localization
{
    /// <summary>
    /// A class to provide static localized messages from the <see cref="DBLangEngine"/>.
    /// Implements the <see cref="ScriptNotepad.Localization.IStaticMessageProvider" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.Localization.IStaticMessageProvider" />
    public class StaticMessageLocalizationProvider: IStaticMessageProvider
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="StaticMessageLocalizationProvider"/> class from being created.
        /// </summary>
        private StaticMessageLocalizationProvider()
        {
            Instance = this;
        }

        /// <summary>
        /// Gets or sets the of this class.
        /// </summary>
        /// <value>The instance.</value>
        public static StaticMessageLocalizationProvider Instance { get; set; } = new();

        /// <summary>
        /// Gets the message by the specified name localized to the current software culture setting.
        /// </summary>
        /// <param name="messageName">Name of the message.</param>
        /// <param name="defaultMessage">The default message to fall back into if a localized message was not found.</param>
        /// <returns>A localized value for the specified message name.</returns>
        public string GetMessage(string messageName, string defaultMessage)
        {
            return DBLangEngine.GetStatMessage(messageName, defaultMessage);
        }
    }
}
