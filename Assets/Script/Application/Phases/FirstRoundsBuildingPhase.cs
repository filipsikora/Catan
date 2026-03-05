#nullable enable
using Catan.Application.Controllers;
using Catan.Application.Interfaces;
using Catan.Application.UIMessages;
using Catan.Core.DomainEvents;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class FirstRoundsBuildingPhase : BaseBuildPhase
    {
        private bool villagePlaced = false;
        private bool roadPlaced = false;

        public FirstRoundsBuildingPhase(Facade facade) : base(facade) { }

        public override IUIMessages? Enter()
        {
            return new LogMessageMessage(EnumLogTypes.Info, "Select a vertex to build your free village, then select an edge to build a free road", 4);
        }

        public override GameResult Handle(object command)
        {
            switch (command)
            {
                case VertexClickedCommand c:
                    return HandleVertexClicked(c);

                case EdgeClickedCommand c:
                    return HandleEdgeClicked(c);

                case BuildVillageCommand c:
                    return HandleBuildVillage(c);

                case BuildRoadCommand c:
                    return HandleBuildRoad(c);

                case EndTurnCommand c:
                    return HandleTurnEnded(c);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleVertexClicked(VertexClickedCommand signal)
        {
            if (villagePlaced)
                return GameResult.Fail().AddUIMessage(new LogMessageMessage(EnumLogTypes.Info, "Build a road now"));

            SelectedVertexId = signal.VertexId;

            bool village = true;
            bool road = false;
            bool town = false;

            return GameResult.Ok().AddUIMessage(new VertexHighlightedMessage(signal.VertexId)).AddUIMessage(new BuildOptionsSentMessage(village, road, town));
        }

        private GameResult HandleEdgeClicked(EdgeClickedCommand signal)
        {
            if (!villagePlaced)
                return GameResult.Fail().AddUIMessage(new LogMessageMessage(EnumLogTypes.Info, "Build a village first"));

            if (roadPlaced)
                return GameResult.Fail().AddUIMessage(new LogMessageMessage(EnumLogTypes.Info, "Finish turn now"));

            SelectedEdgeId = signal.EdgeId;

            bool village = false;
            bool road = true;
            bool town = false;

            return GameResult.Ok().AddUIMessage(new EdgeHighlightedMessage(signal.EdgeId)).AddUIMessage(new BuildOptionsSentMessage(village, road, town));
        }

        private GameResult HandleBuildVillage(BuildVillageCommand signal)
        {
            int id = SelectedVertexId.Value;
            var result = Facade.UseBuildInitialVillage(id);

            var selectionMessage = ResetSelection();

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage((new ActionRejectedMessage(result.PlayerId, result.Reason)));
            }

            villagePlaced = true;

            return GameResult.Ok().AddDomainEvent(new VillagePlacedEvent(id)).AddUIMessage(selectionMessage);
        }

        private GameResult HandleBuildRoad(BuildRoadCommand signal)
        {
            int id = SelectedEdgeId.Value;
            var vertexId = Facade.GetLastPlacedVillagePositionId();
            var result = Facade.UseBuildInitialRoad(id, vertexId);

            var selectionMessage = ResetSelection();

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage((new ActionRejectedMessage(result.PlayerId, result.Reason)));
            }

            roadPlaced = true;

            return GameResult.Ok().AddDomainEvent(new RoadPlacedEvent(id)).AddUIMessage(selectionMessage);
        }

        private GameResult HandleTurnEnded(EndTurnCommand signal)
        {
            var result = Facade.UseFinishTurn();

            return GameResult.Ok(result.NextPhase);
        }
    }
}