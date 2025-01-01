using BepInEx;
using BepInEx.Logging;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

namespace SamLib
{
    [BepInPlugin("com.magelock.samlib", "SamLib", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Debug.Log("SamLib Init");
            SceneManager.sceneLoaded += OnTitleScreenSL;
        }

        private void OnTitleScreenSL(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("OnTitleScreenSL");
            if (scene.name == "TitleScreen")
            {
                OnTitleScreen(CreateModButton);
            }
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnTitleScreenSL;
        }

        void CreateModButton()
        {
            Debug.Log("Creating Mod Button");
            GameObject qb = GameObject.Find("Canvas (1)/Quit Button");
            if (qb != null)
            {
                if (FindObjectOfType<EventSystem>() == null)
                {
                    GameObject eventSystem = new GameObject("EventSystem");
                    eventSystem.AddComponent<EventSystem>();
                    eventSystem.AddComponent<StandaloneInputModule>();
                }

                GameObject modButton = Instantiate(qb, qb.transform.parent);
                modButton.name = "Mod Button";
                Destroy(modButton.GetComponent<QuitButton>());
                modButton.transform.position = new Vector3(-0.0257f, -51.7577f, 90f);
                GameObject text = modButton.transform.Find("Text (TMP)").gameObject;
                text.GetComponent<TMPro.TextMeshProUGUI>().text = "Mods";
                ModButton mbcomp = modButton.AddComponent<ModButton>();
            }
        }

        public void OnTitleScreen(Action action)
        {
            action();
        }

        public void OnMenuCreated(object sender, EventArgs e)
        {
            
        }
    }
}
