﻿#region License
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
using System.Linq;

namespace ScriptNotepad.UtilityClasses.Encoding.CharacterSets
{
    /// <summary>
    /// An enumeration containing different categories for different encodings.
    /// </summary>
    [Flags]
    public enum CharacterSets : long
    {
        /// <summary>
        /// An enumeration value for the Arabic character sets.
        /// </summary>
        Arabic = 1,

        /// <summary>
        /// An enumeration value for the Baltic character sets.
        /// </summary>
        Baltic = 2,

        /// <summary>
        /// An enumeration value for the Canada character sets.
        /// </summary>
        Canada = 4,

        /// <summary>
        /// An enumeration value for the Cyrillic character sets.
        /// </summary>
        Cyrillic = 8,

        /// <summary>
        /// An enumeration value for the Central European character sets.
        /// </summary>
        CentralEuropean = 16,

        /// <summary>
        /// An enumeration value for the Chinese character sets.
        /// </summary>
        Chinese = 32,

        /// <summary>
        /// An enumeration value for the Denmark and Norway character sets.
        /// </summary>
        DenmarkNorway = 64,

        /// <summary>
        /// An enumeration value for the Finland and Sweden character sets.
        /// </summary>
        FinlandSweden = 128,

        /// <summary>
        /// An enumeration value for the France character sets.
        /// </summary>
        France = 256,

        /// <summary>
        /// An enumeration value for the German character sets.
        /// </summary>
        German = 512,

        /// <summary>
        /// An enumeration value for the Greek character sets.
        /// </summary>
        Greek = 1024,

        /// <summary>
        /// An enumeration value for the Hebrew character sets.
        /// </summary>
        Hebrew = 2048,

        /// <summary>
        /// An enumeration value for the Icelandic character sets.
        /// </summary>
        Icelandic = 4096,

        /// <summary>
        /// An enumeration value for the Italy character sets.
        /// </summary>
        Italy = 8192,

        /// <summary>
        /// An enumeration value for the Arabic character sets.
        /// </summary>
        Japanese = 16384,

        /// <summary>
        /// An enumeration value for the Korean character sets.
        /// </summary>
        Korean = 32768,

        /// <summary>
        /// An enumeration value for the Latin character sets.
        /// </summary>
        Latin = 65536,

        /// <summary>
        /// An enumeration value for the Miscellaneous character sets.
        /// </summary>
        Miscellaneous = 131072,

        /// <summary>
        /// An enumeration value for the Norwegian character sets.
        /// </summary>
        Norwegian = 262144,

        /// <summary>
        /// An enumeration value for the Western European character sets.
        /// </summary>
        WesternEuropean = 524288,

        /// <summary>
        /// An enumeration value for the Spain character sets.
        /// </summary>
        Spain = 1048576,

        /// <summary>
        /// An enumeration value for the Swedish character sets.
        /// </summary>
        Swedish = 2097152,

        /// <summary>
        /// An enumeration value for the Taiwan character sets.
        /// </summary>
        Taiwan = 4194304,

        /// <summary>
        /// An enumeration value for the Thai character sets.
        /// </summary>
        Thai = 8388608,

        /// <summary>
        /// An enumeration value for the Turkish character sets.
        /// </summary>
        Turkish = 16777216,

        /// <summary>
        /// An enumeration value for the Unicode character sets.
        /// </summary>
        Unicode = 33554432,

        /// <summary>
        /// An enumeration value for the Assamese character sets.
        /// </summary>
        Assamese = 67108864,

        /// <summary>
        /// An enumeration value for the Bengali character sets.
        /// </summary>
        Bengali = 134217728,

        /// <summary>
        /// An enumeration value for the Devanagari character sets.
        /// </summary>
        Devanagari = 268435456,

        /// <summary>
        /// An enumeration value for the Estonian character sets.
        /// </summary>
        Estonian = 536870912,

        /// <summary>
        /// An enumeration value for the Kannada character sets.
        /// </summary>
        Kannada = 1073741824,

        /// <summary>
        /// An enumeration value for the Malayalam character sets.
        /// </summary>
        Malayalam = 2147483648,

        /// <summary>
        /// An enumeration value for the Oriya character sets.
        /// </summary>
        Oriya = 4294967296,

