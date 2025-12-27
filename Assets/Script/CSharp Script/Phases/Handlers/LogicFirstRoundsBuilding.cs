using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;

namespace Catan.Core.Phases.Handlers
{
    public class LogicFirstRoundsBuilding : BaseBuildPhaseLogic
    {
        private bool villagePlaced = false;
        private bool roadPlaced = false;

        public LogicFirstRoundsBuilding(GameState game, EventBus bus) : base(game, bus) { }

        public override void Enter() { }

        public override void Exit() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case VertexClickedCommand c:
                    HandleVertexClicked(c);
                    break;

                case EdgeClickedCommand c:
                    HandleEdgeClicked(c);
                    break;

                case BuildVillageCommand c:
                    HandleVillageRequested(c);
                    break;

                case BuildRoadCommand c:
                    HandleRoadRequested(c);
                    break;

                case EndTurnCommand c:
                    HandleTurnEnded(c);
                    break;

            }
        }

        private void HandleVertexClicked(VertexClickedCommand signal)
        {
            if (villagePlaced)
                return;

            SelectedVertexId = signal.VertexId;

            bool village = true;
            bool road = false;
            bool town = false;

            Bus.Publish(new VertexHighlightedEvent(signal.VertexId));
            Bus.Publish(new BuildOptionsSentEvent(village, road, town));
        }

        private void HandleEdgeClicked(EdgeClickedCommand signal)
        {
            if (!villagePlaced)
                return;

            if (roadPlaced)
                return;

            SelectedEdgeId = signal.EdgeId;

            bool village = false;
            bool road = true;
            bool town = false;

            Bus.Publish(new EdgeHighlightedEvent(signal.EdgeId));
            Bus.Publish(new BuildOptionsSentEvent(village, road, town));
        }

        private void HandleVillageRequested(BuildVillageCommand signal)
        {
            var player = Game.GetCurrentPlayer();
            int id = SelectedVertexId.Value;
            var vertex = Game.Map.GetVertexById(id);
            var result = Game.BuildFreeVillage(player, vertex);

            ResetSelection();

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(player.ID, result.Reason));
                return;
            }

            if (player.Points == 2)
            {
                GiveResourcesForSecondVillage(player, vertex);
            }

            villagePlaced = true;
            Bus.Publish(new VillagePlacedEvent(id));
        }

        private void HandleRoadRequested(BuildRoadCommand signal)
        {
            int id = SelectedEdgeId.Value;
            var edge = Game.Map.GetEdgeById(id);

            if (!edge.IsNextToVertex(Game.LastPlacedVillagePosition))
            {
                Bus.Publish(new LogMessageEvent(Shared.Data.EnumLogTypes.Error, "Need to place this road next to a village placed in this turn"));
                return;
            }

            ResetSelection();

            var result = Game.BuildFreeRoad(Game.CurrentPlayer, edge);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(Game.CurrentPlayer.ID, result.Reason));
                return;
            }

            roadPlaced = true;
            Bus.Publish(new RoadPlacedEvent(id));
        }

        private void GiveResourcesForSecondVillage(Player player, Vertex vertex)
        {
            foreach (HexTile hex in vertex.AdjacentHexTiles)
            {
                var resourceType = hex.GetResourceType();

                if (resourceType.HasValue)
                {
                    vertex.Owner?.Resources.AddExactAmount(resourceType.Value, 1);
                    Game.Bank.SubtractExactAmount(resourceType.Value, 1);
                }
            }
        }

        private void HandleTurnEnded(EndTurnCommand signal)
        {
            Game.EndTurn();

            bool firstRoundsLeft = Game.FirstRoundsIndices.Count > 0;

            Bus.Publish(new FirstRoundsBuildingCompletedEvent(firstRoundsLeft));
        }
    }
}