using UnityEngine;
using Catan.Unity.Visuals.Models;
using Catan.Core.Snapshots;
using Catan.Shared.Communication;

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

        public GameObject DrawDevelopmentCard(DevelopmentCardSnapshot snapshot, Transform parent)
        {
            GameObject cardObject = Instantiate(DevelopmentCardPrefab, parent);

            var visual = cardObject.GetComponent<VisualDevelopmentCard>();
            visual.Initialize(snapshot, _bus);

            return cardObject;
        }
    }
}