#nullable enable
using Catan.Catan;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


namespace Catan
{
    public class DevelopmentCardHandler
    {
        private GameState game;

        public DevelopmentCardHandler(GameState gameState)
        {
            game = gameState;
        }

        public void UseCard(EnumDevelopmentCardTypes type)
        {
            switch (type)
            {
                case EnumDevelopmentCardTypes.Knight:
                    OnKnightUsed();
                    break;

                case EnumDevelopmentCardTypes.Monopoly:
                    OnMonopolyUsed();
                    break;

                case EnumDevelopmentCardTypes.RoadBuilding:
                    OnRoadBuildingUsed();
                    break;

                case EnumDevelopmentCardTypes.VictoryPoint:
                    OnVictoryPointUsed();
                    break;

                case EnumDevelopmentCardTypes.YearOfPlenty:
                    OnYearOfPlentyUsed();
                    break;
            }
        }

        public void OnKnightUsed()
        {
            Debug.Log("chuj");
        }

        public void OnVictoryPointUsed()
        {

        }

        public void OnMonopolyUsed()
        {

        }

        public void OnYearOfPlentyUsed()
        {

        }

        public void OnRoadBuildingUsed()
        {

        }
    }
}
