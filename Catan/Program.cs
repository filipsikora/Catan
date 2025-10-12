using System.Drawing;

namespace Catan

{
    internal class Program
    {
        static void Main(string[] args)
        {
            int radius = 2;
            float hexSize = 1.0f;

            GameState game = new GameState(new HexMap(radius));

            game.Map.GenerateHexesInAxial();

            game.Map.ConvertHexToPixel(hexSize);

            game.Map.GenerateVerticesInPixel(hexSize);

            game.Map.GenerateEdgesInPixels(hexSize);

            game.Map.DrawBoard(hexSize);

            Console.WriteLine(game.Map.Edges.Count());

            var vertex = game.Map.Vertices.Values.First();  // bierzesz pierwszy wierzchołek    
            var neighbors = vertex.NeighbourVertices;       // to zwraca listę sąsiadów

            foreach (var n in neighbors)
            {
                Console.WriteLine($"Sąsiad wierzchołka ({vertex.X}, {vertex.Y}) -> ({n.X}, {n.Y})");
            }
        }
    }
}