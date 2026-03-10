using Catan.Application.Controllers;
using Catan.Application.Interfaces;
using Catan.Application.UIMessages;
using Catan.Core.Results;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Data;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Application.Phases
{
    public class BeforeRollPhase : BasePhase
    {
        public BeforeRollPhase(Facade facade) : base(facade) { }
        
        public override GameResult Handle(object command)
        {
            switch (command)
            {
                case RollDiceCommand c:
                    return HandleRollDice(c);

                case ShowDevelopmentCardsCommand c:
                    return GameResult.Ok(EnumGamePhases.DevelopmentCards);

                case VertexClickedCommand:
                case EdgeClickedCommand:
                case HexClickedCommand:
                    return HandleInvalidClick();

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleRollDice(RollDiceCommand signal)
        {
            var result = Facade.UseRollDice();

            var logList = GetLogList(result);

            return GameResult.Ok(result.NextPhase.Value).AddUIMessagesList(logList);
        }

        private GameResult HandleInvalidClick()
        {
            var playerId = Facade.GetCurrentPlayerId();

            return GameResult.Ok().AddUIMessage(new ActionRejectedMessage(playerId, ConditionFailureReason.NotRolledYet));
        }

        private List<IUIMessages> GetLogList(ResultRollDice result)
        {
            List<IUIMessages> logList = new();

            var byPlayer = result.Distributions.GroupBy(d => new { d.PlayerId, d.PlayerName });

            foreach (var playerGroup in byPlayer)
            {
                var resources = playerGroup.GroupBy(d => d.Type).Select(g => $"{g.Sum(x => x.Granted)} {g.Key.ToString().ToLower()}").ToList();

                if (resources.Count == 0)
                    continue;

                var text = $"{playerGroup.Key.PlayerName}: {string.Join(", ", resources)}";

                logList.Add(new LogMessageMessage(EnumLogTypes.Info, text));
            }

            return logList;
        }
    }
}