#region License
/*
MIT License

Copyright(c) 2020 Petteri Kautonen

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

using System.Windows.Forms;
using ScintillaNET;
using VPKSoft.LangLib;

namespace ScriptNotepad.UtilityClasses.MenuHelpers;

/// <summary>
/// A class to add to the <see cref="Scintilla"/> context menu a style mark selection sub menu.
/// </summary>
public class ContextMenuStyles
{
    /// <summary>
    /// Creates the style menu.
    /// </summary>
    /// <param name="menuStrip">The menu strip to create the style menu to.</param>
    /// <param name="menuClickMark">The event handler to mark text using a style.</param>
    /// <param name="menuClickClearMark">The event handler to clear a style mark.</param>
    /// <param name="menuClearAllMarks">The event handler to clear all style marks.</param>
    public static void CreateStyleMenu(ContextMenuStrip menuStrip, EventHandler menuClickMark,
        EventHandler menuClickClearMark, EventHandler menuClearAllMarks)
    {
        // add the style menu to the given context menu LOCALIZED..
        menuStrip.Items.Add(new ToolStripSeparator());

        var toolStripDropDown = new ToolStripMenuItem(
            DBLangEngine.GetStatMessage("msgStyleSet",
                "Style|A menu item text to indicate marking / un-marking text using some style"));

        menuStrip.Items.Add(toolStripDropDown);

        toolStripDropDown.DropDownItems.Add(new ToolStripMenuItem(
            DBLangEngine.GetStatMessage("msgStyleClearAll",
                "Clear all styles|A menu item text to indicate clearing all style marks"), null,
            menuClearAllMarks));

        toolStripDropDown.DropDownItems.Add(new ToolStripSeparator());

        toolStripDropDown.DropDownItems.Add(new ToolStripMenuItem(
            DBLangEngine.GetStatMessage("msgStyleSet1",
                "Mark using the first style|A menu item text to indicate marking text using style 1"), null,
            menuClickMark) {Tag = "9", });

        toolStripDropDown.DropDownItems.Add(new ToolStripMenuItem(
            DBLangEngine.GetStatMessage("msgStyleSet2",
                "Mark using the seconds style|A menu item text to indicate marking text using style 2"), null,
            menuClickMark) {Tag = "10", });

        toolStripDropDown.DropDownItems.Add(new ToolStripMenuItem(
            DBLangEngine.GetStatMessage("msgStyleSet3",
                "Mark using the third style|A menu item text to indicate marking text using style 3"), null,
            menuClickMark) {Tag = "11", });

        toolStripDropDown.DropDownItems.Add(new ToolStripMenuItem(
            DBLangEngine.GetStatMessage("msgStyleSet4",
                "Mark using the fourth style|A menu item text to indicate marking text using style 4"), null,
            menuClickMark) {Tag = "12", });

        toolStripDropDown.DropDownItems.Add(new ToolStripMenuItem(
            DBLangEngine.GetStatMessage("msgStyleSet5",
                "Mark using the fifth style|A menu item text to indicate marking text using style 5"), null,
            menuClickMark) {Tag = "13", });

        toolStripDropDown.DropDownItems.Add(new ToolStripSeparator());

        toolStripDropDown.DropDownItems.Add(new ToolStripMenuItem(
            DBLangEngine.GetStatMessage("msgStyleClear1",
                "Clear style one|A menu item text to indicate clearing the text style 1 marks"), null,
            menuClickClearMark) {Tag = "9", });

        toolStripDropDown.DropDownItems.Add(new ToolStripMenuItem(
            DBLangEngine.GetStatMessage("msgStyleClear2",
                "Clear style two|A menu item text to indicate clearing the text style 2 marks"), null,
            menuClickClearMark) {Tag = "10", });

        toolStripDropDown.DropDownItems.Add(new ToolStripMenuItem(
            DBLangEngine.GetStatMessage("msgStyleClear3",
                "Clear style three|A menu item text to indicate clearing the text style 3 marks"), null,
            menuClickClearMark) {Tag = "11", });

        toolStripDropDown.DropDownItems.Add(new ToolStripMenuItem(
            DBLangEngine.GetStatMessage("msgStyleClear4",
                "Clear style four|A menu item text to indicate clearing the text style 4 marks"), null,
            menuClickClearMark) {Tag = "12", });

        toolStripDropDown.DropDownItems.Add(new ToolStripMenuItem(
            DBLangEngine.GetStatMessage("msgStyleClear5",
                "Clear style five|A menu item text to indicate clearing the text style 5 marks"), null,
            menuClickClearMark) {Tag = "13", });
    }
}