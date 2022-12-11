using DataStructureCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST
{
    public delegate void ProcessData<T>(T data);
    public enum TRAVERSALORDER { PRE_ORDER, IN_ORDER, POST_ORDER }
    public interface I_BST<T>: I_Collection<T> where T: IComparable<T>
    {
        T Find(T data);
        int Height();
        void iterate(ProcessData<T> pd, TRAVERSALORDER to);
    }
}
