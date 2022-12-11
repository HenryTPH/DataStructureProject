using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureCommon
{
    public interface I_Collection<T>: IEnumerable<T> where T : IComparable<T>
    {
        ///<summary>
        ///Adds the given data to the collection
        /// </summary>
        /// <para name = "data"> Item to add </para>
        void Add(T data);
        ///<summary>
        ///Remove all items from the collection
        /// </summary>
        void Clear();
        ///<summary>
        ///Determines if the data item is in the collection or not
        /// </summary>
        /// <para name = "data"> Item to look for </para>
        /// <returns>True if it is in the data structure, else false</returns>
        bool Contains(T data);
        ///<summary>
        ///Determines if the data structure is equal to another instance
        /// </summary>
        /// <para name = "other"> The passed in object to compare with this data structure </para>
        /// <returns>True if equal, else false</returns>
        bool Equals(Object other);
        ///<summary>
        ///Remove the data from the collection
        /// </summary>
        /// <para name = "data"> The passed in data to be removed from this data structure </para>
        /// <returns>True if remove successfully, else false</returns>
        bool Remove(T data);
        /// <summary>
        /// A property used to access the number of items in a collection
        /// A property is similar to a getter/setter
        /// </summary>
        int Count
        {
            get;
        }

    }
}
