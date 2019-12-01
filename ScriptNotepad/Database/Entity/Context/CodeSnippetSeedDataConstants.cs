using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
