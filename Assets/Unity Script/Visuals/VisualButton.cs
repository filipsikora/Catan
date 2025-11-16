using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Catan
{
    public abstract class VisualButton<TEnum> : MonoBehaviour where TEnum : Enum
    {
        private Dictionary<TEnum, Button> _buttons = new();

        public void RegisterButton(TEnum key, Button button)
        {

            if (button == null)
            {
                Debug.LogError($"Button for {key} is NULL in {gameObject.name}");
                return;
            }

            _buttons[key] = button;
        }

        public void Bind(TEnum key, Action callback)
        {

            if (!_buttons.TryGetValue(key, out var button))
            {
                Debug.LogWarning($"Button {key} not registered in {gameObject.name}");
                return;
            }

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => callback?.Invoke());
        }

        public void Show(TEnum key)
        {
            if (_buttons.TryGetValue(key, out var btn))
                btn.gameObject.SetActive(true);
        }

        public void Hide(TEnum key)
        {
            if (_buttons.TryGetValue(key, out var btn))
                btn.gameObject.SetActive(false);
        }
    }   
}