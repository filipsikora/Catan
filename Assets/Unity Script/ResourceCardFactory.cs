using Catan;
using Catan.Catan;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Catan
{
    public class ResourceCardFactory : MonoBehaviour
    {

        public GameObject ResourceCardPrefab;

        public GameObject CreateResourceCard(EnumResourceTypes type, Transform parent)
        {
            GameObject card = Instantiate(ResourceCardPrefab, parent);

            var visual = card.GetComponent<VisualResourceCard>();
            visual.Type = type;
            visual.Container = parent;

            return card;
        }
    }
}