using UnityEngine;
using Catan.Unity.Visuals.Models;
using Catan.Unity.Models;

namespace Catan.Unity.Helpers
{
    public class FactoryDevelopmentCards : MonoBehaviour
    {
        public GameObject DevelopmentCardPrefab;

        public GameObject DrawDevelopmentCard(DevCardModel dto, Transform parent)
        {
            GameObject cardObject = Instantiate(DevelopmentCardPrefab, parent);

            var visual = cardObject.GetComponent<VisualDevelopmentCard>();
            visual.Initialize(dto, _bus);

            return cardObject;
        }
    }
}