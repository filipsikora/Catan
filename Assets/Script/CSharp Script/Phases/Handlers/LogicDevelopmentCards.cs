using Catan.Core.Engine;
using Catan.Core.Helpers;
using Catan.Core.Models;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Core.Phases.Handlers
{
    public class LogicDevelopmentCards : BasePhaseLogic
    {
        public LogicDevelopmentCards(GameState game, EventBus bus) : base(game, bus) { }

        public override void Enter() { }

        public override void Exit() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case DevelopmentCardClickedCommand c:
                    HandleDevCardClicked(c);
                    break;

                case DevelopmentCardsCanceledCommand c:
                    HandleDevCardsCanceled(c);
                    break;
            }
        }

        public void HandleDevCardClicked(DevelopmentCardClickedCommand signal)
        {
            DevelopmentCard cardModel = Game.DevelopmentCardsDeckAll.Find(d => d.ID == signal.DevelopmentCardId);

            UseCard(cardModel);
        }

        public void HandleDevCardsCanceled(DevelopmentCardsCanceledCommand signal)
        {
            bool afterRoll = Game.GetAfterRoll();

            Bus.Publish(new DevelopmentCardsCompletedEvent(afterRoll));
        }

        private void UseCard(DevelopmentCard card)
        {
            bool afterRoll = Game.GetAfterRoll();
            Player player = Game.GetCurrentPlayer();

            var result = Conditions.CanPlayDevCard(player, card, Game.GetAfterRoll());

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(player.ID, result.Reason));

                return;
            }

            Game.CurrentPlayer?.DevelopmentCardsByID.Remove(card.ID);

            switch (card.Type)
            {
                case EnumDevelopmentCardTypes.Knight:
                    Game.UseKnight(Game.CurrentPlayer);
                    Bus.Publish(new ProceedToRobberPlacingEvent());
                    break;

                case EnumDevelopmentCardTypes.Monopoly:
                    Bus.Publish(new DevelopmentCardsToMonopolyCardEvent());
                    break;

                case EnumDevelopmentCardTypes.RoadBuilding:
                    Bus.Publish(new DevelopmentCardsToRoadBuildingEvent());
                    break;

                case EnumDevelopmentCardTypes.VictoryPoint:
                    Game.UseVictoryPoint(Game.CurrentPlayer);
                    Bus.Publish(new ReturnToNormalRoundEvent());
                    break;

                case EnumDevelopmentCardTypes.YearOfPlenty:
                    Bus.Publish(new DevelopmentCardsToYearOfPlentyUsedEvent());
                    break;
            }
        }
    }
}
