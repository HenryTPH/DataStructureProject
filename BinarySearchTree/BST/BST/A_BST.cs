using DataStructureCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST
{
    public abstract class A_BST<T>: A_Collection<T>, I_BST<T>
        where T : IComparable<T>
    {
        #region Attributes
        protected Node<T> nRoot;
        protected int iCount = 0;
        #endregion
        public override int Count { get { return iCount; } }
        public abstract T Find(T data);
        public abstract int Height();
        public abstract void iterate(ProcessData<T> pd, TRAVERSALORDER to);
    }
}
