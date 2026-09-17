using TMPro;
using UnityEngine;

namespace Unity.Catan.Panels
{
    public class AwaitingUI : MonoBehaviour
    {
        public TextMeshProUGUI Message;

        public void Show(string message = "Waiting for other players")
        {
            gameObject.SetActive(true);
            Message.text = message;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
