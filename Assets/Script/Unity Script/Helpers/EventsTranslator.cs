using Catan.Application.Interfaces;
using Catan.Application.UIMessages;
using Catan.Core.DomainEvents;
using Catan.Core.Interfaces;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Interfaces;

namespace Catan.Unity.Helpers
{
    public class EventsTranslator
    {
        public EventsTranslator() { }

        public IInternalUIEvents TranslateUIMessage(IUIMessages uiMessage)
        {
            return uiMessage switch
            {
                VertexHighlightedMessage m => new VertexHighlightedUIEvent(m.VertexId),
                EdgeHighlightedMessage m => new EdgeHighlightedUIEvent(m.EdgeId),
                BuildOptionsSentMessage m => new BuildOptionsSentUIEvent(m.CanBuildVillage, m.CanBuildRoad, m.CanUpgradeVillage),
                LogMessageMessage m =>  new LogMessageUIEvent(m.Type, m.Message),
                ActionRejectedMessage m => new ActionRejectedUIEvent(m.PlayerId, m.Reason),
                ResourceSelectedMessage m => new ResourceSelectedUIEvent(m.Selected, m.Type),
                SelectionChangedMessage m => new SelectionChangedUIEvent(m.ActionAvailable),
                DesiredCardsChangedMessage m => new DesiredCardsChangedUIEvent(m.HasDesired),
                PlayerSelectedToDiscardMessage m => new PlayerSelectedToDiscardUIEvent(m.PlayerId),
                PotentialVictimsFoundMessage m => new PotentialVictimsFoundUIEvent(m.VictimsIds),
                BankTradeRatioChangedMessage m => new BankTradeRatioChangedUIEvent(m.Ratio, m.PossibleForPlayer, m.Resource)
            };
        }

        public IInternalUIEvents TranslateDomainEvent(IDomainEvent domainEvent)
        {
            return domainEvent switch
            {
                VillagePlacedEvent m => new VillagePlacedUIEvent(m.VertexId, m.OwnerId),
                RoadPlacedEvent m => new RoadPlacedUIEvent(m.EdgeId, m.OwnerId),
                TownPlacedEvent m => new TownPlacedUIEvent(m.VertexId, m.OwnerId),
                DevelopmentCardBoughtEvent m => new DevelopmentCardBoughtUIEvent(m.CardId),
                RobberPlacedEvent m => new RobberMovedUIEvent(m.HexId),
            };
        }
    }
}
