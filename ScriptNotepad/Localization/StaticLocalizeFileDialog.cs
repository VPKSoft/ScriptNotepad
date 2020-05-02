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
using VPKSoft.LangLib;

namespace ScriptNotepad.Localization
{
    /// <summary>
    /// A class to localize file dialog extensions.
    /// </summary>
    public static class StaticLocalizeFileDialog
    {
        /// <summary>
        /// Localizes a given file dialog and adds the localized Hunspell dictionary file filter to it's Filter property.
        /// </summary>
        /// <param name="dialog">The file dialog to localize.</param>
        public static void InitOpenHunspellDictionaryDialog(FileDialog dialog)
        {
            dialog.Filter = string.Empty;
            dialog.Filter += DBLangEngine.GetStatMessage("msgFileDic", "Hunspell dictionary file|*.dic|A text in a file dialog filter to indicate a Hunspell dictionary file");
        }

        /// <summary>
        /// Localizes a given file dialog and adds the localized Hunspell dictionary file filter to it's Filter property.
        /// </summary>
        /// <param name="dialog">The file dialog to localize.</param>
        public static void InitOpenHunspellAffixFileDialog(FileDialog dialog)
        {
            dialog.Filter = string.Empty;
            dialog.Filter += DBLangEngine.GetStatMessage("msgFileAffix", "Hunspell affix file |*.aff|A text in a file dialog filter to indicate a Hunspell affix file");
        }

        /// <summary>
        /// Localizes a given file dialog and adds the localized XML file filter to it's Filter property.
        /// </summary>
        /// <param name="dialog">The file dialog to localize.</param>
        public static void InitOpenXmlFileDialog(FileDialog dialog)
        {
            dialog.Filter = string.Empty;
            dialog.Filter += DBLangEngine.GetStatMessage("msgeXtensibleMarkupLanguageFile", "eXtensible Markup Language file|*.xml;*.xsml;*.xls;*.xsd;*.kml;*.wsdl;*.xlf;*.xliff|A text in a file dialog indicating eXtensible Markup Language files");
        }

        /// <summary>
        /// Localizes a given file dialog and adds the localized custom spell check library file filter (zip) to it's Filter property.
        /// </summary>
        /// <param name="dialog">The file dialog to localize.</param>
        public static void InitOpenSpellCheckerZip(FileDialog dialog)
        {
            dialog.Filter = string.Empty;
            dialog.Filter += DBLangEngine.GetStatMessage("msgCustomSpellCheckerZipFile", "Custom spell check library|*.zip|A text in a file dialog filter to indicate a custom spell checker library in a compressed zip package");
        }

        /// <summary>
        /// Localizes a given file dialog with a HTML filter.
        /// </summary>
        /// <param name="dialog">The file dialog to localize.</param>
        // ReSharper disable once InconsistentNaming
        public static void InitHTMLFileDialog(FileDialog dialog)
        {
            dialog.Filter = DBLangEngine.GetStatMessage("msgHyperTextMarkupLanguageFileHTML", "Hyper Text Markup Language file|*.html|A text in a file dialog indicating Hyper Text Markup Language files (HTML only)");
        }

