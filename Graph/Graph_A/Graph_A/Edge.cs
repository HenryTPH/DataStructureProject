using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_A
{
    public class Edge<T>: IComparable<Edge<T>>
        where T : IComparable<T>
    {
        #region Attributes
        private Vertex<T> from;
        private Vertex<T> to;
        private bool isWeighted;
        private double weight;
        #endregion

        #region Properties
        public Vertex<T> From { get { return from; } }
        public Vertex<T> To { get { return to; } }
        public bool IsWeighted => isWeighted;
        public double Weight => weight;
        #endregion

        #region Constructors
        public Edge(Vertex<T> from, Vertex<T> to)
        {
            this.from = from;
            this.to = to;
            this.weight = double.PositiveInfinity;
            this.isWeighted = false;
        }
        public Edge(Vertex<T> from, Vertex<T> to, double weight)
        {
            this.from = from;
            this.to = to;
            this.weight = weight;
            this.isWeighted = true;
        }
        public Edge(Vertex<T> from, Vertex<T> to, bool isWeighted, double weight): this(from, to)
        {
            this.weight = weight;
            this.isWeighted = isWeighted;
        }
        #endregion

        public int CompareTo(Edge<T> other)
        {
            int result = 0;
            //Don't compare weight unless both edges are weighted
            if(this.isWeighted && other.isWeighted)
            {
                result = this.Weight.CompareTo(other.Weight);
            }
            //What if the edge have the same weight
            if(result == 0)
            {
                //Compare the From vertices
                result = from.CompareTo(other.from);
                //If the From vertices is equal
                if(result == 0)
                {
                    //Compare the To vertices
                    result = to.CompareTo(other.to);
                }
            }
            return result;
        }
        public override bool Equals(object obj)
        {
            return this.CompareTo((Edge<T>)obj) == 0;
        }
        public override string ToString()
        {
            return from + " to " + to + (isWeighted ? " , W = " + weight : "");
        }
    }
}
