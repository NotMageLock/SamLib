using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BepInEx;
using UnityEngine.UI;
using TMPro;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BepInEx.Bootstrap;

namespace SamLib
{
    public class ModButton : MonoBehaviour
    {
        private Canvas? canvas;
        private GameObject? panel;
        private TextMeshProUGUI? pluginListText;
        private Button? button;
        internal string plugins = "Plugins:\n";

        void Awake()
        {
            button = this.gameObject.GetComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if (panel == null)
            {
                CreateMenu();
            }

            else if (panel != null && panel.activeSelf)
            {
                panel.SetActive(false);
            }
                

            else if (panel != null && !panel.activeSelf)
            {
                panel.SetActive(true);
            }
        }

        public void CreateMenu()
        {
            canvas = this.gameObject.transform.parent.gameObject.GetComponent<Canvas>();
            if (canvas == null)
            {
                canvas = this.gameObject.transform.parent.gameObject.AddComponent<Canvas>();
            }

            if (canvas.GetComponent<CanvasScaler>() == null)
            canvas.gameObject.AddComponent<CanvasScaler>();
            if (canvas.GetComponent<GraphicRaycaster>() == null)
            canvas.gameObject.AddComponent<GraphicRaycaster>();
            Debug.Log("Canvas Assigned");
            Panel();
        }
        void Panel()
        {
            panel = new GameObject("Mod Panel");
            if (canvas != null)
            panel.transform.SetParent(canvas.transform);
            panel.AddComponent<CanvasRenderer>();
            panel.transform.localPosition = new Vector3(0f, 0f, 0f);
            Image panelImage = panel.AddComponent<Image>();
            panelImage.color = new Color(0f, 0f, 0f, 0.5f);
            Debug.Log("Panel created");
            panel.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            Rect();
        }

        void Rect()
        {
            if (panel != null)
            {
                RectTransform panelRect = panel.GetComponent<RectTransform>();
                panelRect.sizeDelta = new Vector2(Screen.width * 0.75f, Screen.height * 0.65f);
                panelRect.pivot = new Vector2(0.5f, 0.5f);
                panelRect.anchoredPosition = Vector2.zero;
                Text(panelRect);
                Debug.Log("PanelRect created");
            }
        }

        void Text(RectTransform panelRect)
        {
            GameObject textObject = new GameObject("PluginListText");
            if (panel != null)
                textObject.transform.SetParent(panel.transform);
            pluginListText = textObject.AddComponent<TextMeshProUGUI>();
            pluginListText.text = plugins;
            pluginListText.color = Color.white;
            pluginListText.alignment = TextAlignmentOptions.TopLeft;
            textObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.11f);
            Debug.Log("Text created");
            TextRect(textObject, panelRect);
        }

        void TextRect(GameObject textObject, RectTransform panelRect)
        {
            RectTransform textRect = textObject.GetComponent<RectTransform>();
            textRect.sizeDelta = new Vector2(panelRect.sizeDelta.x - 20, panelRect.sizeDelta.y - 20);
            textRect.anchorMin = new Vector2(0.5f, 0.5f);
            textRect.anchorMax = new Vector2(0.5f, 0.5f);
            textRect.pivot = new Vector2(0.5f, 0.5f);
            textRect.anchoredPosition = Vector2.zero;
            Debug.Log("TextRect created");
            Debug.Log("Mod panel complete");
        }

        void OnDestroy()
        {
            if (button != null)
            button.onClick.RemoveListener(OnButtonClick);
        }

        void OnTextCreated()
        {

        }
    }
}
