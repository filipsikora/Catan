using Catan.Application.Controllers;
using Catan.Application.Interfaces;
using Catan.Application.UIMessages;
using Catan.Core.DomainEvents;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Application.Phases
{
    public class RobberPlacingPhase : BasePhase
    {
        private bool _clickableHexes = true;

        public RobberPlacingPhase(Facade facade) : base(facade) { }

        public override IUIMessages Enter()
        {
            return new LogMessageMessage(EnumLogTypes.Info, "Choose a hex to block");
        }

        public override GameResult Handle(object command)
        {
            switch (command)
            {
                case HexClickedCommand c:
                    return HandleHexClicked(c);

                case VictimChosenCommand c:
                    return VictimChosen(c);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleHexClicked(HexClickedCommand signal)
        {
            if (!_clickableHexes)
                return GameResult.Fail();

            var hexId = signal.HexId;
            var result = Facade.UseBlockHex(hexId);

            if (!result.Success)
                return GameResult.Fail();


            var gameResult = HandleVictimsAfterBlocking(result.CanSteal, result.PotentialVictimsIds);

            _clickableHexes = false;

            return gameResult.AddDomainEvent(new RobberPlacedEvent(hexId));
        }

        private GameResult HandleVictimsAfterBlocking(bool canSteal, List<int> potentialVictimsIds)
        {
            if (!canSteal)
            {
                return GameResult.Ok(EnumGamePhases.NormalRound).AddUIMessage(new LogMessageMessage(EnumLogTypes.Info, "Noone to steal from"));
            }

            else
            {
                return GameResult.Ok().AddUIMessage(new PotentialVictimsFoundMessage(potentialVictimsIds));
            }
        }

        private GameResult VictimChosen(VictimChosenCommand signal)
        {
            var result = Facade.UseSelectVictim(signal.VictimId);
            
            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(Facade.GetCurrentPlayerId(), result.Reason));
            }

            return GameResult.Ok(result.NextPhase);
        }
    }
}