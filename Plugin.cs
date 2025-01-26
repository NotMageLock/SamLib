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
    [BepInPlugin("com.magelock.samlib", "SamLib", "0.0.1")]
    public class Plugin : BaseUnityPlugin
    {
        internal string ModInfo = "SamLib by MageLock (v0.0.1, com.magelock.samlib)";
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
        public void AddMod(string title, string author, string version, string guid)
        {
            string info = title + " by " + author + " (v" + version + ", " + guid + ")";
            ModInfo += "\n" + info;
        }

        void CreateModButton()
        {
            Debug.Log("Creating Mod Button");
            GameObject qb = GameObject.Find("Canvas (1)/Quit Button");
            if (qb != null)
            {
                GameObject modButtonA = Instantiate(qb, qb.transform.parent);
                modButtonA.name = "Mod Button";
                Destroy(modButtonA.GetComponent<QuitButton>());
                modButtonA.transform.position = new Vector3(0f, -57.8576f, 90f);
                GameObject text = modButtonA.transform.Find("Text (TMP)").gameObject;
                text.GetComponent<TMPro.TextMeshProUGUI>().text = "Mods";
                ModButton modButton = modButtonA.AddComponent<ModButton>();
                modButton.plugins += ModInfo;
            }

            else
            {
                Debug.Log("qb null");
            }
        }

        public void OnTitleScreen(Action action)
        {
            action();
        }
    }
}
