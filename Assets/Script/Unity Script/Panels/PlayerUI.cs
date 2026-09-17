using UnityEngine;
using TMPro;
using Catan.Unity.Helpers;
using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Unity.Panels
{
    public class PlayerUI : MonoBehaviour
    {
        public TextMeshProUGUI PlayerNameText;
        public TextMeshProUGUI PlayerBuildingsText; // remnants to be moved to information ui
        public TextMeshProUGUI PlayerPointsText;

        public Transform ResourceCardsPanel;
        public FactoryResourceCards ResourceCardFactory;

        public void UpdateResources(Dictionary<EnumResourceType, int> resources)
        {
            // redraw resources using visualhelper
        }
    }
}