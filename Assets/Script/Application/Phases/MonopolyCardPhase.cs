using Catan.Application.Controllers;
using Catan.Application.UIMessages;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class MonopolyCardPhase : BasePhase
    {
        private EnumResourceTypes? _type;

        public MonopolyCardPhase(Facade facade) : base(facade) { }

        public override GameResult Handle(object command)
        {
            switch (command)
            {
                case StolenCardSelectedCommand c:
                    return HandleResourceSelected(c);

                case CardSelectionAcceptedCommand c:
                    return HandleResourceAccepted(c);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleResourceSelected(StolenCardSelectedCommand signal)
        {
            if (_type == signal.Type)
            {
                _type = null;
            }
            else
            {
                _type = signal.Type;
            }

            bool hasSelected = _type != null;

            return GameResult.Ok().AddUIMessage(new ResourceSelectedMessage(hasSelected, signal.Type));
        }

        private GameResult HandleResourceAccepted(CardSelectionAcceptedCommand signal)
        {
            var result = Facade.UseMonopolyCard(_type.Value);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.ThiefId, result.Reason));
            }

            return GameResult.Ok(result.NextPhase);
        }
    }
}