        /// <summary>
        /// An enumeration value for the Punjabi character sets.
        /// </summary>
        Punjabi = 8589934592,

        /// <summary>
        /// An enumeration value for the Tamil character sets.
        /// </summary>
        Tamil = 17179869184,

        /// <summary>
        /// An enumeration value for the Telugu character sets.
        /// </summary>
        Telugu = 34359738368,

        /// <summary>
        /// An enumeration value for the Vietnamese character sets.
        /// </summary>
        Vietnamese = 68719476736,

        /// <summary>
        /// An enumeration value for the single character sets.
        /// </summary>
        SingleCharacterSets = 137438953472
    }

    /// <summary>
    /// A class which categorizes the .NET character encodings under different localizable name-gategory pairs.
    /// </summary>
    public class EncodingCharacterSet
    {
        /// <summary>
        /// An internal list of character sets and their encodings.
        /// </summary>
        private List<KeyValuePair<List<int>, CharacterSets>> internalList = new List<KeyValuePair<List<int>, CharacterSets>>();

        /// <summary>
        /// An internal list of character set names with their corresponding <see cref="CharacterSets"/> enumeration pairs.
        /// </summary>
        private static List<KeyValuePair<CharacterSets, string>> internalEnumDescriptionPairs = new List<KeyValuePair<CharacterSets, string>>();

