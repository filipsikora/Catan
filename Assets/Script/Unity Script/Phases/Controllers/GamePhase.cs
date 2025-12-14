#nullable enable
using Catan.Catan;
using Catan.Communication;
using System;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Catan
{
    public abstract class GamePhase
    {
        protected ManagerGame Manager => ManagerGame.Instance;
        protected ManagerUI UI => ManagerGame.Instance.UIManager;
        internal HandlerPhases? Handler;
        protected GameState Game => Manager.Game;
        protected Player CurrentPlayer => Game.CurrentPlayer;
        protected EventBus EventBus => Manager.EventBus;

        public event Action<Player>? VictimChosen;

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
    }
}