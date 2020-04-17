using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace WiruLib
{
    public class WiruLibLocalizationEditorMenu : UnityEditor.EditorWindow
    {

        string path;
        public string temp = "Path to sheet: ";

        [UnityEditor.MenuItem("WiruLib/LoadTranslateSheet")]

        public static void OpenWindowPopup()
        {
            WiruLibLocalizationEditorMenu window = new WiruLibLocalizationEditorMenu();
            window.ShowUtility();
        }
        public static void LoadTranslateSheet(string _path)
        {
            Debug.Log("Opening " + _path);
            WiruLocalization.LoadLocalization(_path);
        }

        void OnGUI()
        {
            UnityEditor.EditorGUILayout.TextField("Load Sheet from CSV", UnityEditor.EditorStyles.wordWrappedLabel);
            path = UnityEditor.EditorGUILayout.TextField(temp, path);

            if (GUILayout.Button("OK"))
            {
                Debug.Log(path);
                LoadTranslateSheet(path);
                this.Close();
            }
        }
    }
}
#endif
