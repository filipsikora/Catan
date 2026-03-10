using Catan.Application.Snapshots;
using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Shared.Data;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Application.Queries.DevCards
{
    public sealed class InMemoryDevCardQueryService : IDevCardsQueryService
    {
        private readonly GameState _game;

        public InMemoryDevCardQueryService(GameState game)
        {
            _game = game;
        }

        public IReadOnlyList<DevelopmentCardSnapshot> GetCurrentPlayerDevCards()
        {
            var player = _game.GetCurrentPlayer();
            bool afterRoll = _game.GetAfterRoll();

            return player.DevelopmentCardsByID.Select(id => FindCard(id)).Select(card => Map(card, afterRoll)).ToList();
        }

        private DevelopmentCard FindCard(int id)
        {
            return _game.DevelopmentCardsDeckAll.First(c => c.ID == id);
        }

        private DevelopmentCardSnapshot Map(DevelopmentCard card, bool afterRoll)
        {
            bool isPlayable = !card.IsNew && (afterRoll || card.Type == EnumDevelopmentCardTypes.Knight);

            return new DevelopmentCardSnapshot(card.ID, card.Type, card.IsNew, isPlayable);
        }
    }
}
