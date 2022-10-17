using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Debuger
{
    public class DeBuger : MonoBehaviour
    {
        private static RectTransform _canvasTrs;

        static DeBuger()
        {
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();
            if(canvas==null)
            {
                Debug.LogError("UI Äµ¹ö½º ¾øÀ½");
            }
            else
            {
                _canvasTrs = canvas.GetComponent<RectTransform>();
            }
        }

        public static void CreateButton(Vector2 pos, string name, Vector2 size, Action action)
        {
            GameObject button = new GameObject($"Btn_{name}");
            RectTransform rect = button.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.zero;
            rect.anchoredPosition = pos;
            rect.sizeDelta = size;
            button.AddComponent<Image>();
            CustomButton btnCompo = button.AddComponent<CustomButton>();
            btnCompo.BtnAction = action;

            rect.SetParent(_canvasTrs);

            GameObject text = new GameObject("ButtonText");
            RectTransform textRect = text.AddComponent<RectTransform>();
            textRect.SetParent(rect);
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = new Vector2(1, 1);
            textRect.pivot = new Vector2(0.5f, 0.5f);
            textRect.sizeDelta = Vector2.zero;
            textRect.anchoredPosition = Vector2.zero;

            Text textCompo = text.AddComponent<Text>();
            textCompo.text = name;
            textCompo.color = Color.black;
            textCompo.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            textCompo.resizeTextForBestFit = true;
        }
    }

    public class CustomButton : MonoBehaviour, IPointerClickHandler
    {
        private Action _btnAction = null;
        public Action BtnAction { set => _btnAction = value; }
        public void OnPointerClick(PointerEventData eventData)
        {
            _btnAction?.Invoke();
        }
    }
}

