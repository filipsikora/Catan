using Catan.Unity.Helpers;
using Catan.Unity.Networking;
using Catan.Unity.Panels;
using System;
using UnityEngine;

namespace Catan.Unity.Bootstrap
{
    public class GameBootstrap : MonoBehaviour
    {
        public static GameBootstrap Instance { get; private set; }

        public EventBus Bus;
        public HandlerEvents EventsHandler;

        public HandlerCameraClicks ClickHandler;
        public BoardManager BoardManager;
        public ManagerUI ManagerUI;

        private async void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        async void Start()
        {
            Debug.Log("Creating game");

            Bus = new EventBus();

            var client = new GameClient();
            Guid gameId;

            try
            {
                gameId = await client.CreateGame();

                Debug.Log($"Game created: {gameId}");
            }

            catch (Exception ex)
            {
                Debug.Log($"Error: {ex.Message}");
                return;
            }

            var translator = new EventsTranslator();

            EventsHandler = new HandlerEvents(translator, Bus, client, gameId);

            ClickHandler.Initialize(Bus);
            ManagerUI.Initialize(Bus, )
        }
    }
}