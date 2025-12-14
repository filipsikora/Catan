using Catan;
using Catan.Catan;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Catan
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