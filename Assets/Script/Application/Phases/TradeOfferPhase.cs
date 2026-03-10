using Catan.Shared.Communication.Commands;
using Catan.Core.Models;
using Catan.Application.Controllers;
using Catan.Shared.Data;
using Catan.Application.UIMessages;

namespace Catan.Application.Phases
{
    public class TradeOfferPhase : BasePhase
    {
        private readonly ResourceCostOrStock _cardsDesired = new();

        public TradeOfferPhase(Facade facade) : base(facade) { }

        public override GameResult Handle(object command)
        {
            switch (command)
            {
                case ResourceCardSelectedCommand c:
                    return HandleResourceCardClicked(c);

                case TradeOfferCanceledCommand c:
                    return GameResult.Ok(EnumGamePhases.NormalRound);

                case TradePartnerChosenCommand c:
                    return HandleTradePartnerChosen(c);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleResourceCardClicked(ResourceCardSelectedCommand signal)
        {
            if (signal.IsSelected)
            { 
                _cardsDesired.AddExactAmount(signal.Type, 1);
            }

            if (!signal.IsSelected)
            {
                _cardsDesired.SubtractExactAmount(signal.Type, 1);
            }

            bool hasDesired = Facade.CheckIfCardsSelected(_cardsDesired);

            return GameResult.Ok().AddUIMessage(new DesiredCardsChangedMessage(hasDesired));
        }

        private GameResult HandleTradePartnerChosen(TradePartnerChosenCommand signal)
        {
            var buyerId = signal.PlayerId;
            var result = Facade.UseOfferTrade(buyerId, _cardsDesired);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.SellerId, result.Reason));
            }

            return GameResult.Ok(result.NextPhase);
        }
    }
}