        /// <summary>
        /// Localizes a given file dialog and adds the localized lexer file types to it's Filter property.
        /// </summary>
        /// <param name="dialog">The file dialog to localize.</param>
        public static void InitFileDialog(FileDialog dialog)
        {
            dialog.Filter = string.Empty;
            dialog.Filter += DBLangEngine.GetStatMessage("msgAllFileTypes", "All types|*.*|A text in a file dialog indicating all files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgNormalTextFile", "Normal text file|*.txt|A text in a file dialog indicating .txt files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgFlashActionScriptFile", "Flash ActionScript file|*.as;*.mx|A text in a file dialog indicating Flash ActionScript files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgAdaFile", "Ada file|*.ada;*.ads;*.adb|A text in a file dialog indicating Ada files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgAssemblySourceFile", "Assembly language source file|*.asm|A text in a file dialog indicating Assembly language source files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgActiveServerPagesScriptFile", "Active Server Pages script file|*.asp|A text in a file dialog indicating Active Server Pages script files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgAutoItFile", "AutoIt|*.au3|A text in a file dialog indicating AutoIt files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgUnixScriptFile", "Unix script file|*.sh;*.bsh|A text in a file dialog indicating Unix script files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgBatchFile", "Batch file|*.bat;*.cmd;*.nt|A text in a file dialog indicating Batch files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgCSourceFile", "C source file|*.c|A text in a file dialog indicating C source files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgCategoricalAbstractMachineLanguageFile", "Categorical Abstract Machine Language|*.ml;*.mli;*.sml;*.thy|A text in a file dialog indicating Categorical Abstract Machine Language files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgCMakeFile", "CMake file|*.cmake|A text in a file dialog indicating CMake files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgCommonBusinessOrientedLanguage", "Common Business Oriented Language|*.cbl;*.cbd;*.cdb;*.cdc;*.cob|A text in a file dialog indicating Common Business Oriented Language files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgCoffeeScriptLanguage", "Coffee Script file|*.litcoffee|A text in a file dialog indicating Coffee Script files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgCPPSourceFile", "C++ source file|*.h;*.hpp;*.hxx;*.cpp;*.cxx;*.cc|A text in a file dialog indicating C++ source files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgCSSourceFile", "C# source file|*.cs|A text in a file dialog indicating C# source files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgCSSSourceFile", "Cascading Style Sheets File|*.css|A text in a file dialog indicating Cascading Style Sheets files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgDSourceFile", "D programming language|*.d|A text in a file dialog indicating D programming language files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgDiffFile", "Diff file|*.diff;*.patch|A text in a file dialog indicating Diff files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgFortranFreeFormSourceFile", "Fortran free form source file|*.f;*.for;*.f90;*.f95;*.f2k|A text in a file dialog indicating Fortran free form source files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgHaskellSourceFile", "Haskell source file|*.hs;*.lhs;*.as;*.las|A text in a file dialog indicating Haskell source files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgHyperTextMarkupLanguageFile", "Hyper Text Markup Language file|*.html;*.htm;*.shtml;*.shtm;*.xhtml;*.hta|A text in a file dialog indicating Hyper Text Markup Language files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgMSIniFile", "MS INI file|*.ini;*.inf;*.reg;*.url|A text in a file dialog indicating MS INI files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgInnoSetupScriptFile", "Inno Setup Script File|*.iss|A text in a file dialog indicating Inno Setup Script files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgJavaSourceFile", "Java source file|*.java|A text in a file dialog indicating Java source files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgJavaScriptFile", "Java script file|*.js|A text in a file dialog indicating Java script files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgJavaServerPagesFile", "JavaServer pages file|*.jsp|A text in a file dialog indicating JavaServer pages files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgKiXtartFile", "KiXtart file|*.kix|A text in a file dialog indicating KiXtart files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgListProcessingLanguageFile", "List Processing language file|*.lsp;*.lisp|A text in a file dialog indicating List Processing language files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgLuaSourceFile", "Lua source file|*.lua|A text in a file dialog indicating Lua source files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgMakeFile", "Makefile|*.mak|A text in a file dialog indicating Makefile files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgMATrixLABoratoryFile", "MATrix LABoratory file|*.m|A text in a file dialog indicating MATrix LABoratory files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgMSDOSStyleAsciiArtFile", "MSDOS Style/ASCII Art file|*.nfo|A text in a file dialog indicating MSDOS Style/ASCII Art file files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgNullsoftScriptableInstallSystemScriptFile", "Nullsoft Scriptable Install System script file|*.nsi;*.nsh|A text in a file dialog indicating Nullsoft Scriptable Install System script files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgPascalSourceFile", "Pascal source file|*.pas|A text in a file dialog indicating Pascal source files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgPerlSourceFile", "Perl source file|*.pl;*.pm;*.plx|A text in a file dialog indicating Perl source files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgPHPHypertextPreprocessorFile", "PHP Hypertext Preprocessor file|*.php;*.php3;*.phtml|A text in a file dialog indicating PHP Hypertext Preprocessor files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgPostScriptFile", "PostScript file|*.ps|A text in a file dialog indicating PostScript files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgWindowsPowerShellFile", "Windows PowerShell file|*.ps1;*.psm1|A text in a file dialog indicating Windows PowerShell files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgPropertiesFile", "Properties file|*.properties|A text in a file dialog indicating Properties files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgPythonFile", "Python file|*.py;*.pyw|A text in a file dialog indicating Python files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgRProgrammingLanguageFile", "R programming language file|*.r|A text in a file dialog indicating R programming language files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgWindowsResourceFile", "Windows resource file|*.rc|A text in a file dialog indicating Windows resource files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgRubyFile", "Ruby file|*.rb;*.rbw|A text in a file dialog indicating Ruby files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgSchemeFile", "Scheme file|*.scm;*.smd;*.ss|A text in a file dialog indicating Scheme files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgSmalltalkFile", "Smalltalk file|*.st|A text in a file dialog indicating Smalltalk files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgStructuredQueryLanguageFile", "Structured Query Language file|*.sql;*.sql_script|A text in a file dialog indicating Structured Query Language files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgToolCommandLanguageFile", "Tool Command Language file|*.tlc|A text in a file dialog indicating Tool Command Language files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgTeXFile", "TeX file|*.tex|A text in a file dialog indicating TeX files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgVisualBasicFile", "Visual Basic file|*.vb;*.vbs|A text in a file dialog indicating Visual Basic files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgVerilogFile", "Verilog file|*.vs;*.sv;*.vh;*.svh|A text in a file dialog indicating Verilog files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgVHSICHardwareDescriptionLanguageFile", "VHSIC Hardware Description Language file|*.vhd;*.vhdl|A text in a file dialog indicating VHSIC Hardware Description Language files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgeXtensibleMarkupLanguageFile", "eXtensible Markup Language file|*.xml;*.xsml;*.xls;*.xsd;*.kml;*.wsdl;*.xlf;*.xliff|A text in a file dialog indicating eXtensible Markup Language files");
            dialog.Filter += @"|" + DBLangEngine.GetStatMessage("msgYAMLFile", "YAML Ain't a Markup Language file|*.yml|A text in a file dialog indicating YAML Ain't a Markup Language files");
        }
    }
}
