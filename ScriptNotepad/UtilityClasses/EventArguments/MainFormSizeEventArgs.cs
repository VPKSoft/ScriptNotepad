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

using System.Drawing;
using System.Windows.Forms;

namespace ScriptNotepad.UtilityClasses.EventArguments;

/// <summary>
/// Event arguments for the <see cref="FormMain.SizeVisibilityChange"/> event.
/// Implements the <see cref="System.EventArgs" />
/// </summary>
/// <seealso cref="System.EventArgs" />
public class MainFormSizeEventArgs: System.EventArgs
{
    /// <summary>
    /// Gets or sets the size of the <see cref="FormMain"/> instance.
    /// </summary>
    /// <value>The size of the <see cref="FormMain"/> instance.</value>
    public Size Size { get; set; }

    /// <summary>
    /// Gets or sets the previous size of the <see cref="FormMain"/> instance.
    /// </summary>
    /// <value>The previous size of the <see cref="FormMain"/> instance.</value>
    public Size PreviousSize { get; set; }

    /// <summary>
    /// Gets or sets the location of the <see cref="FormMain"/> instance.
    /// </summary>
    /// <value>The location of the <see cref="FormMain"/> instance.</value>
    public Point Location { get; set; }

    /// <summary>
    /// Gets or sets the previous location of the <see cref="FormMain"/> instance.
    /// </summary>
    /// <value>The previous location of the <see cref="FormMain"/> instance.</value>
    public Point PreviousLocation { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="FormWindowState"/> state of the <see cref="FormMain"/> instance.
    /// </summary>
    /// <value>The <see cref="FormWindowState"/> state of the <see cref="FormMain"/> instance.</value>
    public FormWindowState State { get; set; }

    /// <summary>
    /// Gets or sets the previous <see cref="FormWindowState"/> state of the <see cref="FormMain"/> instance.
    /// </summary>
    /// <value>The previous <see cref="FormWindowState"/> state of the <see cref="FormMain"/> instance.</value>
    public FormWindowState PreviousState { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the <see cref="FormMain"/> instance is visible.
    /// </summary>
    /// <value><c>true</c> if the <see cref="FormMain"/> instance is visible; otherwise, <c>false</c>.</value>
    public bool Visible { get; set; }

    /// <summary>
    /// Gets the boundaries of the size and location of the the <see cref="FormMain"/> instance.
    /// </summary>
    /// <value>The boundaries of the size and location of the the <see cref="FormMain"/> instance.</value>
    public Rectangle Boundaries => new (Location.X, Location.Y, Size.Width, Size.Height);

    /// <summary>
    /// Gets the boundaries of the previous size and location of the the <see cref="FormMain"/> instance.
    /// </summary>
    /// <value>The boundaries of the previous size and location of the the <see cref="FormMain"/> instance.</value>
    public Rectangle PreviousBoundaries => new (PreviousLocation.X, PreviousLocation.Y, PreviousSize.Width, PreviousSize.Height);
}