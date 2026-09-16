using Catan.Unity.Helpers;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Catan.Unity.Panels
{
    public class LogsUI : MonoBehaviour
    {
        public TextMeshProUGUI LogText;
        public int MaxLines = 20;

        private LogQueue _logQueue;

        public void Initialize(LogQueue logQueue)
        {
            _logQueue = logQueue;
        }

        public void AddInfo(string message, int time)
        {
            LogText.text = $"<style=Info>{message}</style>\n";
            StartCoroutine(ClearAfterDelay(time));
        }

        public void AddWarning(string message)
        {
            LogText.text = $"<style=Warning>{message}</style>\n";
            StartCoroutine(ClearAfterDelay(2));
        }

        public void AddError(string message)
        {
            LogText.text = $"<style=Error>{message}</style>\n";
            StartCoroutine(ClearAfterDelay(2));
        }

        private IEnumerator ClearAfterDelay(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            LogText.text = "";

            _logQueue.LogDisplayFinished();
        }

        public void Awake() { }
    }
}