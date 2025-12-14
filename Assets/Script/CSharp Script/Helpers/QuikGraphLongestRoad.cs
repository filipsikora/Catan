using QuikGraph;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using System;

namespace Catan
{
    public class QuikGraphLongestRoad
    {
        private readonly UndirectedGraph<Vertex, EdgeAdapter> graph;
        private readonly Player player;


        public QuikGraphLongestRoad(IEnumerable<Vertex> allVertices, IEnumerable<Edge> allEdges, Player player)
        {
            this.player = player;
            graph = new UndirectedGraph<Vertex, EdgeAdapter>();

            foreach (var v in allVertices)
                graph.AddVertex(v);

            foreach (var edge in allEdges)
            {
                if (edge.Owner == player)
                {
                    var a = edge.VertexA;
                    var b = edge.VertexB;

                    if (!graph.ContainsVertex(a)) graph.AddVertex(a);
                    if (!graph.ContainsVertex(b)) graph.AddVertex(b);

                    var adapter = new EdgeAdapter(a, b, edge);
                    graph.AddEdge(adapter);
                }
            }
        }

        private bool VertexAllowed(Vertex v)
        {
            return !v.IsOwned || v.Owner == player;
        }

        private int DFSCount(Vertex current, HashSet<EdgeAdapter> visitedEdges)
        {
            int maxLength = 0;

            foreach (var edge in graph.AdjacentEdges(current))
            {
                if (visitedEdges.Contains(edge))
                    continue;

                var next = edge.Source == current ? edge.Target : edge.Source;

                if (!VertexAllowed(next))
                    continue;

                visitedEdges.Add(edge);
                int length = 1 + DFSCount(next, visitedEdges);
                visitedEdges.Remove(edge);

                if (length > maxLength)
                    maxLength = length;
            }

            return maxLength;
        }

        public int ComputeLongestRoad()
        {
            int longest = 0;

            foreach (var vertex in graph.Vertices)
            {
                int length = DFSCount(vertex, new HashSet<EdgeAdapter>());
                if (length > longest)
                    longest = length;
            }

            return longest;
        }
    }
}
