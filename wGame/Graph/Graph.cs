using System;
using System.Collections.Generic;
using wGame.Graph.Visitor;

namespace wGame.Graph
{
    public class Graph<V>
        where V : IComparable, IComparable<V>, IEquatable<V>
    {
        private readonly Dictionary<V, HashSet<V>> _adajacencies
            = new Dictionary<V, HashSet<V>>();

        private bool ContainsVertex(V vertex)
        {
            return _adajacencies.ContainsKey(vertex);
        }

        private bool ContainsEdge(V a, V b)
        {
            return ContainsVertex(a) && _adajacencies[a].Contains(b);
        }

        private void AddVertex(V vertex)
        {
            if (_adajacencies.ContainsKey(vertex))
                throw new Exception(string.Format("Graph already contains vertex {0}", vertex));

            _adajacencies.Add(vertex, new HashSet<V>());
        }

        private void AddEdge(V a, V b)
        {
            if (!ContainsVertex(a))
                AddVertex(a);

            if (!ContainsVertex(b))
                AddVertex(b);

            _adajacencies[a].Add(b);
        }

        private IEnumerable<V> GetNeighbours(V vertex)
        {
            if (!_adajacencies.ContainsKey(vertex))
                return new V[] {};

            return _adajacencies[vertex];
        }

        public void Visit(IVisitor<V> visitor)
        {
            
        }
    }
}