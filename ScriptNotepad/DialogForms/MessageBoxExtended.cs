using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ScriptNotepad.DialogForms
{
    /// <summary>
    /// An extended message box class.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class MessageBoxExtended : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBoxExtended"/> class.
        /// </summary>
        public MessageBoxExtended()
        {
            InitializeComponent();
        }

        #region Localize
        /// <summary>
        /// Gets or sets the text for a No button. To property is for localization.
        /// </summary>
        public static string TextNo { get; set; } = "&No";

        /// <summary>
        /// Gets or sets the text for an OK button. To property is for localization.
        /// </summary>
        public static string TextOk { get; set; } = "&OK";

        /// <summary>
        /// Gets or sets the text for an Yes button. To property is for localization.
        /// </summary>
        public static string TextYes { get; set; } = "&Yes";

        /// <summary>
        /// Gets or sets the text for a Cancel button. To property is for localization.
        /// </summary>
        public static string TextCancel { get; set; } = "&Cancel";

        /// <summary>
        /// Gets or sets the text for a Retry button. To property is for localization.
        /// </summary>
        public static string TextRetry { get; set; } = "&Retry";

        /// <summary>
        /// Gets or sets the text for an Abort button. To property is for localization.
        /// </summary>
        public static string TextAbort { get; set; } = "&Abort";

        /// <summary>
        /// Gets or sets the text for an Yes to all button. To property is for localization.
        /// </summary>
        public static string TextYesToAll { get; set; } = "Yes &to all";

        /// <summary>
        /// Gets or sets the text for an Ignore button. To property is for localization.
        /// </summary>
        public static string TextIgnore { get; set; } = "&Ignore";

        /// <summary>
        /// Gets or sets the text for a check box asking whether to remember the given answer for the dialog. To property is for localization.
        /// </summary>
        public static string TextRemember { get; set; } = "&Remember answer";
        #endregion

        // Documentation: (©): Microsoft  (copy/paste) documentation whit modifications..
        /// <summary>
        /// Displays an extended message box in front of the specified object and with the specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window" /> that will own the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="T:ScriptNotepad.DialogForms.MessageBoxButtonsExtended" /> values that specifies which buttons to display in the message box. </param>
        /// <param name="icon">One of the <see cref="T:System.Drawing.Image" /> values that specifies which icon to display in the message box. </param>
        /// <param name="useMnemonic">A value indicating whether the first character that is preceded by an ampersand (&amp;) is used as the mnemonic key for the buttons within the dialog.</param>
        /// <returns>One of the <see cref="T:ScriptNotepad.DialogForms.DialogResultExtended" /> values.</returns>
        public static DialogResultExtended Show(IWin32Window owner, string text, string caption,
            MessageBoxButtonsExtended buttons, Image icon, bool useMnemonic)
        {
            var messageBoxExtended = new MessageBoxExtended();

            var dialogButtons = messageBoxExtended.CreateButtons(buttons);

            foreach (var dialogButton in dialogButtons)
            {
                messageBoxExtended.flpButtons.Controls.Add(dialogButton);
                dialogButton.UseMnemonic = useMnemonic; // set the (stupid) mnemonic value..
            }

            messageBoxExtended.cbRememberAnswer.Visible = messageBoxExtended.DisplayRememberBox;

            messageBoxExtended.cbRememberAnswer.Text = TextRemember;
            messageBoxExtended.cbRememberAnswer.Checked = messageBoxExtended.CheckRememberBox;
            messageBoxExtended.lbText.Text = text;
            messageBoxExtended.Text = caption;
            messageBoxExtended.pbMessageBoxIcon.Image = icon;

            if (owner == null)
            {
                messageBoxExtended.ShowDialog();
            }
            else
            {
                messageBoxExtended.ShowDialog(owner);
            }

            return messageBoxExtended.result;
        }

        // Documentation: (©): Microsoft  (copy/paste) documentation whit modifications..
        /// <summary>
        /// Displays an extended message box in front of the specified object and with the specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window" /> that will own the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="T:ScriptNotepad.DialogForms.MessageBoxButtonsExtended" /> values that specifies which buttons to display in the message box. </param>
        /// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
        /// <param name="useMnemonic">A value indicating whether the first character that is preceded by an ampersand (&amp;) is used as the mnemonic key for the buttons within the dialog.</param>
        /// <returns>One of the <see cref="T:ScriptNotepad.DialogForms.DialogResultExtended" /> values.</returns>
        public static DialogResultExtended Show(IWin32Window owner, string text, string caption,
            MessageBoxButtonsExtended buttons, MessageBoxIcon icon, bool useMnemonic)
        {
            return Show(owner, text, caption, buttons, GetMessageBoxIcon(icon), useMnemonic);
        }

        // Documentation: (©): Microsoft  (copy/paste) documentation whit modifications..
        /// <summary>
        /// Displays an extended message box in front of the specified object and with the specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window" /> that will own the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="T:ScriptNotepad.DialogForms.MessageBoxButtonsExtended" /> values that specifies which buttons to display in the message box. </param>
        /// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
        /// <returns>One of the <see cref="T:ScriptNotepad.DialogForms.DialogResultExtended" /> values.</returns>
        public static DialogResultExtended Show(IWin32Window owner, string text, string caption,
            MessageBoxButtonsExtended buttons, MessageBoxIcon icon)
        {
            return Show(owner, text, caption, buttons, GetMessageBoxIcon(icon), true);
        }

        // Documentation: (©): Microsoft  (copy/paste) documentation whit modifications..
        /// <summary>
        /// Displays an extended message box in front of the specified object and with the specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="T:ScriptNotepad.DialogForms.MessageBoxButtonsExtended" /> values that specifies which buttons to display in the message box. </param>
        /// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
        /// <param name="useMnemonic">A value indicating whether the first character that is preceded by an ampersand (&amp;) is used as the mnemonic key for the buttons within the dialog.</param>
        /// <returns>One of the <see cref="T:ScriptNotepad.DialogForms.DialogResultExtended" /> values.</returns>
        public static DialogResultExtended Show(string text, string caption, MessageBoxButtonsExtended buttons,
            MessageBoxIcon icon, bool useMnemonic)
        {
            return Show(null, text, caption, buttons, GetMessageBoxIcon(icon), useMnemonic);
        }

        // Documentation: (©): Microsoft  (copy/paste) documentation whit modifications..
        /// <summary>
        /// Displays an extended message box in front of the specified object and with the specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="T:ScriptNotepad.DialogForms.MessageBoxButtonsExtended" /> values that specifies which buttons to display in the message box. </param>
        /// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
        /// <returns>One of the <see cref="T:ScriptNotepad.DialogForms.DialogResultExtended" /> values.</returns>
        public static DialogResultExtended Show(string text, string caption, MessageBoxButtonsExtended buttons,
            MessageBoxIcon icon)
        {
            return Show(null, text, caption, buttons, GetMessageBoxIcon(icon), true);
        }

        // Documentation: (©): Microsoft  (copy/paste) documentation whit modifications..
        /// <summary>
        /// Displays an extended message box in front of the specified object and with the specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="T:ScriptNotepad.DialogForms.MessageBoxButtonsExtended" /> values that specifies which buttons to display in the message box. </param>
        /// <param name="icon">One of the <see cref="T:System.Drawing.Image" /> values that specifies which icon to display in the message box. </param>
        /// <param name="useMnemonic">A value indicating whether the first character that is preceded by an ampersand (&amp;) is used as the mnemonic key for the buttons within the dialog.</param>
        /// <returns>One of the <see cref="T:ScriptNotepad.DialogForms.DialogResultExtended" /> values.</returns>
        public static DialogResultExtended Show(string text, string caption,
            MessageBoxButtonsExtended buttons, Image icon, bool useMnemonic)
        {
            return Show(null, text, caption, buttons, icon, useMnemonic);
        }

        // Documentation: (©): Microsoft  (copy/paste) documentation whit modifications..
        /// <summary>
        /// Displays an extended message box in front of the specified object and with the specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="T:ScriptNotepad.DialogForms.MessageBoxButtonsExtended" /> values that specifies which buttons to display in the message box. </param>
        /// <param name="icon">One of the <see cref="T:System.Drawing.Image" /> values that specifies which icon to display in the message box. </param>
        /// <returns>One of the <see cref="T:ScriptNotepad.DialogForms.DialogResultExtended" /> values.</returns>
        public static DialogResultExtended Show(string text, string caption,
            MessageBoxButtonsExtended buttons, Image icon)
        {
            return Show(null, text, caption, buttons, icon, true);
        }


        private DialogResultExtended result = DialogResultExtended.None;

        private bool DisplayRememberBox { get; set; }

        private bool CheckRememberBox { get; set; }

        private List<Button> CreateButtons(MessageBoxButtonsExtended buttons)
        {
            List<Button> uiButtons = new List<Button>();

            Button button;

            switch (buttons)
            {
                case MessageBoxButtonsExtended.OK:
                    button = new Button{ Text = TextOk};
                    button.Click += delegate
                        { result = DialogResultExtended.OK; Close(); };
                    uiButtons.Add(button); 
                    break;

                case MessageBoxButtonsExtended.AbortRetryIgnore:
                    button = new Button{ Text = TextAbort};
                    button.Click += delegate
                        { result = DialogResultExtended.Abort; Close(); };
                    uiButtons.Add(button); 

                    button = new Button{ Text = TextRetry};
                    button.Click += delegate
                        { result = DialogResultExtended.Retry; Close(); };
                    uiButtons.Add(button); 

                    button = new Button{ Text = TextIgnore};
                    button.Click += delegate
                        { result = DialogResultExtended.Retry; Close(); };
                    uiButtons.Add(button); 
                    break;

                case MessageBoxButtonsExtended.OKCancel:
                    button = new Button{ Text = TextOk};
                    button.Click += delegate
                        { result = DialogResultExtended.OK; Close(); };
                    uiButtons.Add(button); 

                    button = new Button{ Text = TextCancel};
                    button.Click += delegate
                        { result = DialogResultExtended.Cancel; Close(); };
                    uiButtons.Add(button); 
                    break;
                
                case MessageBoxButtonsExtended.RetryCancel:
                    button = new Button{ Text = TextRetry};
                    button.Click += delegate
                        { result = DialogResultExtended.Retry; Close(); };
                    uiButtons.Add(button); 

                    button = new Button{ Text = TextCancel};
                    button.Click += delegate
                        { result = DialogResultExtended.Cancel; Close(); };
                    uiButtons.Add(button); 
                    break;

                case MessageBoxButtonsExtended.YesNo:
                    button = new Button{ Text = TextYes};
                    button.Click += delegate
                        { result = DialogResultExtended.Yes; Close(); };
                    uiButtons.Add(button); 

                    button = new Button{ Text = TextNo};
                    button.Click += delegate
                        { result = DialogResultExtended.No; Close(); };
                    uiButtons.Add(button); 
                    break;

                case MessageBoxButtonsExtended.YesNoCancel:
                    button = new Button{ Text = TextYes};
                    button.Click += delegate
                        { result = DialogResultExtended.Yes; Close(); };
                    uiButtons.Add(button); 

                    button = new Button{ Text = TextNo};
                    button.Click += delegate
                        { result = DialogResultExtended.No; Close(); };
                    uiButtons.Add(button); 

                    button = new Button{ Text = TextCancel};
                    button.Click += delegate
                        { result = DialogResultExtended.Cancel; Close(); };
                    uiButtons.Add(button); 
                    break;

                case MessageBoxButtonsExtended.YesNoYesToAll:
                    button = new Button{ Text = TextYes};
                    button.Click += delegate
                        { result = DialogResultExtended.Yes; Close(); };
                    uiButtons.Add(button); 

                    button = new Button{ Text = TextNo};
                    button.Click += delegate
                        { result = DialogResultExtended.No; Close(); };
                    uiButtons.Add(button); 

                    button = new Button{ Text = TextYesToAll};
                    button.Click += delegate
                        { result = DialogResultExtended.YesToAll; Close(); };
                    uiButtons.Add(button); 
                    break;

                case MessageBoxButtonsExtended.YesNoYesToAllRememberChecked:
                    button = new Button{ Text = TextYes};
                    button.Click += delegate
                        { result = DialogResultExtended.Yes; Close(); };
                    uiButtons.Add(button); 

                    button = new Button{ Text = TextNo};
                    button.Click += delegate
                        { result = DialogResultExtended.No; Close(); };
                    uiButtons.Add(button); 

                    button = new Button{ Text = TextYesToAll};
                    button.Click += delegate
                        { result = DialogResultExtended.YesToAll; Close(); };
                    uiButtons.Add(button);
                    DisplayRememberBox = true;
                    CheckRememberBox = true;
                    break;

                case MessageBoxButtonsExtended.YesNoYesToAllRemember:
                    button = new Button{ Text = TextYes};
                    button.Click += delegate
                        { result = DialogResultExtended.Yes; Close(); };
                    uiButtons.Add(button); 

                    button = new Button{ Text = TextNo};
                    button.Click += delegate
                        { result = DialogResultExtended.No; Close(); };
                    uiButtons.Add(button); 

                    button = new Button{ Text = TextYesToAll};
                    button.Click += delegate
                        { result = DialogResultExtended.YesToAll; Close(); };
                    uiButtons.Add(button);
                    DisplayRememberBox = true;
                    CheckRememberBox = false;
                    break;
            }

            return uiButtons;
        }

        /// <summary>
        /// Gets the message box icon based on the given <see cref="MessageBoxIcon"/> enumeration value.
        /// </summary>
        /// <param name="icon">The icon enumeration value.</param>
        /// <returns>An <see cref="Image"/> instance representing the icon or an empty image if the icon wasn't found.</returns>
        public static Image GetMessageBoxIcon(MessageBoxIcon icon)
        {
            // store the size to take higher system DPI setting into account,
            // don't know if size changes though..
            var size = SystemIcons.Asterisk.Size;
            switch (icon)
            {
                case MessageBoxIcon.Asterisk: return SystemIcons.Asterisk.ToBitmap();
                case MessageBoxIcon.Exclamation: return SystemIcons.Exclamation.ToBitmap();
                case MessageBoxIcon.Hand: return SystemIcons.Hand.ToBitmap();
                case MessageBoxIcon.None: return new Bitmap(size.Width, size.Height);
                case MessageBoxIcon.Question: return SystemIcons.Question.ToBitmap();
            }
            return new Bitmap(size.Width, size.Height);
        }

        private void MessageBoxExtended_Shown(object sender, EventArgs e)
        {
            
        }
    }

    // Documentation: (©): Microsoft  (copy/paste) documentation whit modifications..

