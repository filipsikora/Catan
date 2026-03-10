using Catan.Core.Snapshots;
using Catan.Core.Queries.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Catan.Core.Models;
using Catan.Shared.Data;

namespace Catan.Core.Queries.InMemory
{
    public sealed class InMemoryDevCardQueryService : IDevCardsQueryService
    {
        private readonly GameSession _session;

        public InMemoryDevCardQueryService(GameSession session)
        {
            _session = session;
        }

        public IReadOnlyList<DevelopmentCardSnapshot> GetCurrentPlayerDevCards()
        {
            var player = _session.GetCurrentPlayer();
            bool afterRoll = _session.GetAfterRoll();

            return player.DevelopmentCardsByID.Select(id => FindCard(id)).Select(card => Map(card, afterRoll)).ToList();
        }

        private DevelopmentCard FindCard(int id)
        {
            return _session.GetDevCardById(id);
        }

        private DevelopmentCardSnapshot Map(DevelopmentCard card, bool afterRoll)
        {
            bool isPlayable = !card.IsNew && (afterRoll || card.Type == EnumDevelopmentCardTypes.Knight);

            return new DevelopmentCardSnapshot(card.ID, card.Type, card.IsNew, isPlayable);
        }
    }
}
