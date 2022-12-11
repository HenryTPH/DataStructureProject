using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructureCommon;

namespace LinkedList
{
    public interface I_List<T>: I_Collection<T> where T : IComparable<T>
    {
        /// <summary>
        /// Returns the element at a particular index
        /// </summary>
        /// <para name="index"> The index of the item to find </para>
        /// <returns> The item at index </returns>
        T ElementAt(int index);
        /// <summary>
        /// Given a data item, find the first instance and return its index
        /// </summary>
        /// <para name="data"> The data of the item to find </para>
        /// <returns> The index of the item if found, -1 if not found </returns>
        int IndexOf(T data);
        /// <summary>
        /// Insert an item at a particular index
        /// </summary>
        /// <para name="data"> The data item to insert </para>
        /// <para name="index"> The index of the item to be inserted </para>
        void Insert(int index, T data);
        /// <summary>
        /// Remove an item at a particular given index
        /// </summary>
        /// <para name="index"> The index of the item to be removed  </para>
        /// <returns> The item that is removed from the data structured </returns>
        T RemoveAt(int index);
        /// <summary>
        /// Replace an item at a particular given index with the given data
        /// </summary>
        /// <para name="index"> The index of the item to be replaced </para>
        /// <para name="data"> The data item to replace existing one </para>
        /// <returns> The item that was replaced </returns>
        T ReplaceAt(int index, T data);
    }
}
