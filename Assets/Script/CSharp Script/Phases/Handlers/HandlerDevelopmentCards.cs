using Catan.Catan;
using Catan.Communication;
using Catan.Communication.Signals;
using Catan.Core;
using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Catan.Core
{
    public class HandlerDevelopmentCards : BaseHandler
    {
        private readonly List<int> DevelopmentCardsByID = new();

        public HandlerDevelopmentCards(GameState game, EventBus bus) : base(game, bus)
        {
            Bus.Subscribe<DevelopmentCardClickedSignal>(OnDevelopmentCardClicked);
        }

        public void OnDevelopmentCardClicked(DevelopmentCardClickedSignal signal)
        {
            VisualDevelopmentCard cardVisual = signal.Card;
            DevelopmentCard cardModel = cardVisual.LinkedCard;
            int cardID = cardModel.ID;

            if (!cardModel.IsNew)
            {
                UseCard(cardModel);
            }
        }

        private void UseCard(DevelopmentCard card)
        {
            card.IsUsed = true;
            Game.CurrentPlayer?.DevelopmentCardsByID.Remove(card.ID);

            switch (card.Type)
            {
                case EnumDevelopmentCardTypes.Knight:
                    Game.OnKnightUsed(Game.CurrentPlayer);
                    Bus.Publish(new KnightCardUsedSignal());
                    break;

                case EnumDevelopmentCardTypes.Monopoly:
                    Bus.Publish(new MonopolyCardUsedSignal());
                    break;

                case EnumDevelopmentCardTypes.RoadBuilding:
                    Bus.Publish(new RoadBuildingCardUsedSignal());
                    break;

                case EnumDevelopmentCardTypes.VictoryPoint:
                    Game.OnVictoryPointUsed(Game.CurrentPlayer);
                    Bus.Publish(new VictoryPointUsedSignal());
                    break;

                case EnumDevelopmentCardTypes.YearOfPlenty:
                    Bus.Publish(new YearOfPlentyUsedSignal());
                    break;
            }
        }

        public override void Dispose()
        {
            Bus.Unsubscribe<DevelopmentCardClickedSignal>(OnDevelopmentCardClicked);
        }
    }
}
