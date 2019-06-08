#region License
/*
MIT License

Copyright(c) 2019 Petteri Kautonen

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

using ScriptNotepad.UtilityClasses.SearchAndReplace.Misc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ScriptNotepad.Settings;
using ScriptNotepad.UtilityClasses.Keyboard;
using VPKSoft.LangLib;
using VPKSoft.PosLib;

namespace ScriptNotepad.UtilityClasses.SearchAndReplace
{
    /// <summary>
    /// A form the to display search results if multiple findings are to be reported. 
    /// Implements the <see cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormSearchResultTree : DBLangEngineWinforms
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormSearchResultTree"/> class.
        /// </summary>
        public FormSearchResultTree()
        {
            // Add this form to be positioned..
            PositionForms.Add(this);

            // add positioning..
            PositionCore.Bind();

            InitializeComponent();
            
            DBLangEngine.DBName = "lang.sqlite"; // Do the VPKSoft.LangLib == translation..

            if (Utils.ShouldLocalize() != null)
            {
                DBLangEngine.InitalizeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
                return; // After localization don't do anything more..
            }

            // initialize the language/localization database..
            DBLangEngine.InitalizeLanguage("ScriptNotepad.Localization.Messages");

            ttMain.SetToolTip(pnPreviousResult,
                DBLangEngine.GetMessage("msgPreviousResult",
                    "Previous result|A tool-tip message describing that the button would go to the previous search result"));

            ttMain.SetToolTip(pnNextResult,
                DBLangEngine.GetMessage("msgNextResult",
                    "Advance result|A tool-tip message describing that the button would go to the next search result"));
            
            ttMain.SetToolTip(pnClose,
                DBLangEngine.GetMessage("msgButtonClose",
                    "Close|A message describing a tool-tip for a button which would close something"));

            // don't allow multiple instances of this..
            if (PreviousInstance != null)
            {
                if (PreviousInstance.IsDocked)
                {
                    RequestDockReleaseMainForm?.Invoke(PreviousInstance, new EventArgs());
                }
                else
                {
                    PreviousInstance?.Close();                   
                }
            }

            // save this as the new previous instance..
            PreviousInstance = this;
        }

        /// <summary>
        /// Gets or sets the previous instance of this class.
        /// </summary>
        public static FormSearchResultTree PreviousInstance { get; set; }

        // the search results for the tree view..
        private IEnumerable<(string fileName, int lineNumber, int startLocation, int length, string lineContent, bool isFileOpen)>
            searchResults;

        /// <summary>
        /// Gets or sets the search results for the tree view.
        /// </summary>
        public IEnumerable<(string fileName, int lineNumber, int startLocation, int length, string lineContents, bool isFileOpen)> SearchResults
        {
            // just return the value..
            get => searchResults;

            set
            {
                // value changed..
                if (!Equals(value, searchResults))
                {
                    // save the value..
                    searchResults = value;

                    // build the tree view..
                    CreateTree();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether not to perform owner drawing for the <seealso cref="TreeView"/> control.
        /// </summary>
        /// <value><c>true</c> if the owner drawing is disabled; otherwise, <c>false</c>.</value>
        private bool NoDraw { get; set; }

        /// <summary>
        /// Creates the tree view based on the <seealso cref="SearchResults"/> property value.
        /// </summary>
        private void CreateTree()
        {
            SuspendLayout(); // suspend the layout of the form..
            tvMain.BeginUpdate(); // the tree can be large, so suspend the draw..
            NoDraw = true;
            tvMain.Nodes.Clear(); // clear the previous nodes..
            string fileName = string.Empty;

            // initialize a variable for the 0-level node (a file)..
            TreeNode upperNode = null;
           
            // loop through the search results..
            foreach (var searchResult in SearchResults)
            {
                // if the file name in the search result has changed, create a new primary node for the file..
                if (fileName != searchResult.fileName)
                {
                    int count = searchResults.Count(f => f.fileName == searchResult.fileName);
                    // create the primary node..
                    upperNode = tvMain.Nodes.Add(searchResult.fileName,
                        DBLangEngine.GetMessage("msgFileNameMatchCount",
                            "File: '{0}', matches: {1}|A message describing a file name and a count number of search matches",
                            Path.GetFileName(searchResult.fileName), count), 0);

                    upperNode.Tag = searchResult; // save the result to the tag..

                    // save the file name so the comparison of the file name change can be done..
                    fileName = searchResult.fileName;
                }

                // add the search result to the primary node..
                var subNode = upperNode?.Nodes.Add(searchResult.fileName, DBLangEngine.GetMessage("msgSearchResultLine",
                    "Line: {0}, contents '{1}'.|A message describing a search result with a line number and the line's contents.",
                    searchResult.lineNumber, searchResult.lineContents), 1, 1);

                // save the file name so the comparison of the file name change can be done..
                if (subNode != null)
                {
                    subNode.Tag = searchResult;
                }
            }
            NoDraw = false;
            tvMain.EndUpdate(); // END: the tree can be large, so suspend the draw..
            ResumeLayout(); // END: suspend the layout of the form..
        }

        /// <summary>
        /// Selects the next occurrence within the tree view.
        /// </summary>
        public void NextOccurrence()
        {
            SelectNode(tvMain, true);
        }

        /// <summary>
        /// Selects the previous occurrence within the tree view.
        /// </summary>
        public void PreviousOccurrence()
        {
            SelectNode(tvMain, false);
        }

        /// <summary>
        /// Closes the tree view and its form.
        /// </summary>
        public void CloseTree()
        {
            if (IsDocked)
            {
                RequestDockReleaseMainForm?.Invoke(this, new EventArgs());
            }
            else
            {
                Close();
            }
        }

        /// <summary>
        /// A delegate for the SearchResultSelected event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="SearchResultTreeViewClickEventArgs"/> instance containing the event data.</param>
        public delegate void OnSearchResultClick(object sender, SearchResultTreeViewClickEventArgs e);

        /// <summary>
        /// Occurs when a search result was clicked.
        /// </summary>
        public static event OnSearchResultClick SearchResultSelected;

        /// <summary>
        /// Gets or sets the an event handler if the form requests to dock in the main form.
        /// </summary>
        public static event EventHandler RequestDockMainForm;

        /// <summary>
        /// Gets or sets the an event handler if the form requests the docking to the main form to be released.
        /// </summary>
        public static event EventHandler RequestDockReleaseMainForm;

        // the form is requesting to dock in to the main form..
        private void FormSearchResultTree_Shown(object sender, EventArgs e)
        {
            // if the setting value is true then..
            if (FormSettings.Settings.DockSearchTreeForm)
            {
                // ..raise the event if subscribed..
                RequestDockMainForm?.Invoke(this, new EventArgs());
            }
            else if (Height < Application.OpenForms[0].Height * 20 / 100)
            {
                Height = Application.OpenForms[0].Height * 20 / 100;
            }
        }

        // a flag indicating whether this instance is docked to the main form..
        private bool isDocked;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is docked to the main form.
        /// </summary>
        public bool IsDocked
        {
            get => isDocked;

            set
            {
                lbSearchResultDesc.Visible = value;
                pnClose.Visible = value;
                isDocked = value;
            }
        }

        /// <summary>
        /// Gets or sets the absolute index of the selected node within the three branches.
        /// </summary>
        private int SelectedNodeIndex { get; set; } = 1;

        /// <summary>
        /// Selects the previous or the next node of a tree view based on the given direction.
        /// </summary>
        /// <param name="treeView">An instance to <see cref="TreeView"/> class to be used with the method.</param>
        /// <param name="forward">if set to <c>true</c> the selection goes forward.</param>
        private void SelectNode(TreeView treeView, bool forward)
        {
            if (forward) // forward..
            {
                SelectNextNode(treeView);
            }
            else
            {
                // backward..
                SelectPreviousNode(treeView);
            }
        }

        /// <summary>
        /// Selects the next node within a tree view.
        /// </summary>
        /// <param name="treeView">The tree view which next node to select.</param>
        private void SelectNextNode(TreeView treeView)
        {
            int count = treeView.GetNodeCount(true);

            if (SelectedNodeIndex == count - 1)
            {
                SelectedNodeIndex = 1;
            }

            int counter = 0;
            for (int i = 0; i < treeView.Nodes.Count; i++)
            {
                for (int j = 0; j < treeView.Nodes[i].Nodes.Count; j++)
                {
                    counter++;

                    if (counter == SelectedNodeIndex)
                    {
                        treeView.SelectedNode = treeView.Nodes[i].Nodes[j];
                        SelectedNodeIndex++;
                        return;
                    }
                }
            }

            SelectedNodeIndex = 1;
            SelectNextNode(treeView);
        }

        /// <summary>
        /// Selects the previous node of a give <see cref="TreeView"/>.
        /// </summary>
        /// <param name="treeView">The tree view which previous node to select.</param>
        private void SelectPreviousNode(TreeView treeView)
        {
            int counter = treeView.GetNodeCount(true);

            for (int i = treeView.Nodes.Count - 1; i >= 0; i--)
            {
                for (int j = treeView.Nodes[i].Nodes.Count - 1; j >= 0; j--)
                {
                    if (counter == SelectedNodeIndex)
                    {
                        treeView.SelectedNode = treeView.Nodes[i].Nodes[j];
                        SelectedNodeIndex--;
                        return;
                    }

                    counter--;
                }
            }

            SelectedNodeIndex = treeView.GetNodeCount(true);
            SelectPreviousNode(treeView);
        }

        // a method to handle the "tiny" button clicks..
        private void TinyButton_Click(object sender, EventArgs e)
        {
            // the search result window is requested to be closed..
            if (sender.Equals(pnClose)) 
            {
                CloseTree();
            }
            // a user wishes to navigate to a next or to a previous search result..
            else if (sender.Equals(pnNextResult) || sender.Equals(pnPreviousResult))
            {
                SelectNode(tvMain, sender.Equals(pnNextResult));
            }
        }

        /// <summary>
        /// Finds the "true" index of a give <see cref="TreeNode"/> within a given <see cref="TreeView"/>.
        /// </summary>
        /// <param name="treeView">The tree view to which the <paramref name="node"/> belongs to.</param>
        /// <param name="node">The node which "true" index to get.</param>
        /// <returns>An index for the node if the operation was successful; otherwise false.</returns>
        private int FindNodeTrueIndex(TreeView treeView, TreeNode node)
        {
            int counter = 0;
            for (int i = 0; i < treeView.Nodes.Count; i++)
            {
                for (int j = 0; j < treeView.Nodes[i].Nodes.Count; j++)
                {
                    counter++;

                    if (node.Equals(treeView.Nodes[i].Nodes[j]))
                    {
                        return counter;
                    }
                }

                counter++;
            }

            return -1;
        }

        /// <summary>
        /// Handles the AfterSelect event of the TvMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TreeViewEventArgs"/> instance containing the event data.</param>
        private void TvMain_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // the parent node does not need to be handled..
            if (e.Node.Level == 0)
            {
                return;
            }

            // if the node was selected by the user, the node index needs updating..
            SelectedNodeIndex = FindNodeTrueIndex((TreeView) sender, e.Node);

            // get the tag of the clicked tree view node..
            var clickResult =
                ((string fileName, int lineNumber, int startLocation, int length, string lineContents, bool isFileOpen))
                e.Node.Tag;

            // raise the click event if subscribed..
            SearchResultSelected?.Invoke(this, new SearchResultTreeViewClickEventArgs { SearchResult = clickResult });

            // set the value back to the node's tag..
            e.Node.Tag = clickResult;
        }

        /// <summary>
        /// Sets the tree view data for a file name to <paramref name="value"/>.
        /// </summary>
        /// <param name="fileName">Name of the file which flag to set.</param>
        /// <param name="value">if set to <c>true</c> the file in the tree view is set as opened, <c>false</c> otherwise.</param>
        public void SetFileOpened(string fileName, bool value)
        {
            SuspendLayout(); // suspend the layout of the form..
            tvMain.BeginUpdate(); // the tree can be large, so suspend the draw..
            NoDraw = true;

            for (int i = 0; i < tvMain.Nodes.Count; i++)
            {
                var node =
                    ((string fileName, int lineNumber, int startLocation, int length, string lineContents, bool
                        isFileOpen)) (tvMain.Nodes[i].Tag);

                if (node.fileName == fileName)
                {
                    node.isFileOpen = value;

                    tvMain.Nodes[i].Tag = node;

                    for (int j = 0; j < tvMain.Nodes[i].Nodes.Count; j++)
                    {
                        var subNode = 
                            ((string fileName, int lineNumber, int startLocation, int length, string lineContents, bool
                                isFileOpen)) (tvMain.Nodes[i].Nodes[j].Tag);

                        subNode.isFileOpen = value;

                        tvMain.Nodes[i].Nodes[j].Tag = subNode;
                    }

                }
            }

            NoDraw = false;
            tvMain.EndUpdate(); // END: the tree can be large, so suspend the draw..
            ResumeLayout(); // END: suspend the layout of the form..
        }

        // (C):: https://social.msdn.microsoft.com/Forums/windows/en-US/7e7b25bd-7adf-43c1-8546-08a308084cf5/any-way-to-change-the-highlight-color-for-an-inactive-not-focused-treeview-control?forum=winforms
        /// <summary>
        /// A <see cref="TreeView.DrawNode"/>
        /// <see cref="DrawTreeNodeEventHandler"/> delegate method. Called 
        /// when the tree view node gets drawn. This handler keeps 
        /// highlighting the selected node even  when the control does not 
        /// have focus.
        /// </summary>
        /// <param name="sender">
        /// The <see cref="TreeView"/> instance that called the event.
        /// </param>
        /// <param name="e">
        /// The tree node event.
        /// </param>
        /// <remarks>
        /// Set the <see cref="TreeView.DrawMode"/> property to
        /// <see cref="TreeViewDrawMode.OwnerDrawText"/> so this event handler
        /// gets called. 
        /// </remarks>
        private void DrawTreeNodeHighlightSelectedEvenWithoutFocus(object sender, DrawTreeNodeEventArgs e)
        {
            if (NoDraw)
            {
                return;
            }

            Color foreColor;
            if (e.Node == ((TreeView)sender).SelectedNode)
            {
                // is selected, draw a highlighted text rectangle under the text..
                foreColor = SystemColors.HighlightText;
                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds, foreColor, SystemColors.Highlight);
            }
            else
            {
                foreColor = (e.Node.ForeColor == Color.Empty) ? ((TreeView)sender).ForeColor : e.Node.ForeColor;
                e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
            }

            TextRenderer.DrawText(
                e.Graphics,
                e.Node.Text,
                e.Node.NodeFont ?? e.Node.TreeView.Font,
                e.Bounds,
                foreColor,
                TextFormatFlags.GlyphOverhangPadding);
        }

        /// <summary>
        /// Handles the DrawNode event of the TvMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DrawTreeNodeEventArgs"/> instance containing the event data.</param>
        private void TvMain_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            DrawTreeNodeHighlightSelectedEvenWithoutFocus(sender, e);
        }

        /// <summary>
        /// Handles the KeyDown event of the FormSearchResultTree control for navigating within the tree view.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void FormSearchResultTree_KeyDown(object sender, KeyEventArgs e)
        {
            // a user wishes to navigate within the FormSearchResultTree..
            if (e.OnlyAlt() && e.KeyCodeIn(Keys.Left, Keys.Right, Keys.X))
            {
                // validate that there is an instance of the FormSearchResultTree which is visible..
                if (Visible)
                {
                    // Alt+Left navigates to the previous tree node within the form..
                    if (e.KeyCode == Keys.Left) 
                    {
                        PreviousOccurrence();
                    }
                    // Alt+Right navigates to the next tree node within the form..
                    else if (e.KeyCode == Keys.Right)
                    {
                        NextOccurrence();
                    }
                    // Alt+X closes the FormSearchResultTree instance..
                    else
                    {
                        CloseTree();
                    }

                    // this is handled..
                    e.Handled = true;
                }
            }
        }
    }
}
