using Catan.Application.CommandHandlers;
using Catan.Core.Engine;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;
using TMPro.EditorUtilities;

namespace Catan.Core.Phases.Handlers
{
    public class LogicFirstRoundsBuilding : BaseBuildPhaseLogic
    {
        BuildFreeVillageHandler _handlerVillage;
        BuildFreeRoadHandler _handlerRoad;
        FinishTurnHandler _handlerTurn;

        private bool villagePlaced = false;
        private bool roadPlaced = false;

        public LogicFirstRoundsBuilding(GameState game, EventBus bus) : base(game, bus)
        {
            _handlerVillage = new BuildFreeVillageHandler(game);
            _handlerRoad = new BuildFreeRoadHandler(game);
            _handlerTurn = new FinishTurnHandler(game);
        }

        public override void Enter()
        {
            Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, "Select a vertex to build your free village, then select an edge to build a free road", 4));
        }

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
                    HandleBuildVillage(c);
                    break;

                case BuildRoadCommand c:
                    HandleBuildRoad(c);
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

        private void HandleBuildVillage(BuildVillageCommand signal)
        {
            var player = Game.GetCurrentPlayer();
            int id = SelectedVertexId.Value;
            var vertex = Game.Map.GetVertexById(id);
            var result = _handlerVillage.Handle(player.ID, vertex);

            ResetSelection();

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(player.ID, result.Reason));
                return;
            }

            villagePlaced = true;
            Bus.Publish(new VillagePlacedEvent(id));
        }

        private void HandleBuildRoad(BuildRoadCommand signal)
        {
            var player = Game.GetCurrentPlayer();
            int id = SelectedEdgeId.Value;
            var edge = Game.Map.GetEdgeById(id);
            var vertex = Game.GetLastPlacedVillagePosition;

            var result = _handlerRoad.Handle(player.ID, edge, vertex);

            ResetSelection();

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(Game.CurrentPlayer.ID, result.Reason));
                return;
            }

            roadPlaced = true;
            Bus.Publish(new RoadPlacedEvent(id));
        }

        private void HandleTurnEnded(EndTurnCommand signal)
        {
            var result = _handlerTurn.Handle(Game.GetCurrentPlayer());

            Bus.Publish(new FirstRoundsBuildingCompletedEvent(result.InitialRoundsRemaining));
        }
    }
}