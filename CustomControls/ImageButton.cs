#region License
/*
MIT License

Copyright(c) 2022 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CustomControls;

/// <summary>
/// A custom button control with an image and a label.
/// Implements the <see cref="System.Windows.Forms.UserControl" />
/// </summary>
/// <seealso cref="System.Windows.Forms.UserControl" />
[DefaultEvent(nameof(Click))]
public partial class ImageButton : UserControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ImageButton"/> class.
    /// </summary>
    public ImageButton()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Gets or sets the button image.
    /// </summary>
    /// <value>The button image.</value>
    [Category("Appearance")]
    [Description("The button image.")]
    public Image ButtonImage { get => pnImage.BackgroundImage; set => pnImage.BackgroundImage = value; }

    /// <summary>
    /// Gets or sets the button image layout.
    /// </summary>
    /// <value>The button image layout.</value>
    [Category("Appearance")]
    [Description("The button image layout.")]
    public ImageLayout ButtonImageLayout { get => pnImage.BackgroundImageLayout; set => pnImage.BackgroundImageLayout = value; }

    /// <summary>
    /// Gets or sets the text associated with this control.
    /// </summary>
    /// <value>The text.</value>
    [Category("Appearance")]
    [Description("The text associated with this control.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    public override string Text { get => lbButtonText.Text; set => lbButtonText.Text = value; }

    /// <summary>
    /// Occurs when the control is clicked.
    /// </summary>
    [Category("Behaviour")]
    [Description("The text associated with this control.")]
    // ReSharper disable once InconsistentNaming
    public new EventHandler Click;

    /// <summary>
    /// Delegates the <see cref="Click"/> event to the base control.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void DelegateClick(object sender, EventArgs e)
    {
        Click?.Invoke(this, EventArgs.Empty);
    }
}