using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan
{
    class HexMap
    {
        public Dictionary<(int q, int r), HexTile> HexDictionary { get; } = new();

        public Dictionary<(int q, int r), Vertex> Vertices { get; } = new();

        public List<Edge> Edges { get; set; } = new();

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
            int thisHex = 0;
            for (int q = -Radius; q <= Radius; q++)
            {
                int id = thisHex;
                int r1 = Math.Max(-Radius, -q - Radius);
                int r2 = Math.Min(Radius, -q + Radius);
                for (int r = r1; r <= r2; r++)
                {
                    HexTile h = new HexTile(q, r, thisHex);
                    HexDictionary[(q, r)] = h;
                    thisHex++;
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

        public void DrawVertices(float minX, float minY, float maxX, float maxY, int width, int height, char[,] grid)
        {
            foreach (var v in Vertices.Values)
            {
                int gx = (int)((v.X - minX) / (maxX - minX) * (width - 1));
                int gy = (int)((v.Y - minY) / (maxY - minY) * (height - 1));
                grid[gy, gx] = 'X';
            }
        }

        public void DrawEdges(float minX, float minY, float maxX, float maxY, int width, int height, char[,] grid)
        {
            foreach (var e in Edges)
            {
                int gx = (int)((e.X - minX) / (maxX - minX) * (width - 1));
                int gy = (int)((e.Y - minY) / (maxY - minY) * (height - 1));
                grid[gy, gx] = '+';
            }
        }

        public void DrawHexes(float minX, float minY, float maxX, float maxY, int width, int height, char[,] grid)
        {
            foreach (var h in HexDictionary.Values)
            {
                int gx = (int)((h.X - minX) / (maxX - minX) * (width - 1));
                int gy = (int)((h.Y - minY) / (maxY - minY) * (height - 1));
                grid[gy, gx] = 'H';
            }
        }

        public void ConvertHexToPixel(float size)
        {
            foreach (var h in HexDictionary.Values)
            {
                (h.X, h.Y) = AxialToPixel(h.Q, h.R, size);
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
    }
}
