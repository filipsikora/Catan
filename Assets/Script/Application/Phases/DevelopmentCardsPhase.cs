using Catan.Application.Controllers;
using Catan.Application.UIMessages;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class DevelopmentCardsPhase : BasePhase
    {
        public DevelopmentCardsPhase(Facade facade) : base(facade) { }

        public override GameResult Handle(object command)
        {
            switch (command)
            {
                case DevelopmentCardClickedCommand c:
                    return HandlePlayDevCard(c);

                case DevelopmentCardsCanceledCommand c:
                    return GameResult.Ok(Facade.GetNextPhaseFromAfterRoll());

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandlePlayDevCard(DevelopmentCardClickedCommand signal)
        {
            var result = Facade.UseDevCard(signal.DevelopmentCardId);
            var playerId = Facade.GetCurrentPlayerId();

            if (!result.Success)
            {
                if (result.Reason == ConditionFailureReason.NoBuildingsAvailable)
                    return GameResult.Fail().AddUIMessage(new LogMessageMessage(EnumLogTypes.Info, "No roads available"));

                if (result.Reason == ConditionFailureReason.NotEnoughResourcesInBank)
                    return GameResult.Fail().AddUIMessage(new LogMessageMessage(EnumLogTypes.Info, "No resources in bank"));

                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, result.Reason));
            }

            return GameResult.Ok(result.NextPhase);
        }
    }
}