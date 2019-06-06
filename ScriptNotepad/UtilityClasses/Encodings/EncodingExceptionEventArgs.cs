using System;

namespace ScriptNotepad.UtilityClasses.Encodings
{
    /// <summary>
    /// Event arguments for the <see cref="DetectEncoding.ExceptionOccurred"/> event.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class EncodingExceptionEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the exception which occurred.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception { get; set; }
    }
}
