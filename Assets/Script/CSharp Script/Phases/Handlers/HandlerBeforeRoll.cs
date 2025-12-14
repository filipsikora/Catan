using Catan.Catan;
using Catan.Communication;
using Catan.Core;
using UnityEngine;
using Catan.Communication.Signals;
using UnityEditor;


namespace Catan.Core
{
    public class HandlerBeforeRoll : BaseHandler
    {
        private bool _afterRoll = false;

        public HandlerBeforeRoll(GameState game, EventBus bus) : base(game, bus)
        {
            Bus.Subscribe<RequestDiceRollSignal>(OnRollDiceClicked);

            Bus.Subscribe<VertexClickedSignal>(OnVertexClicked);
            Bus.Subscribe<EdgeClickedSignal>(OnEdgeClicked);
            Bus.Subscribe<HexClickedSignal>(OnHexClicked);

            Bus.Subscribe<RequestShowDevelopmentCardsSignal>(OnDevelopmentCardsShowRequested);
        }

        private void OnRollDiceClicked(RequestDiceRollSignal signal)
        {
            Game.RollAndServePlayers();

            Bus.Publish(new DiceRolledSignal(Game.LastRoll));
        }

        private void OnVertexClicked(VertexClickedSignal signal)
        {
            Debug.Log("Roll first");
        }

        private void OnEdgeClicked(EdgeClickedSignal signal)
        {
            Debug.Log("Roll first");
        }

        private void OnHexClicked(HexClickedSignal signal)
        {
            Debug.Log("Roll first");
        }

        private void OnDevelopmentCardsShowRequested(RequestShowDevelopmentCardsSignal signal)
        {
            Bus.Publish(new DevelopmentCardsShownSignal(Game.CurrentPlayer.DevelopmentCardsByID, _afterRoll));
        }

        public override void Dispose()
        {
            Bus.Unsubscribe<RequestDiceRollSignal>(OnRollDiceClicked);

            Bus.Unsubscribe<VertexClickedSignal>(OnVertexClicked);
            Bus.Unsubscribe<EdgeClickedSignal>(OnEdgeClicked);
            Bus.Unsubscribe<HexClickedSignal>(OnHexClicked);

            Bus.Unsubscribe<RequestShowDevelopmentCardsSignal>(OnDevelopmentCardsShowRequested);
        }
    }
}