using UnityEngine;
using Catan.Unity.Visuals.Models;
using Catan.Core.Snapshots;

namespace Catan.Unity.Helpers
{
    public class FactoryDevelopmentCards : MonoBehaviour
    {
        public GameObject DevelopmentCardPrefab;

        public GameObject DrawDevelopmentCard(DevelopmentCardSnapshot snapshot, Transform parent)
        {
            GameObject cardObject = Instantiate(DevelopmentCardPrefab, parent);

            var visual = cardObject.GetComponent<VisualDevelopmentCard>();
            visual.Initialize(snapshot);

            return cardObject;
        }
    }
}