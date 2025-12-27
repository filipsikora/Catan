using UnityEngine;
using Catan.Core.Models;
using Catan.Unity.Visuals.Models;

namespace Catan.Unity.Helpers
{
    public class FactoryDevelopmentCards : MonoBehaviour
    {
        public GameObject DevelopmentCardPrefab;

        public GameObject DrawDevelopmentCard(DevelopmentCard card, Transform parent)
        {
            GameObject cardObject = Instantiate(DevelopmentCardPrefab, parent);

            var visual = cardObject.GetComponent<VisualDevelopmentCard>();
            visual.Initialize(card);

            return cardObject;
        }
    }
}