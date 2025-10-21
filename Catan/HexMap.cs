using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan
{
    public class HexMap
    {
        public Dictionary<(int q, int r), HexTile> HexDictionary { get; } = new();

        public List<HexTile> HexList { get; set; } = new List<HexTile>();

        public Dictionary<(int q, int r), Vertex> Vertices { get; } = new();

        public List<Vertex> VertexList { get; set; } = new List<Vertex>();

        public List<Edge> Edges { get; set; } = new List<Edge>();

        public Vertex? GetVertexById(int id) => VertexList.FirstOrDefault(v => v.Id == id);

        public Edge? GetEdgeById(int id) => Edges.FirstOrDefault(e => e.Id == id);


        private static readonly (int dq, int dr)[] Offsets = new (int, int)[]
        {
            ( 1,  0), ( 1, -1), ( 0, -1),
            (-1,  0), (-1,  1), ( 0,  1)
        };

        public int Radius { get; }


        public HexMap(int radius = 2)
        {
            Radius = radius;
        }


        public void GenerateHexesInAxial()
        {
            int hexID = 1;
            for (int r = -Radius; r <= Radius; r++)
            {
                int q1 = Math.Max(-Radius, -r - Radius);
                int q2 = Math.Min(Radius, -r + Radius);
                for (int q = q1; q <= q2; q++)
                {
                    HexTile h = new HexTile(q, r, hexID);
                    HexDictionary[(q, r)] = h;
                    HexList.Add(h);
                    hexID++;
                }
            }
        }

        public IEnumerable<HexTile> GetNeighbors(HexTile hex)
        {
            foreach (var (dq, dr) in Offsets)
            {
                var key = (hex.Q + dq, hex.R + dr);
                if (HexDictionary.TryGetValue(key, out var neigh))
                    yield return neigh;
            }
        }

        public static (float x, float y) AxialToPixel(int q, int r, float size)
        {
            double sqrt3 = Math.Sqrt(3);
            float x = (float)(sqrt3 * (q + r / 2.0) * size);
            float y = (float)(1.5 * r * size);
            return (x, y);
        }

        public static List<(float x, float y)> GetHexCorners(int q, int r, float size)
        {
            var (cx, cy) = AxialToPixel(q, r, size);
            var corners = new List<(float, float)>(6);
            for (int i = 0; i < 6; i++)
            {
                double angle_deg = 60 * i - 30;
                double angle_rad = Math.PI / 180.0 * angle_deg;
                float vx = cx + size * (float)Math.Cos(angle_rad);
                float vy = cy + size * (float)Math.Sin(angle_rad);
                corners.Add((vx, vy));
            }
            return corners;
        }

        public void ConvertHexToPixel(float size)
        {
            foreach (var h in HexDictionary.Values)
            {
                (h.X, h.Y) = AxialToPixel(h.Q, h.R, size);
            }
        }

        public void GenerateVerticesInPixel(float size, int quant = 1000)
        {
            Vertices.Clear();
            foreach (var hex in HexDictionary.Values)
            {
                var corners = GetHexCorners(hex.Q, hex.R, size);

                foreach (var (x, y) in corners)
                {
                    int kx = (int)Math.Round(x * quant);
                    int ky = (int)Math.Round(y * quant);
                    var key = (kx, ky);

                    if (!Vertices.ContainsKey(key))
                    {
                        Vertices[key] = new Vertex(x, y, quant);
                    }
                    if (!Vertices[key].AdjacentHexTiles.Contains(hex))
                        Vertices[key].AdjacentHexTiles.Add(hex);
                } 
            }
        }

        public void AddVerticesToHex(float size, int quant = 1000)
        {
            foreach (var hex in HexDictionary.Values)
            {
                var corners = GetHexCorners(hex.Q, hex.R, size);

                foreach (var (x, y) in corners)

                {
                    int kx = (int)Math.Round(x * quant);
                    int ky = (int)Math.Round(y * quant);
                    var key = (kx, ky);

                    var vertex = Vertices[key];

                    if (!hex.AdjacentVertices.Contains(vertex))
                    {
                        hex.AdjacentVertices.Add(vertex);
                    }
                }
            }
        }

        public void SortAndIDVertices()
        {

            foreach (var entry in Vertices)
            {
                VertexList.Add(entry.Value);
            }

            VertexList = VertexList.OrderBy(v => v.Y).ThenBy(v => v.X).ToList();

            for (int index = 0; index < VertexList.Count; index++)
            {
                VertexList[index].Id = index + 1;
            }

        }

        public void SortAndIDEdges()
        {
                Edges = Edges.OrderBy(e => e.Y).ThenBy(e => e.X).ToList();

                for (int index = 0; index < Edges.Count; index++)
                {
                    Edges[index].Id = index + 1;
                }
        }

        public void GenerateEdgesInPixels(float size, int quant = 1000)
        {
            Edges.Clear();
            foreach (var hex in HexDictionary.Values)
            {
                List<(float x, float y)> corners = GetHexCorners(hex.Q, hex.R, size);

                    for (int index = 0; index < corners.Count(); index++)
                    {
                        var (x1, y1) = corners[index];
                        var (x2, y2) = corners[(index + 1) % 6];

                        int kx1 = (int)Math.Round(x1 * quant);
                        int ky1 = (int)Math.Round(y1 * quant);

                        int kx2 = (int)Math.Round(x2 * quant);
                        int ky2 = (int)Math.Round(y2 * quant);

                        Vertex vertex1 = Vertices[(kx1, ky1)];
                        Vertex vertex2 = Vertices[(kx2, ky2)];

                        if (!Edges.Any(e =>
                            (e.VertexA == vertex1 && e.VertexB == vertex2) ||
                            (e.VertexA == vertex2 && e.VertexB == vertex1)))
                        {
                            Edges.Add(new Edge(vertex1, vertex2));
                        }
                    }
                }
            }

        /*

        public void DrawVertices(float minX, float minY, float maxX, float maxY, int width, int height, char[,] grid)
        {

            char letter = 'A';

            foreach (var v in VertexList)
            {
                char displayChar;

                if (v.IsOwned)
                {

                    if (v.Owner.Name == "Player1") displayChar = v.Building is BuildingTown ? 'A' : 'a';
                    else if (v.Owner.Name == "Player2") displayChar = v.Building is BuildingTown ? 'B' : 'b';
                    else if (v.Owner.Name == "Player3") displayChar = v.Building is BuildingTown ? 'C' : 'c';
                    else displayChar = '?';

                }

                else
                {
                    displayChar = letter;
                    letter++;
                }

                int gx = (int)((v.X - minX) / (maxX - minX) * (width - 1));
                int gy = (int)((v.Y - minY) / (maxY - minY) * (height - 1));
                grid[gy, gx] = displayChar;
            }
        }

        public void DrawEdges(float minX, float minY, float maxX, float maxY, int width, int height, char[,] grid)
        {
            foreach (var e in Edges)
            {

                char letter = (char)('0' + (e.Id % 10));
                Console.WriteLine(letter);

                if (e.IsOwned)
                {
                    if (e.Owner.Name == "Player1")
                    {
                        letter = '1';
                    }
                    if (e.Owner.Name == "Player2")
                    {
                        letter = '2';
                    }
                    if (e.Owner.Name == "Player3")
                    {
                        letter = '3';
                    }
                }
                    int gx = (int)((e.X - minX) / (maxX - minX) * (width - 1));
                int gy = (int)((e.Y - minY) / (maxY - minY) * (height - 1));
                grid[gy, gx] = letter;
            }
        }

        public void DrawHexes(float minX, float minY, float maxX, float maxY, int width, int height, char[,] grid)
        {
            char letter = '*';
            foreach (var h in HexDictionary.Values)
            {
                int gx = (int)((h.X - minX) / (maxX - minX) * (width - 1));
                int gy = (int)((h.Y - minY) / (maxY - minY) * (height - 1));
                grid[gy, gx] = letter;
            }
        }

        public void DrawBoard(float size)
        {
            float minX = Vertices.Values.Min(v => v.X);
            float maxX = Vertices.Values.Max(v => v.X);
            float minY = Vertices.Values.Min(v => v.Y);
            float maxY = Vertices.Values.Max(v => v.Y);

            int width = 40;
            int height = 20;

            char[,] grid = new char[height, width];
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    grid[y, x] = '.';

            DrawVertices(minX, minY, maxX, maxY, width, height, grid);

            DrawHexes(minX, minY, maxX, maxY, width, height, grid);

            DrawEdges(minX, minY, maxX, maxY, width, height, grid);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                    Console.Write(grid[y, x]);
                Console.WriteLine();
            }
        }

        */
    }
}
