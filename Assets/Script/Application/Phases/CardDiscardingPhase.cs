using Catan.Application.Controllers;
using Catan.Application.Interfaces;
using Catan.Application.UIMessages;
using Catan.Core.Models;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class CardDiscardingPhase : BasePhase
    {
        private ResourceCostOrStock _resourcesSelected = new();
        private int _currentDiscardingPlayerId;

        public CardDiscardingPhase(Facade facade) : base(facade) { }

        public override IUIMessages Enter()
        {
            _currentDiscardingPlayerId = Facade.GetNextToDiscardId();
            _resourcesSelected = new ResourceCostOrStock();

            return new PlayerSelectedToDiscardMessage(_currentDiscardingPlayerId);
        }

        public override GameResult Handle(object command)
        {
            switch (command)
            {
                case ResourceCardSelectedCommand c:
                    return HandleResourceSelectionChanged(c);

                case DiscardingAcceptedCommand c:
                    return HandleDiscardingAccepted(c);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleResourceSelectionChanged(ResourceCardSelectedCommand signal)
        {
            if (!signal.IsSelected)
            {
                _resourcesSelected.AddExactAmount(signal.Type, 1);
            }

            else
            {
                _resourcesSelected.SubtractExactAmount(signal.Type, 1);
            }

            var canDiscard = Facade.CanPlayerDiscard(_resourcesSelected, _currentDiscardingPlayerId);

            return GameResult.Ok().AddUIMessage(new SelectionChangedMessage(canDiscard));
        }

        private GameResult HandleDiscardingAccepted(DiscardingAcceptedCommand signal)
        {
            var result = Facade.UseDiscard(_currentDiscardingPlayerId, _resourcesSelected);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(_currentDiscardingPlayerId, result.Reason));
            }

            return ProceedToNextPlayer();
        }

        private GameResult ProceedToNextPlayer()
        {
            if (Facade.GetNextPhaseAfterDiscarding() != null)
            {
                return GameResult.Ok(EnumGamePhases.RobberPlacing);
            }

            _currentDiscardingPlayerId = Facade.GetNextToDiscardId();
            _resourcesSelected = new ResourceCostOrStock();

            return GameResult.Ok().AddUIMessage(new PlayerSelectedToDiscardMessage(_currentDiscardingPlayerId));
        }
    }
}