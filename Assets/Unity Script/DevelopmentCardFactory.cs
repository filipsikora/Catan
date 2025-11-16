    using Catan;
    using Catan.Catan;
    using System.Linq;
    using Unity.VisualScripting;
    using UnityEngine;

    namespace Catan
    {
        public class DevelopmentCardFactory : MonoBehaviour
        {
            public GameObject DevelopmentCardPrefab;

            public GameObject CreateDevelopmentCard(EnumDevelopmentCardTypes type, Transform parent)
            {
                GameObject card = Instantiate(DevelopmentCardPrefab, parent);

                var visual = card.GetComponent<VisualDevelopmentCard>();
                visual.Type = type;
                visual.SetupVisuals();

                return card;
            }
        }
    }
