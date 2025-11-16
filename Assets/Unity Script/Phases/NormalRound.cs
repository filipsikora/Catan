using Catan.Catan;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Catan
{
    public class NormalRound : GamePhase
    {
        private Vertex selectedVertex;

        private Edge selectedEdge;

        private VisualResourceCard selectedCard;

        private ResourceCostOrStock _selectedCards = new ResourceCostOrStock();

        private readonly List<VisualResourceCard> _selectedCardsList= new();


        public override void OnEnter()
        {
            Manager.PlayerUIPanel.UpdatePlayerInfo(Game.currentPlayer);
            Manager.MainUIPanel.ShowNextTurnButton();
            Manager.MainUIPanel.Bind(EnumMainUIButtons.OfferTrade, OnTradeOffered);
            Manager.MainUIPanel.Bind(EnumMainUIButtons.BankTrade, OnBankTradeClicked);
        }

        public override void OnVertexClicked(Vertex vertex)
        {
            OnPositionClickedCommon();

            Manager.MainUIPanel.Show(EnumMainUIButtons.BuildVillage);
            Manager.MainUIPanel.Show(EnumMainUIButtons.UpgradeVillage);

            vertex.IsMarked = true;
            selectedVertex = vertex;

            var vertexObj = Manager.BoardVisuals.GetVertexObject(vertex.Id);
            Manager.BoardVisuals.SetVertexVisual(vertexObj, Color.yellow);

            Debug.Log($"{vertex.Owner}, {vertex.HasVillage}");

        }

        public override void OnEdgeClicked(Edge edge)
        {
            OnPositionClickedCommon();

            Manager.MainUIPanel.Show(EnumMainUIButtons.BuildRoad);

            edge.IsMarked = true;
            selectedEdge = edge;

            var edgeObj = Manager.BoardVisuals.GetEdgeObject(edge.Id);
            Manager.BoardVisuals.SetEdgeVisual(edgeObj, Color.yellow);

            Debug.Log($"{edge}");
        }

        public override void OnBuildVillageClicked()
        {
            bool success = Game.BuildVillage(CurrentPlayer, selectedVertex);

            if (success)
            {
                var vertexObj = Manager.BoardVisuals.GetVertexObject(selectedVertex.Id);
                Vector3 pos = vertexObj.transform.position;

                var villageObj = Manager.BoardVisuals.PlaceObject(Manager.CubeVillagePrefab, pos, null, Game.currentPlayer.PlayerColor, Manager.Board);

                selectedVertex.VillageObject = villageObj;

                Manager.MainUIPanel.Hide(EnumMainUIButtons.BuildVillage);
                Manager.MainUIPanel.Hide(EnumMainUIButtons.UpgradeVillage);

                Manager.PlayerUIPanel.UpdatePlayerInfo(CurrentPlayer);
            }
        }

        public override void OnBuildRoadClicked()
        {
            bool success = Game.BuildRoad(CurrentPlayer, selectedEdge);

            if (success)
            {
                var (_, _, mid) = Manager.BoardVisuals.GetEdgePositions(selectedEdge);
                Quaternion rotation = Manager.BoardVisuals.GetEdgeRotation(selectedEdge);

                var roadObj = Manager.BoardVisuals.PlaceObject(Manager.CubeRoadPrefab, mid, rotation, CurrentPlayer.PlayerColor, Manager.Board);

                selectedEdge.RoadObject = roadObj;

                Manager.MainUIPanel.Hide(EnumMainUIButtons.BuildRoad);

                Manager.PlayerUIPanel.UpdatePlayerInfo(CurrentPlayer);
            }
        }

        public override void OnUpgradeVillageClicked()
        {
            var player = Game.currentPlayer;
            bool success = Game.UpgradeVillage(player, selectedVertex);
            var currentVillage = selectedVertex.VillageObject;

            if (success)
            {
                UnityEngine.Object.Destroy(currentVillage);
                var vertexObj = Manager.BoardVisuals.GetVertexObject(selectedVertex.Id);
                Vector3 pos = vertexObj.transform.position;

                var townObj = Manager.BoardVisuals.PlaceObject(Manager.CubeTownPrefab, pos, null, Game.currentPlayer.PlayerColor, Manager.Board);

                selectedVertex.VillageObject = townObj;

                Manager.MainUIPanel.Hide(EnumMainUIButtons.UpgradeVillage);

                Manager.PlayerUIPanel.UpdatePlayerInfo(player);
            }
        }

        public override void OnResourceCardClicked(VisualResourceCard card)
        {
            if (card.IsSelected)
            {
                _selectedCards.SubtractSingleType(card.Type, 1);
                _selectedCardsList.Remove(card);
            }

            else
            {
                _selectedCards.AddSingleType(card.Type, 1);
                _selectedCardsList.Add(card);
            }

            UpdateResourceCardVisual(card);
            card.ToggleSelection();

            if (_selectedCards.ResourceDictionary.Values.Sum() > 0)
            {
                Manager.MainUIPanel.ShowTradeOfferButton();
            }

            else
            {
                Manager.MainUIPanel.HideTradeOfferButton();
            }
        }

        public void OnTradeOffered()
        {
            Manager.OnTradeOffered(_selectedCards);
        }

        public override void OnDevelopmentCardBought()
        {
            Manager.Game.BuyDevelopmentCard(CurrentPlayer);
            Manager.PlayerUIPanel.UpdatePlayerInfo(CurrentPlayer);
        }

        public void OnBankTradeClicked()
        {
            Manager.OnBankTradeClicked();
        }

        public override void UpdateResourceCardVisual(VisualResourceCard card)
        {
            if (card.IsSelected)
            {
                VisualsUI.MoveResourceCardDown(card);
            }

            else
            {
                VisualsUI.MoveResourceCardUp(card);
            }
        }

        public override void OnNextTurnClicked()
        {
            VisualsUI.DeselectAllResourceCards(_selectedCardsList);

            foreach (var card in _selectedCardsList)
            {
                UpdateResourceCardVisual(card);
            }

            OnNextTurnClickedCommon();
        }
    }
}