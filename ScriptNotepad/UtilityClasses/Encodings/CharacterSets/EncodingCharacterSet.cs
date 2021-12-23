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

using System.Collections.Generic;
using System.Linq;
using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.UtilityClasses.Encodings.CharacterSets
{
    /// <summary>
    /// An enumeration containing different categories for different encodings.
    /// </summary>
    public enum CharacterSets
    {
        /// <summary>
        /// An enumeration value for the Arabic character sets.
        /// </summary>
        Arabic,

        /// <summary>
        /// An enumeration value for the Baltic character sets.
        /// </summary>
        Baltic,

        /// <summary>
        /// An enumeration value for the Canada character sets.
        /// </summary>
        Canada,

        /// <summary>
        /// An enumeration value for the Cyrillic character sets.
        /// </summary>
        Cyrillic,

        /// <summary>
        /// An enumeration value for the Central European character sets.
        /// </summary>
        CentralEuropean,

        /// <summary>
        /// An enumeration value for the Chinese character sets.
        /// </summary>
        Chinese,

        /// <summary>
        /// An enumeration value for the Denmark and Norway character sets.
        /// </summary>
        DenmarkNorway,

        /// <summary>
        /// An enumeration value for the Finland and Sweden character sets.
        /// </summary>
        FinlandSweden,

        /// <summary>
        /// An enumeration value for the France character sets.
        /// </summary>
        France,

        /// <summary>
        /// An enumeration value for the German character sets.
        /// </summary>
        German,

        /// <summary>
        /// An enumeration value for the Greek character sets.
        /// </summary>
        Greek,

        /// <summary>
        /// An enumeration value for the Hebrew character sets.
        /// </summary>
        Hebrew,

        /// <summary>
        /// An enumeration value for the Icelandic character sets.
        /// </summary>
        Icelandic,

        /// <summary>
        /// An enumeration value for the Italy character sets.
        /// </summary>
        Italy,

        /// <summary>
        /// An enumeration value for the Arabic character sets.
        /// </summary>
        Japanese,

        /// <summary>
        /// An enumeration value for the Korean character sets.
        /// </summary>
        Korean,

        /// <summary>
        /// An enumeration value for the Latin character sets.
        /// </summary>
        Latin,

        /// <summary>
        /// An enumeration value for the Miscellaneous character sets.
        /// </summary>
        Miscellaneous,

        /// <summary>
        /// An enumeration value for the Norwegian character sets.
        /// </summary>
        Norwegian,

        /// <summary>
        /// An enumeration value for the Western European character sets.
        /// </summary>
        WesternEuropean,

        /// <summary>
        /// An enumeration value for the Spain character sets.
        /// </summary>
        Spain,

        /// <summary>
        /// An enumeration value for the Swedish character sets.
        /// </summary>
        Swedish,

        /// <summary>
        /// An enumeration value for the Taiwan character sets.
        /// </summary>
        Taiwan,

        /// <summary>
        /// An enumeration value for the Thai character sets.
        /// </summary>
        Thai,

        /// <summary>
        /// An enumeration value for the Turkish character sets.
        /// </summary>
        Turkish,

        /// <summary>
        /// An enumeration value for the Unicode character sets.
        /// </summary>
        Unicode,

        /// <summary>
        /// An enumeration value for the Assamese character sets.
        /// </summary>
        Assamese,

        /// <summary>
        /// An enumeration value for the Bengali character sets.
        /// </summary>
        Bengali,

        /// <summary>
        /// An enumeration value for the Devanagari character sets.
        /// </summary>
        Devanagari,

        /// <summary>
        /// An enumeration value for the Estonian character sets.
        /// </summary>
        Estonian,

        /// <summary>
        /// An enumeration value for the Kannada character sets.
        /// </summary>
        Kannada,

        /// <summary>
        /// An enumeration value for the Malayalam character sets.
        /// </summary>
        Malayalam,

        /// <summary>
        /// An enumeration value for the Oriya character sets.
        /// </summary>
        Oriya,

        /// <summary>
        /// An enumeration value for the Punjabi character sets.
        /// </summary>
        Punjabi,

        /// <summary>
        /// An enumeration value for the Tamil character sets.
        /// </summary>
        Tamil,

        /// <summary>
        /// An enumeration value for the Telugu character sets.
        /// </summary>
        Telugu,

        /// <summary>
        /// An enumeration value for the Vietnamese character sets.
        /// </summary>
        Vietnamese,

        /// <summary>
        /// An enumeration value for the single character sets.
        /// </summary>
        SingleCharacterSets
    }

    /// <summary>
    /// A class which categorizes the .NET character encodings under different localizable name-category pairs.
    /// </summary>
    public class EncodingCharacterSet: ErrorHandlingBase
    {
        /// <summary>
        /// An internal list of character sets and their encodings.
        /// </summary>
        private readonly List<KeyValuePair<List<int>, CharacterSets>> internalList = new List<KeyValuePair<List<int>, CharacterSets>>();

        /// <summary>
        /// An internal list of character set names with their corresponding <see cref="CharacterSets"/> enumeration pairs.
        /// </summary>
        private static readonly List<KeyValuePair<CharacterSets, string>> InternalEnumDescriptionPairs = new List<KeyValuePair<CharacterSets, string>>();

        /// <summary>
        /// Constructs the internal lists for to be used with this class.
        /// </summary>
        private void ConstructInternalList()
        {
            #region CharacterSetList
            internalList.Clear();
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 708, 720, 864, 1256, 10004, 20420, 28596, 57010 }.ToList(), CharacterSets.Arabic));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 775, 1257, 28594 }.ToList(), CharacterSets.Baltic));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 37, 863, 1140 }.ToList(), CharacterSets.Canada));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 855, 866, 1251, 10007, 20866, 20880, 21025, 21866, 28595 }.ToList(), CharacterSets.Cyrillic));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 852, 1250, 10029, 28592 }.ToList(), CharacterSets.CentralEuropean));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 936, 950, 10002, 10008, 20000, 20002, 20936, 50227, 51936, 52936, 54936 }.ToList(), CharacterSets.Chinese));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 1142, 20277 }.ToList(), CharacterSets.DenmarkNorway));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 1143, 20278 }.ToList(), CharacterSets.FinlandSweden));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 1147, 20297 }.ToList(), CharacterSets.France));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 1141, 20106, 20273 }.ToList(), CharacterSets.German));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 737, 869, 875, 1253, 10006, 20423, 28597 }.ToList(), CharacterSets.Greek));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 862, 1255, 10005, 20424, 28598, 38598 }.ToList(), CharacterSets.Hebrew));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 861, 1149, 10079, 20871 }.ToList(), CharacterSets.Icelandic));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 1144, 20280 }.ToList(), CharacterSets.Italy));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 932, 10001, 20290, 20932, 50220, 50221, 50222, 51932 }.ToList(), CharacterSets.Japanese));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 949, 1361, 10003, 20833, 20949, 50225, 51949 }.ToList(), CharacterSets.Korean));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 858, 870, 1026, 1047, 20924, 28593, 28605 }.ToList(), CharacterSets.Latin));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 500, 860, 865, 1146, 1148, 10010, 10017, 10082, 20127, 20261, 20269, 20285, 29001 }.ToList(), CharacterSets.Miscellaneous));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 1142, 20108, 20277 }.ToList(), CharacterSets.Norwegian));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 850, 1252, 10000, 20105, 28591 }.ToList(), CharacterSets.WesternEuropean));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 1145, 20284 }.ToList(), CharacterSets.Spain));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 1143, 20107, 20278 }.ToList(), CharacterSets.Swedish));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 20001, 20003, 20004, 20005 }.ToList(), CharacterSets.Taiwan));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 874, 10021, 20838 }.ToList(), CharacterSets.Thai));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 857, 1026, 1254, 10081, 20905, 28599 }.ToList(), CharacterSets.Turkish));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 437, 1200, 1201, 12000, 12001, 65000, 65001 }.ToList(), CharacterSets.Unicode));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 57006 }.ToList(), CharacterSets.Assamese));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 57003 }.ToList(), CharacterSets.Bengali));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 57002 }.ToList(), CharacterSets.Devanagari));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 28603 }.ToList(), CharacterSets.Estonian));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 57008 }.ToList(), CharacterSets.Kannada));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 57009 }.ToList(), CharacterSets.Malayalam));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 57007 }.ToList(), CharacterSets.Oriya));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 57011 }.ToList(), CharacterSets.Punjabi));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 57004 }.ToList(), CharacterSets.Tamil));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 57005 }.ToList(), CharacterSets.Telugu));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 1258 }.ToList(), CharacterSets.Vietnamese));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new[] { 57006, 57003, 57002, 28603, 57008, 57009, 57007, 57011, 57004, 57005, 1258 }.ToList(), CharacterSets.SingleCharacterSets));
            #endregion

            // the static list will be created with a constructor..
            if (InternalEnumDescriptionPairs.Count == 0)
            {
                ConstructInternalCharacterSetEnumNamePairs();
            }
        }

        /// <summary>
        /// Constructs the internal character set-enumeration pairs.
        /// </summary>
        private static void ConstructInternalCharacterSetEnumNamePairs()
        {
            #region CharacterSetEnumNamePairs
            InternalEnumDescriptionPairs.Clear();
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Arabic, "Arabic"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Baltic, "Baltic"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Canada, "Canada"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Cyrillic, "Cyrillic"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.CentralEuropean, "Central European"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Chinese, "Chinese"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.DenmarkNorway, "Denmark-Norway"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.FinlandSweden, "Finland-Sweden"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.France, "France"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.German, "German"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Greek, "Greek"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Hebrew, "Hebrew"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Icelandic, "Icelandic"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Italy, "Italy"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Japanese, "Japanese"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Korean, "Korean"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Latin, "Latin"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Miscellaneous, "Miscellaneous"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Norwegian, "Norwegian"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.WesternEuropean, "Western European"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Spain, "Spain"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Swedish, "Swedish"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Taiwan, "Taiwan"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Thai, "Thai"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Turkish, "Turkish"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Unicode, "Unicode"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Assamese, "Assamese"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Bengali, "Bengali"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Devanagari, "Devanagari"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Estonian, "Estonian"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Kannada, "Kannada"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Malayalam, "Malayalam"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Oriya, "Oriya"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Punjabi, "Punjabi"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Tamil, "Tamil"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Telugu, "Telugu"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Vietnamese, "Vietnamese"));
            InternalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.SingleCharacterSets, "Single Character Sets"));
            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EncodingCharacterSet"/> class.
        /// </summary>
        public EncodingCharacterSet()
        {
            // make the internal list to be used with this class..
            ConstructInternalList();
        }

        /// <summary>
        /// Gets the character set list.
        /// </summary>
        /// <param name="singleCodePageResults">A flag indicating if character sets containing only single encoding should be returned.</param>
        /// <returns>A collection of CharacterSets enumeration based on the given parameters.</returns>
        public IEnumerable<CharacterSets> GetCharacterSetList(bool singleCodePageResults)
        {
            foreach (var item in internalList)
            {
                if (item.Key.Count == 1 && !singleCodePageResults)
                {
                    continue;
                }
                yield return item.Value;
            }
        }

        /// <summary>
        /// Gets the character sets list for the given encoding.
        /// </summary>
        /// <param name="encoding">The encoding to be used to get the character sets the encoding belongs to.</param>
        /// <param name="singleCodePageResults">A flag indicating if character sets containing only single encoding should be returned.</param>
        /// <returns>A collection of CharacterSets enumeration based on the given parameters.</returns>
        public IEnumerable<CharacterSets> GetCharacterSetsForEncoding(Encoding encoding, bool singleCodePageResults)
        {
            foreach (var item in internalList)
            {
                if (item.Key.Count == 1 && !singleCodePageResults)
                {
                    continue;
                }

                // loop through the code pages of the character set..
                foreach (var codePages in item.Key)
                {
                    // if a match is found..
                    if (codePages == encoding.CodePage)
                    {
                        // ..add it to the resulting collection..
                        yield return item.Value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the name of the character set.
        /// </summary>
        /// <param name="characterSets">The CharacterSets enumeration value.</param>
        /// <returns>A name for a given CharacterSets enumeration.</returns>
        public string GetCharacterSetName(CharacterSets characterSets)
        {
            int idx = InternalEnumDescriptionPairs.FindIndex(f => f.Key == characterSets);
            return idx != -1 ? InternalEnumDescriptionPairs[idx].Value : string.Empty;
        }

        /// <summary>
        /// Localizes the name of the character set.
        /// </summary>
        /// <param name="characterSets">An enumeration of the character set of which to give a new name.</param>
        /// <param name="name">A new name (hopefully localized) for the character set.</param>
        public void LocalizeCharacterSetName(CharacterSets characterSets, string name)
        {
            if (name.Trim() == string.Empty)
            {
                return;
            }
            int idx = InternalEnumDescriptionPairs.FindIndex(f => f.Key == characterSets);
            if (idx != -1)
            {
                InternalEnumDescriptionPairs[idx] = new KeyValuePair<CharacterSets, string>(characterSets, name);
            }
        }

        /// <summary>
        /// Gets a collection the <see cref="Encoding"/> class instances corresponding to the given character set enumeration.
        /// </summary>
        /// <param name="characterSets">An enumeration value indicating which character set's encodings to get.</param>
        /// <returns>A collection of <see cref="Encoding"/> class instances for the given character set enumeration.</returns>
        public IEnumerable<Encoding> this[CharacterSets characterSets]
        {
            get
            {
                var result = new List<Encoding>();
                int idx = internalList.FindIndex(f => f.Value == characterSets);
                if (idx != -1)
                {
                    foreach (int encodingNum in internalList[idx].Key)
                    {
                        try
                        {
                            var encoding = Encoding.GetEncoding(encodingNum);
                            result.Add(encoding);
                        }
                        catch (Exception ex)
                        {
                            ExceptionLogAction?.Invoke(ex);
                            try
                            {
                                var encoding = CodePagesEncodingProvider.Instance.GetEncoding(encodingNum);

                                if (encoding != null)
                                {
                                    result.Add(encoding);
                                }
                            }
                            catch (Exception exInner)
                            {
                                ExceptionLogAction?.Invoke(exInner);
                            }
                        }
                    }
                }

                return result;
            }
        }
    }
}