#pragma warning disable CS0419 // Ambiguous reference in cref attribute
#pragma warning disable 1574
    /// <summary>
    /// An enumeration for the <see cref="MessageBoxExtended.Show"/> method's return value.
    /// </summary>
    [ComVisible(true)]
#pragma warning restore 1574
#pragma warning restore CS0419 // Ambiguous reference in cref attribute
    public enum DialogResultExtended
    {
        /// <summary>
        /// <see langword="Nothing" /> is returned from the dialog box.
        /// This means that the modal dialog continues running.
        /// </summary>
        None,

        /// <summary>
        /// The dialog box return value is <see langword="OK" /> (usually sent from a button labeled OK).
        /// </summary>
        // ReSharper disable once InconsistentNaming
        OK,

        /// <summary>
        /// The dialog box return value is <see langword="Cancel" /> (usually sent from a button labeled Cancel).
        /// </summary>
        Cancel,

        /// <summary>
        /// The dialog box return value is <see langword="Abort" /> (usually sent from a button labeled Abort).
        /// </summary>
        Abort,

        /// <summary>
        /// The dialog box return value is <see langword="Retry" /> (usually sent from a button labeled Retry).
        /// </summary>
        Retry,

        /// <summary>
        /// The dialog box return value is <see langword="Ignore" /> (usually sent from a button labeled Ignore).
        /// </summary>
        Ignore,

        /// <summary>
        /// The dialog box return value is <see langword="Yes" /> (usually sent from a button labeled Yes).
        /// </summary>
        Yes,
        /// <summary>
        /// The dialog box return value is <see langword="No" /> (usually sent from a button labeled No).
        /// </summary>
        No,

        /// <summary>
        /// The dialog box return value is <see langword="YesToAll" /> (usually sent from a button labeled YesToAll).
        /// </summary>
        YesToAll,

        /// <summary>
        /// The dialog box return value is <see langword="YesToAllRemember" /> (usually sent from a button labeled YesToAll) with the remember check box checked.
        /// </summary>
        YesToAllRemember,
    }

    // Documentation: (©): Microsoft  (copy/paste) documentation whit modifications..
    /// <summary>
    /// An enumeration for the buttons for the <see cref="MessageBoxExtended"/> dialog.
    /// </summary>
    [Flags]
    public enum MessageBoxButtonsExtended
    {
        /// <summary>
        /// The message box contains an OK button.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        OK,

        /// <summary>
        /// The message box contains OK and Cancel buttons.
        /// </summary>
        /// 
        // ReSharper disable once InconsistentNaming
        OKCancel,

        /// <summary>
        /// The message box contains Abort, Retry, and Ignore buttons.
        /// </summary>
        AbortRetryIgnore,

        /// <summary>
        /// The message box contains Yes, No, and Cancel buttons.
        /// </summary>
        YesNoCancel,

        /// <summary>
        /// The message box contains Yes and No buttons.
        /// </summary>
        YesNo,

        /// <summary>
        /// The message box contains Retry and Cancel buttons.
        /// </summary>
        RetryCancel,

        /// <summary>
        /// The message box contains Yes, No and Yes to all buttons.
        /// </summary>
        YesNoYesToAll,

        /// <summary>
        /// The message box contains Yes, No and Yes to all (with remember choice check box unchecked) buttons.
        /// </summary>
        YesNoYesToAllRemember,

        /// <summary>
        /// The message box contains Yes, No and Yes to all (with remember choice check box checked) buttons.
        /// </summary>
        YesNoYesToAllRememberChecked,
    }
}
