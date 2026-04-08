using UnityEngine;
using Catan.Unity.Visuals.Models;
using Catan.Shared.Dtos;

namespace Catan.Unity.Helpers
{
    public class FactoryDevelopmentCards : MonoBehaviour
    {
        public GameObject DevelopmentCardPrefab;

        private EventBus _bus;

        public void Initialize(EventBus bus)
        {
            _bus = bus;
        }

        public GameObject DrawDevelopmentCard(DevelopmentCardDto dto, Transform parent)
        {
            GameObject cardObject = Instantiate(DevelopmentCardPrefab, parent);

            var visual = cardObject.GetComponent<VisualDevelopmentCard>();
            visual.Initialize(dto, _bus);

            return cardObject;
        }
    }
}