using System;
using System.Collections.Generic;
using System.Linq;

namespace wGame.Graph
{
    public class Vertex<T> : IComparable, IComparable<Vertex<T>>, IEquatable<Vertex<T>>
        where T : IComparable, IComparable<T>, IEquatable<T>
    {
        private T Value { get; set; }
        private Func<T, IEnumerable<T>> EdgeProducer { get; set; }

        public Vertex(T val) : this(val, null) {}

        public Vertex(T val, Func<T,IEnumerable<T>> edgeProducer)
        {
            if (ReferenceEquals(val, null))
                throw new ArgumentNullException("val", "Cannot create a vertex with null value");

            Value = val;

            if (ReferenceEquals(edgeProducer, null))
                EdgeProducer = x => new T[] {};
            else
                EdgeProducer = edgeProducer;
        }

        public IEnumerable<Vertex<T>> Neighbours
        {
            get { return EdgeProducer(Value).Select(x => new Vertex<T>(x)); }
        } 

        public int CompareTo(Vertex<T> other)
        {
            return Value.CompareTo(other.Value);
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as Vertex<T>);
        }

        public bool Equals(Vertex<T> other)
        {
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Vertex<T>);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Vertex<T> a, Vertex<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Equals(b);
        }

        public static bool operator !=(Vertex<T> a, Vertex<T> b)
        {
            return !(a == b);
        }

        public static bool operator <(Vertex<T> a, Vertex<T> b)
        {
            if (ReferenceEquals(a, null))
                return !ReferenceEquals(b, null);

            return a.CompareTo(b) < 0;
        }

        public static bool operator >(Vertex<T> a, Vertex<T> b)
        {
            return b < a;
        }

        public static bool operator <=(Vertex<T> a, Vertex<T> b)
        {
            return (a == b) || (a < b);
        }

        public static bool operator >=(Vertex<T> a, Vertex<T> b)
        {
            return b <= a;
        }
    }
}
