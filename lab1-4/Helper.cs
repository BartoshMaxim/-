using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_4
{
    public static class Helper
    {
        /// <summary>
        /// Extension method that helps to write the contents of a generic Dictionary to a string, ordered by it's values and 
        /// printing the key and it's value between brackets.
        /// </summary>
        /// <typeparam name="TKey">Generic key</typeparam>
        /// <typeparam name="TValue">Generic value type</typeparam>
        /// <param name="dictionary">The dictionary</param>
        /// <exception cref="ArgumentNullException">Throws an argument null exception if the provided dictionary is null</exception>
        /// <returns></returns>
        public static string PrintFrequencies<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            var items = from kvp in dictionary
                        orderby kvp.Value
                        select kvp.Key + " [" + kvp.Value + "]";

            return string.Join(Environment.NewLine, items);
        }

        /// <summary>
        /// Determines whether a given Huffman node is a leaf or not.
        /// A node is considered to be a leaf when it has no childnodes
        /// </summary>
        /// <param name="node">A huffman node</param>
        /// <returns>True if no children are left, false otherwise</returns>
        public static bool IsLeaf(this HuffmanNode node)
        {
            return (null == node.Left && null == node.Right);
        }
    }
}
