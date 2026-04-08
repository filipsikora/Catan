#nullable enable
using Catan.Unity.Phases.Controllers;
using Catan.Unity.Panels;
using Catan.Unity.Helpers;
using Catan.Unity.Networking;
using System;

namespace Catan.Unity.Phases.Adapters
{
    public abstract class BasePhaseAdapter
    {
        protected ManagerUI UI;
        protected EventBus EventBus;
        protected HandlerEvents EventsHandler;
        protected GameClient Client;
        protected Guid GameId;

        internal AdapterPhaseTransition? Handler;

        public BasePhaseAdapter(ManagerUI ui, EventBus bus, HandlerEvents eventHandler, GameClient client, Guid gameId)
        {
            UI = ui;
            EventBus = bus;
            EventsHandler = eventHandler;
            Client = client;
            GameId = gameId;
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
    }
}