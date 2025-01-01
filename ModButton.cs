using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BepInEx;
using UnityEngine.UI;
using TMPro;

namespace SamLib
{
    public class ModButton : MonoBehaviour
    {
        private Canvas? canvas;
        private GameObject? panel;
        private Text? pluginListText;
        public Button? button;
        public bool isCreated = false;
        public bool isOpen = false;

        void Awake()
        {
            button = this.gameObject.GetComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if (!isCreated)
            {
                CreateMenu();
                isCreated = true;
            }
            else if (isOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }

        void CreateMenu()
        {
            canvas = new GameObject("Canvas").AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.gameObject.AddComponent<CanvasScaler>();
            canvas.gameObject.AddComponent<GraphicRaycaster>();

            panel = new GameObject("Panel");
            panel.transform.SetParent(canvas.transform);
            panel.AddComponent<CanvasRenderer>();
            Image panelImage = panel.AddComponent<Image>();
            panelImage.color = new Color(0f, 0f, 0f, 0.5f);

            RectTransform panelRect = panel.AddComponent<RectTransform>();
            panelRect.sizeDelta = new Vector2(Screen.width * 0.75f, Screen.height * 0.65f);
            panelRect.anchorMin = new Vector2(0.5f, 0.5f);
            panelRect.anchorMax = new Vector2(0.5f, 0.5f);
            panelRect.pivot = new Vector2(0.5f, 0.5f);
            panelRect.anchoredPosition = Vector2.zero;

            GameObject textObject = new GameObject("PluginListText");
            textObject.transform.SetParent(panel.transform);
            pluginListText = textObject.AddComponent<Text>();
            pluginListText.color = Color.white;
            pluginListText.alignment = TextAnchor.UpperLeft;

            RectTransform textRect = textObject.GetComponent<RectTransform>();
            textRect.sizeDelta = new Vector2(panelRect.sizeDelta.x - 20, panelRect.sizeDelta.y - 20);
            textRect.anchorMin = new Vector2(0.5f, 0.5f);
            textRect.anchorMax = new Vector2(0.5f, 0.5f);
            textRect.pivot = new Vector2(0.5f, 0.5f);
            textRect.anchoredPosition = Vector2.zero;
        }

        void CloseMenu()
        {
            panel?.SetActive(false);
            isOpen = false;
        } 

        void OpenMenu()
        {
            panel?.SetActive(true);
            isOpen = true;
        }
    }
}
