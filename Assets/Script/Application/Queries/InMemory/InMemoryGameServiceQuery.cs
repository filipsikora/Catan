using Catan.Application.Snapshots;
using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Shared.Data;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Application.Queries.InMemory
{
    public sealed class InMemoryGameQueryService : IGameQueryService
    {
        private readonly GameState _game;

        public InMemoryGameQueryService(GameState game)
        {
            _game = game;
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

        public IReadOnlyList<DevelopmentCardSnapshot> GetCurrentPlayerDevelopmentCards()
        {
            var player = _game.GetCurrentPlayer();
            bool afterRoll = _game.GetAfterRoll();

            return player.DevelopmentCardsByID.Select(id => FindCard(id)).Select(card => Map(card, afterRoll)).ToList();
        }
    }
}
