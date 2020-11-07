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


namespace InstallerBaseWixSharp.Files.Dialogs.DialogClasses
{
    /// <summary>
    /// A class representing a single file association.
    /// </summary>
    public class FileAssociation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileAssociation"/> class.
        /// </summary>
        /// <param name="extension">The extension to associate.</param>
        /// <param name="associationName">The name of the association.</param>
        public FileAssociation(string extension, string associationName)
        {
            Extension = extension;
            AssociationName = associationName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileAssociation"/> from a specified pseudo-serialization string.
        /// </summary>
        /// <param name="serializeString">The pseudo-serialization string representing a <see cref="FileAssociation"/> class instance.</param>
        /// <returns>FileAssociation.</returns>
        public static FileAssociation FromSerializeString(string serializeString)
        {
            return new FileAssociation(serializeString.Split('|')[0], serializeString.Split('|')[1]);
        }

        /// <summary>
        /// Gets or sets the extension to associate.
        /// </summary>
        /// <value>The extension to associate.</value>
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets the name of the association.
        /// </summary>
        /// <value>The name of the association.</value>
        public string AssociationName { get; set; }

        /// <summary>
        /// Converts this instance to a pseudo-serialization string.
        /// </summary>
        /// <returns>A string representing an instance of this class.</returns>
        public string ToSerializeString()
        {
            return Extension + "|" + AssociationName;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return AssociationName;
        }
    }
}

