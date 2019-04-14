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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptNotepad.Settings;
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
            
            // don't allow multiple instances of this..
            PreviousInstance?.Close();
            PreviousInstance = this;
        }

        /// <summary>
        /// Gets or sets the previous instance of this class.
        /// </summary>
        public static FormSearchResultTree PreviousInstance { get; set; } = null;


        private IEnumerable<(string fileName, int lineNumber, int startLocation, int length, string lineContent, bool isFileOpen)>
            searchResults;

        /// <summary>
        /// Gets or sets the search results from the <see cref="SearchOpenDocumentsBase"/> class.
        /// </summary>
        public IEnumerable<(string fileName, int lineNumber, int startLocation, int length, string lineContents, bool isFileOpen)> SearchResults
        {
            // just return the value..
            get => searchResults;

            set
            {
                // value changed..
                if (value != searchResults)
                {
                    // save the value..
                    searchResults = value;

                    // build the tree view..
                    CreateTree();
                }
            }
        }

        /// <summary>
        /// Creates the tree view based on the <seealso cref="SearchResults"/> property value.
        /// </summary>
        private void CreateTree()
        {
            tvMain.BeginUpdate(); // the tree can be large, so suspend the draw..
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
                    // create the primary node..
                    upperNode = tvMain.Nodes.Add(searchResult.fileName, Path.GetFileName(searchResult.fileName), 0);
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
            tvMain.EndUpdate(); // END: the tree can be large, so suspend the draw..
        }

        /// <summary>
        /// A delegate for the SearchResultClick event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="SearchResultTreeViewClickEventArgs"/> instance containing the event data.</param>
        public delegate void OnSearchResultClick(object sender, SearchResultTreeViewClickEventArgs e);

        /// <summary>
        /// Occurs when a search result was clicked.
        /// </summary>
        public static event OnSearchResultClick SearchResultClick;

        /// <summary>
        /// Gets or sets the an event handler if the form requests to dock in the main form.
        /// </summary>
        public static event EventHandler RequestDockMainForm;

        private void TvMain_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // the parent node does not need to be handled..
            if (e.Node.Level == 0)
            {
                return;
            }

            // get the tag of the clicked tree view node..
            var clickResult =
                ((string fileName, int lineNumber, int startLocation, int length, string lineContents, bool isFileOpen))
                e.Node.Tag;

            // raise the click event if subscribed..
            SearchResultClick?.Invoke(this, new SearchResultTreeViewClickEventArgs { SearchResult = clickResult });
        }

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
    }
}
