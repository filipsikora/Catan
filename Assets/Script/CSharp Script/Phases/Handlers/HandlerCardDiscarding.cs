using Catan.Catan;
using Catan.Communication;
using Catan.Core;
using UnityEngine;
using Catan.Communication.Signals;
using System.Collections.Generic;
using System.Linq;


namespace Catan.Core
{
    public class HandlerCardDiscarding : BaseHandler
    {
        private Queue<Player> _playersToDiscard;
        private ResourceCostOrStock _resourcesSelected = new();
        private Player _currentPlayer;
        private int _requiredResources;
        private readonly List<int> _selectedCardsIds = new();

        public HandlerCardDiscarding(GameState game, EventBus bus) : base(game, bus)
        {
            Bus.Subscribe<ResourceCardClickedSignal>(OnResourceCardClicked);
            Bus.Subscribe<DiscardingAcceptedSignal>(OnDiscardingAccepted);

            _playersToDiscard = new Queue<Player>(Game.PlayerList.Where(p => p.Resources.ResourceDictionary.Values.Sum() > 7));
        }

        public void ProceedToNextPlayer()
        {
            if (_playersToDiscard.Count == 0)
            {
                Bus.Publish(new AllDiscardingCompleteSignal());
                return;
            }

            _currentPlayer = _playersToDiscard.Dequeue();
            _resourcesSelected = new ResourceCostOrStock();

            Bus.Publish(new DiscardShownForPlayerSignal(_currentPlayer.ID));
            UpdateRequiredResources();
        }

        private bool UpdateRequiredResources()
        {
            int total = _currentPlayer.Resources.ResourceDictionary.Values.Sum();
            _requiredResources = (int)System.Math.Ceiling(total / 2.0);
            bool canDiscard = _resourcesSelected.ResourceDictionary.Values.Sum() == _requiredResources;

            return canDiscard;
        }

        private void OnResourceCardClicked(ResourceCardClickedSignal signal)
        {
            if (signal.IsLeftClicked && _selectedCardsIds.Contains(signal.VisualResourceCardId))
            {
                _resourcesSelected.SubtractSingleType(signal.Type, 1);
                _selectedCardsIds.Remove(signal.VisualResourceCardId);
                Bus.Publish(new ResourceCardVisualStateChangedSignal(signal.VisualResourceCardId, signal.Location, EnumResourceCardVisualState.None));
            }

            else if (signal.IsLeftClicked && !_selectedCardsIds.Contains(signal.VisualResourceCardId))
            {
                _resourcesSelected.AddSingleType(signal.Type, 1);
                _selectedCardsIds.Add(signal.VisualResourceCardId);
                Bus.Publish(new ResourceCardVisualStateChangedSignal(signal.VisualResourceCardId, signal.Location, EnumResourceCardVisualState.Highlighted));
            }

            bool canDiscard = UpdateRequiredResources();

            Bus.Publish(new AcceptedDiscardVisibilitySignal(canDiscard));
        }

        private void OnDiscardingAccepted(DiscardingAcceptedSignal signal)
        {
            _currentPlayer.Resources.SubtractCards(_resourcesSelected);
            Game.Bank.AddCards(_resourcesSelected);

            ProceedToNextPlayer();
        }

        public override void Dispose()
        {
            Bus.Unsubscribe<ResourceCardClickedSignal>(OnResourceCardClicked);
            Bus.Unsubscribe<DiscardingAcceptedSignal>(OnDiscardingAccepted);
        }
    }
}
