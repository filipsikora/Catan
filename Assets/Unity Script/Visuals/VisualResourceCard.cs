using Catan.Catan;
using UnityEngine;

namespace Catan
{
    public class VisualResourceCard : MonoBehaviour
    {
        public EnumResourceTypes Type;

        public Transform Container;

        public bool IsSelected { get; private set; } = false;

        public void OnCardClicked()
        {
            CatanGameManager.Instance.PhaseHandler.CurrentPhase?.OnResourceCardClicked(this);
        }

        public void ToggleSelection()
        {
            if (IsSelected)
            {
                IsSelected = false;
            }

            else
                IsSelected = true;
        }

        public override string ToString()
        {
            return $"Card: {Type}";
        }
    }
}