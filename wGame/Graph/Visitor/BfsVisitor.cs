using System;

namespace wGame.Graph.Visitor
{
    public class BfsVisitor<T> : IVisitor<T>
        where T : IComparable, IComparable<T>, IEquatable<T>
    {
        public void Visit(Vertex<T> vertex)
        {
            throw new NotImplementedException();
        }
    }
}
