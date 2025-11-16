#nullable enable
using Catan.Catan;
using System;
using UnityEngine;

namespace Catan
{
    public abstract class GamePhase
    {
        protected CatanGameManager Manager => CatanGameManager.Instance;

        protected GameState Game => Manager.Game;

        protected Player CurrentPlayer => Game.currentPlayer;

        public event Action<Player>? VictimChosen;


        public virtual void OnEnter() { }
        public virtual void OnExit() { }

        public virtual void OnNextTurnClicked() { }
        public virtual void OnRollDiceClicked() { }

        public virtual void OnVertexClicked(Vertex vertex) { }
        public virtual void OnHexClicked(HexTile hex) { }
        public virtual void OnEdgeClicked(Edge edge) { }

        public virtual void OnBuildFreeVillageClicked() { }
        public virtual void OnBuildFreeRoadClicked() { }

        public virtual void OnBuildVillageClicked() { }
        public virtual void OnBuildRoadClicked() { }
        public virtual void OnUpgradeVillageClicked() { }

        public virtual void OnResourceCardClicked(VisualResourceCard card) { }

        public virtual void UpdateResourceCardVisual(VisualResourceCard card) { }

        public virtual void OnDevelopmentCardClicked(VisualDevelopmentCard card) { }

        public virtual void OnVictimChosen(Player victim) { }

        public virtual void OnDevelopmentCardBought() { }

        public virtual void OnClickedOnNothing() { }

        protected void RaiseVictimChosen(Player victim)
        {
            VictimChosen?.Invoke(victim);
        }

        protected virtual void OnPositionClickedCommon()
        {
            Manager.BoardVisuals.ResetMarkedPositions();

            Manager.MainUIPanel.HideStartingBuildings();
            Manager.MainUIPanel.HideLaterBuildings();
        }

        protected virtual void OnNextTurnClickedCommon()
        {
            Game.EndTurn();
            Manager.BoardVisuals.ResetMarkedPositions();

            Manager.MainUIPanel.UpdateTurnCounter(Game.Turn);
            Manager.MainUIPanel.NextTurnButton.gameObject.SetActive(false);
            CurrentPlayer.DevelopmentCardsNew.Clear();
        }
    }
}