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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptNotepad.UtilityClasses.SearchAndReplace;
using VPKSoft.LangLib;
using VPKSoft.PosLib;

namespace ScriptNotepad.DialogForms
{
    /// <summary>
    /// A dialog to report various length process states.
    /// Implements the <see cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    /// </summary>
    /// <seealso cref="VPKSoft.LangLib.DBLangEngineWinforms" />
    public partial class FormDialogCommonProgress : DBLangEngineWinforms
    {
        private SearchOpenDocuments SearchOpenDocuments { get; } = null;

        private  bool SearchAllOpenDocuments { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogCommonProgress"/> class.
        /// <param name="searchOpenDocuments">An instance to a <see cref="SearchOpenDocuments"/> class to perform search.</param>
        /// <param name="searchAllOpenDocuments">A flag indicating whether to perform a search for all the open documents.</param>
        /// </summary>
        public FormDialogCommonProgress(SearchOpenDocuments searchOpenDocuments, bool searchAllOpenDocuments)
        {
            if (!ConstructorHelper())
            {
                return;
            }

            SearchOpenDocuments = searchOpenDocuments;

            SearchAllOpenDocuments = searchAllOpenDocuments;

            ShowDialog();
        }

        private bool ConstructorHelper()
        {
            InitializeComponent();
            
            DBLangEngine.DBName = "lang.sqlite"; // Do the VPKSoft.LangLib == translation..

            if (Utils.ShouldLocalize() != null)
            {
                DBLangEngine.InitalizeLanguage("ScriptNotepad.Localization.Messages", Utils.ShouldLocalize(), false);
                return false; // After localization don't do anything more..
            }

            return true;
        }


        private void BwMain_DoWork(object sender, DoWorkEventArgs e)
        {
            if (SearchOpenDocuments != null)
            {
                if (SearchAllOpenDocuments)
                {
                    // TODO::Search all!
                }
                else
                {
                    SearchOpenDocumentsBase.SearchProgress += SearchOpenDocuments_SearchProgress;

                    var result = SearchOpenDocuments.SearchAll();

                    Invoke(new MethodInvoker(delegate 
                    {
                        var tree = new FormSearchResultTree();

                        tree.Show();

                        tree.SearchResults = result;
                    }));                                        
                }
            }
        }

        int previousProgress = -1;

        private void SearchOpenDocuments_SearchProgress(object sender, UtilityClasses.SearchAndReplace.Misc.SearchAndReplaceProgressEventArgs e)
        {
            if (previousProgress == e.ProgressPercentage)
            {
                return;
            }
            else
            {
                previousProgress = e.ProgressPercentage;
                bwMain.ReportProgress(e.ProgressPercentage);                
            }
        }

        private void FormDialogCommonProgress_Shown(object sender, EventArgs e)
        {
            bwMain.RunWorkerAsync();
        }

        private void BwMain_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate { pbMain.Value = e.ProgressPercentage; }));
        }
    }
}