        /// <summary>
        /// Constructs the internal lists for to be used with this class.
        /// </summary>
        private void ConstructInternalList()
        {
            #region CharacterSetList
            internalList.Clear();
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 708, 720, 864, 1256, 10004, 20420, 28596, 57010 }.ToList(), CharacterSets.Arabic));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 775, 1257, 28594 }.ToList(), CharacterSets.Baltic));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 37, 863, 1140 }.ToList(), CharacterSets.Canada));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 855, 866, 1251, 10007, 20866, 20880, 21025, 21866, 28595 }.ToList(), CharacterSets.Cyrillic));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 852, 1250, 10029, 28592 }.ToList(), CharacterSets.CentralEuropean));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 936, 950, 10002, 10008, 20000, 20002, 20936, 50227, 51936, 52936, 54936 }.ToList(), CharacterSets.Chinese));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 1142, 20277 }.ToList(), CharacterSets.DenmarkNorway));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 1143, 20278 }.ToList(), CharacterSets.FinlandSweden));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 1147, 20297 }.ToList(), CharacterSets.France));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 1141, 20106, 20273 }.ToList(), CharacterSets.German));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 737, 869, 875, 1253, 10006, 20423, 28597 }.ToList(), CharacterSets.Greek));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 862, 1255, 10005, 20424, 28598, 38598 }.ToList(), CharacterSets.Hebrew));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 861, 1149, 10079, 20871 }.ToList(), CharacterSets.Icelandic));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 1144, 20280 }.ToList(), CharacterSets.Italy));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 932, 10001, 20290, 20932, 50220, 50221, 50222, 51932 }.ToList(), CharacterSets.Japanese));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 949, 1361, 10003, 20833, 20949, 50225, 51949 }.ToList(), CharacterSets.Korean));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 858, 870, 1026, 1047, 20924, 28593, 28605 }.ToList(), CharacterSets.Latin));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 500, 860, 865, 1146, 1148, 10010, 10017, 10082, 20127, 20261, 20269, 20285, 29001 }.ToList(), CharacterSets.Miscellaneous));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 1142, 20108, 20277 }.ToList(), CharacterSets.Norwegian));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 850, 1252, 10000, 20105, 28591 }.ToList(), CharacterSets.WesternEuropean));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 1145, 20284 }.ToList(), CharacterSets.Spain));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 1143, 20107, 20278 }.ToList(), CharacterSets.Swedish));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 20001, 20003, 20004, 20005 }.ToList(), CharacterSets.Taiwan));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 874, 10021, 20838 }.ToList(), CharacterSets.Thai));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 857, 1026, 1254, 10081, 20905, 28599 }.ToList(), CharacterSets.Turkish));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 437, 1200, 1201, 12000, 12001, 65000, 65001 }.ToList(), CharacterSets.Unicode));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 57006 }.ToList(), CharacterSets.Assamese));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 57003 }.ToList(), CharacterSets.Bengali));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 57002 }.ToList(), CharacterSets.Devanagari));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 28603 }.ToList(), CharacterSets.Estonian));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 57008 }.ToList(), CharacterSets.Kannada));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 57009 }.ToList(), CharacterSets.Malayalam));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 57007 }.ToList(), CharacterSets.Oriya));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 57011 }.ToList(), CharacterSets.Punjabi));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 57004 }.ToList(), CharacterSets.Tamil));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 57005 }.ToList(), CharacterSets.Telugu));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 1258 }.ToList(), CharacterSets.Vietnamese));
            internalList.Add(new KeyValuePair<List<int>, CharacterSets>(new int[] { 57006, 57003, 57002, 28603, 57008, 57009, 57007, 57011, 57004, 57005, 1258 }.ToList(), CharacterSets.SingleCharacterSets));
            #endregion

            // the static list will be created with a constructor..
            ConstructInternalCharacterSetEnumNamePairs();
        }

        /// <summary>
        /// Constructs the internal character set-enumeration pairs.
        /// </summary>
        private static void ConstructInternalCharacterSetEnumNamePairs()
        {
            #region CharacterSetEnumNamePairs
            internalEnumDescriptionPairs.Clear();
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Arabic, "Arabic"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Baltic, "Baltic"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Canada, "Canada"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Cyrillic, "Cyrillic"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.CentralEuropean, "CentralEuropean"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Chinese, "Chinese"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.DenmarkNorway, "DenmarkNorway"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.FinlandSweden, "FinlandSweden"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.France, "France"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.German, "German"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Greek, "Greek"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Hebrew, "Hebrew"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Icelandic, "Icelandic"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Italy, "Italy"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Japanese, "Japanese"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Korean, "Korean"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Latin, "Latin"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Miscellaneous, "Miscellaneous"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Norwegian, "Norwegian"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.WesternEuropean, "WesternEuropean"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Spain, "Spain"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Swedish, "Swedish"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Taiwan, "Taiwan"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Thai, "Thai"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Turkish, "Turkish"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Unicode, "Unicode"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Assamese, "Assamese"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Bengali, "Bengali"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Devanagari, "Devanagari"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Estonian, "Estonian"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Kannada, "Kannada"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Malayalam, "Malayalam"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Oriya, "Oriya"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Punjabi, "Punjabi"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Tamil, "Tamil"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Telugu, "Telugu"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.Vietnamese, "Vietnamese"));
            internalEnumDescriptionPairs.Add(new KeyValuePair<CharacterSets, string>(CharacterSets.SingleCharacterSets, "SingleCharacterSets"));
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
        /// Gets a collection of encodings not yet covered by this class.
        /// </summary>
        /// <returns>A collection of <see cref="Encoding"/> class instance.</returns>
        public IEnumerable<System.Text.Encoding> GetMissingEncodings()
        {
            var encodings = System.Text.Encoding.GetEncodings();
            List<int> allEncodings = new List<int>();
            foreach (var item in internalList)
            {
                allEncodings.AddRange(item.Key);
            }

            foreach (var encodingInfo in encodings)
            {
                if (!allEncodings.Contains(encodingInfo.CodePage))
                {
                    yield return System.Text.Encoding.GetEncoding(encodingInfo.CodePage);
                }
            }
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
            int idx = internalEnumDescriptionPairs.FindIndex(f => f.Key == characterSets);
            if (idx != -1)
            {
                internalEnumDescriptionPairs[idx] = new KeyValuePair<CharacterSets, string>(characterSets, name);
            }
        }

        /// <summary>
        /// Gets a collection the <see cref="Encoding"/> class instances corresponding to the given character set enumeration.
        /// </summary>
        /// <param name="characterSets">An enumeration value indicating which character set's encodings to get.</param>
        /// <returns>A collection of <see cref="Encoding"/> class instances for the given character set enumeration.</returns>
        public IEnumerable<System.Text.Encoding> this[CharacterSets characterSets]
        {
            get
            {
                int idx = internalList.FindIndex(f => f.Value == characterSets);
                if (idx != -1)
                {
                    foreach (int encodingNum in internalList[idx].Key)
                    {
                        yield return System.Text.Encoding.GetEncoding(encodingNum);
                    }
                }
            }
        }
    }
}
