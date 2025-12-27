using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Core.Phases.Handlers
{
    public class LogicDevelopmentCards : BasePhaseLogic
    {
        private List<int> _playerCardsById;

        public LogicDevelopmentCards(GameState game, EventBus bus, List<int> playerCardsById) : base(game, bus)
        {
            _playerCardsById = playerCardsById;
        }

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

                case RequestDevelopmentCardsViewCommand c:
                    HandleDevCardsViewRequested(c);
                    break;
            }
        }

        public void HandleDevCardClicked(DevelopmentCardClickedCommand signal)
        {
            DevelopmentCard cardModel = Game.DevelopmentCardsDeckAll.Find(d => d.ID == signal.DevelopmentCardId);

            if (!cardModel.IsNew)
            {
                UseCard(cardModel);
            }
        }

        public void HandleDevCardsCanceled(DevelopmentCardsCanceledCommand signal)
        {
            bool afterRoll = Game.GetAfterRoll();

            Bus.Publish(new DevelopmentCardsCompletedEvent(afterRoll));
        }

        public void HandleDevCardsViewRequested(RequestDevelopmentCardsViewCommand signal)
        {
            Bus.Publish(new DevelopmentCardsShownEvent(_playerCardsById, Game.GetAfterRoll()));
        }

        private void UseCard(DevelopmentCard card)
        {
            card.IsUsed = true;
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
