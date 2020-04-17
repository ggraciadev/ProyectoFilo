using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionGenerator : UnityEditor.EditorWindow
{
    int selected;
    string questionKey;
    string[] options;

    [UnityEditor.MenuItem("WiruLib/Generate Question")]

    public static void OpenWindowPopup()
    {
        QuestionGenerator window = new QuestionGenerator();

        List<string> temp = new List<string>();
        WiruLib.WiruLocalization.GenerateJSON("Localization/Localization");

        window.options = temp.ToArray();

        window.ShowUtility();
    }

    void OnGUI()
    {
        UnityEditor.EditorGUILayout.TextField("Load Sheet from CSV", UnityEditor.EditorStyles.wordWrappedLabel);
        selected = UnityEditor.EditorGUILayout.Popup("Question Key", selected, options);

        if (GUILayout.Button("OK"))
        {
            Debug.Log("");

            this.Close();
        }
    }
}
