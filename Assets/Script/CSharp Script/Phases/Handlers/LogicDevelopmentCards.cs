using Catan.Application.CommandHandlers;
using Catan.Core.Engine;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Core.Phases.Handlers
{
    public class LogicDevelopmentCards : BasePhaseLogic
    {
        PlayDevCardHandler _handler;
        public LogicDevelopmentCards(GameState game, EventBus bus) : base(game, bus)
        {
            _handler = new PlayDevCardHandler(game);
        }

        public override void Enter() { }

        public override void Exit() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case DevelopmentCardClickedCommand c:
                    HandlePlayDevCard(c);
                    break;

                case DevelopmentCardsCanceledCommand c:
                    HandleDevCardsCanceled(c);
                    break;
            }
        }

        private void HandlePlayDevCard(DevelopmentCardClickedCommand signal)
        {
            var (result, card) = _handler.Handle(signal.DevelopmentCardId);
            var player = Game.GetCurrentPlayer();

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(player.ID, result.Reason));

                return;
            }

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

        public void HandleDevCardsCanceled(DevelopmentCardsCanceledCommand signal)
        {
            bool afterRoll = Game.GetAfterRoll();

            Bus.Publish(new DevelopmentCardsCompletedEvent(afterRoll));
        }
    }
}