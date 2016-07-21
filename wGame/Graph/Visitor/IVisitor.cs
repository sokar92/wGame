using System;

namespace wGame.Graph.Visitor
{
    public interface IVisitor<T>
        where T : IComparable, IComparable<T>, IEquatable<T>
    {
        void Visit(Vertex<T> vertex);
    }
}
