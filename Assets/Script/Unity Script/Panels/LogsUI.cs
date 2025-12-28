using System.Collections;
using TMPro;
using UnityEngine;

namespace Catan.Unity.Panels
{
    public class LogsUI : MonoBehaviour
    {
        public TextMeshProUGUI LogText;
        public int MaxLines = 20;

        public void AddInfo(string message, int time)
        {
            StopAllCoroutines();
            LogText.text = $"<style=Info>{message}</style>\n";
            StartCoroutine(ClearAfterDelay(time));
        }

        public void AddWarning(string message)
        {
            StopAllCoroutines();
            LogText.text = $"<style=Warning>{message}</style>\n";
            StartCoroutine(ClearAfterDelay(2));
        }

        public void AddError(string message)
        {
            StopAllCoroutines();
            LogText.text = $"<style=Error>{message}</style>\n";
            StartCoroutine(ClearAfterDelay(2));
        }

        private IEnumerator ClearAfterDelay(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            LogText.text = "";
        }

        public void Awake() { }
    }
}