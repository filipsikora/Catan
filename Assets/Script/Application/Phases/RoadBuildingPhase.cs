using Catan.Application.Controllers;
using Catan.Application.UIMessages;
using Catan.Core.DomainEvents;
using Catan.Shared.Communication.Commands;

namespace Catan.Application.Phases
{
    public class RoadBuildingPhase : BaseBuildPhase
    {
        public RoadBuildingPhase(Facade facade) : base(facade) { }

        public override GameResult Handle(object command)
        {
            switch (command)
            {
                case EdgeClickedCommand c:
                    return HandleEdgeClicked(c);

                case BuildRoadCommand c:
                    return HandleRoadRequested(c);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleEdgeClicked(EdgeClickedCommand signal)
        {
            SelectedEdgeId = signal.EdgeId;

            var village = false;
            var road = true;
            var town = false;

            return GameResult.Ok().AddUIMessage(new EdgeHighlightedMessage(signal.EdgeId)).AddUIMessage(new BuildOptionsSentMessage(village, road, town));

        }

        private GameResult HandleRoadRequested(BuildRoadCommand signal)
        {
            var playerId = Facade.GetCurrentPlayerId();
            var id = SelectedEdgeId.Value;
            var result = Facade.UseBuildFreeRoad(id);

            ResetSelection();

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, result.Reason));
            }

            return GameResult.Ok(result.NextPhase).AddDomainEvent(new RoadPlacedEvent(id, Facade.GetCurrentPlayerId()));
        }
    }
}