using System;
using System.Collections.Generic;

namespace ScriptNotepad.Editor.EntityHelpers.DataHolders
{
    /// <summary>
    /// A class to hold a get indexer for type of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The class which is to be indexed.</typeparam>
    public class DataHolderIndexer<T> where T: class
    {
        /// <summary>
        /// Gets the values for the this indexer.
        /// </summary>
        /// <value>The values for the this indexer.</value>
        private Dictionary<int, T> Values { get; } = new();

        /// <summary>
        /// Gets the <typeparamref name="T"/> with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The <typeparamref name="T"/> instance with the specified key.</returns>
        public T this[int key]
        {
            get
            {
                if (!Values.ContainsKey(key))
                {
                    Values.Add(key, Activator.CreateInstance<T>());
                }

                return Values[key];
            }
        }

        /// <summary>
        /// Removes the <typeparamref name="T"/> instance with a specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(int key)
        {
            Values.Remove(key);
        }
    }
}
