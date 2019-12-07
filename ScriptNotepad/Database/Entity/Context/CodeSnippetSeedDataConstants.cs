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

namespace ScriptNotepad.Database.Entity.Context
{
    /// <summary>
    /// Constants for the script (C#) sample code.
    /// </summary>
    public class CodeSnippetSeedDataConstants
    {
        /// <summary>
        /// The simple text lines replace script (C#).
        /// </summary>
        public const string SimpleReplaceScript =
            @"#region Usings
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
#endregion


public class ManipulateLineLines
{
    public static string Evaluate(List<string> fileLines)
    {
        string result = string.Empty;
        // replace text from the file''s lines..
        for (int i = 0; i < fileLines.Count; i++)
        {
           fileLines[i] = fileLines[i].Replace(""value1"", ""value2"");
        }

        result = string.Concat(fileLines); // concatenate the result lines.. 
        return result;
    }
}";
        /// <summary>
        /// The simple line ending change script (C#).
        /// </summary>
        public const string SimpleLineEndingChangeScript =
            @"#region Usings
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
#endregion


public class ManipulateText
{
    public static string Evaluate(string fileContents)
    {
        // convert Windows line endings to Linux / Unix line endings..
        fileContents = fileContents.Replace(""\r\n"", ""\n""); 
        return fileContents;
    }
}";

        /// <summary>
        /// The simple XML manipulation script (C#).
        /// </summary>
        public const string SimpleXmlManipulationScript =
            @"#region Usings
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
#endregion


public class ManipulateText
{
    public static string Evaluate(string fileContents)
    {
	    XDocument xDoc = XDocument.Parse(fileContents);

		// An example loop: foreach (XElement element in xDoc.Descendants(""give_a_valid_name""))
        // An example loop: {
			// Do something with the: element.Attribute(""some_name"").Value;
        // An example loop: }

        return xDoc.ToString();
    }
}";
    }
}
