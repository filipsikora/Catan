#nullable enable
using System.Drawing;   

namespace Catan

{

    /*
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

            game.Map.AddVerticesToHex(hexSize);

            game.Map.GenerateEdgesInPixels(hexSize);

            game.Map.SortAndIDVertices();

            game.Map.SortAndIDEdges();

            game.Map.DrawBoard(hexSize);

            game.ReadyBoard();

            Console.WriteLine(game.Map.HexList.Count());

            Console.WriteLine(game.Map.Edges.Count());

            Console.WriteLine(game.Map.VertexList.Count());

            game.ReadyPlayer(2);


            for (int i = 0; i < 2; i++)
            {

                foreach (Player player in game.PlayerList)
                {

                    game.FirstVillagesAndRoads(player);

                    game.Map.DrawBoard(hexSize);

                }
            }
                            
            while (!game.AnyoneHasTenPoints)
            {

                foreach (Player player in game.PlayerList)
                {

                    game.RollAndServePlayers();

                    game.LetPlayerChoose(player);

                    player.Resources.ShowResources();

                    game.WinCheck(player);

                    game.Map.DrawBoard(hexSize);

                }
            }


            Console.WriteLine("chuj");

        }
    }

    */

}

// test
// dostawane surowce za wioski
// next turn