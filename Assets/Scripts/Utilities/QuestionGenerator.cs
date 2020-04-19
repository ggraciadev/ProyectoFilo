using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionGenerator : UnityEditor.EditorWindow
{
    string[] ALL_TYPES = { "Test", "MultipleChoice", "Order", "FillGaps" };
    int selected;
    string questionKey = "Key de la Pregunta";
    string questionTitleSpa = "Texto de la pregunta (SPA)";
    Question.QuestionTypes questionType;

    string[] correctAnswers;
    int correctAnswersCount;

    string[] incorrectAnswers;
    int incorrectAnswersCount;

    [UnityEditor.MenuItem("WiruLib/Generate Question")]

    public static void OpenWindowPopup()
    {
        QuestionGenerator window = new QuestionGenerator();
        window.correctAnswers = new string[10];
        window.incorrectAnswers = new string[10];

        window.questionKey = "Key de la Pregunta";
        window.questionTitleSpa = "Texto de la pregunta (SPA)";

        for (int i = 0; i < 10; ++i)
        {
            window.correctAnswers[i] = "Respuesta correcta " + i;
            window.incorrectAnswers[i] = "Respuesta incorrecta " + i;
        }
        
        
        WiruLib.WiruLocalization.GenerateJSON("Localization/Localization");
        
        window.ShowUtility();
    }

    void OnGUI()
    {
        UnityEditor.EditorStyles.textArea.wordWrap = true;
        
        questionKey = UnityEditor.EditorGUILayout.TextArea(questionKey);
        
        questionTitleSpa = UnityEditor.EditorGUILayout.TextArea(questionTitleSpa);
        selected = UnityEditor.EditorGUILayout.Popup("Tipo de la pregunta:", selected, ALL_TYPES);
        
        switch(selected)
        {
            case (int)Question.QuestionTypes.Test:
                OnGUI_Test();
                break;
            case (int)Question.QuestionTypes.MultipleChoice:
                OnGUI_MultiChoice();
                break;
            case (int)Question.QuestionTypes.Order:
                break;
            case (int)Question.QuestionTypes.FillGaps:
                break;
        }

        if (GUILayout.Button("Añadir Pregunta!"))
        {
            Debug.Log(questionKey + " | " + questionTitleSpa + " | " + correctAnswers[0]);
        }
    }

    void OnGUI_MultiChoice()
    {
        correctAnswersCount = UnityEditor.EditorGUILayout.IntField("Cuantas respuestas correctas tiene?", correctAnswersCount);
        for (int i = 0; i < correctAnswersCount; ++i)
        {
            correctAnswers[i] = UnityEditor.EditorGUILayout.TextArea(correctAnswers[i]);
        }

        incorrectAnswersCount = UnityEditor.EditorGUILayout.IntField("Cuantas respuestas incorrectas tiene?", incorrectAnswersCount);
        for (int i = 0; i < incorrectAnswersCount; ++i)
        {
            incorrectAnswers[i] = UnityEditor.EditorGUILayout.TextArea(incorrectAnswers[i]);
        }
    }

    void OnGUI_Test()
    {
        correctAnswersCount = 1;
        correctAnswers[0] = UnityEditor.EditorGUILayout.TextArea(correctAnswers[0]);

        incorrectAnswersCount = UnityEditor.EditorGUILayout.IntField("Cuantas respuestas incorrectas tiene?", incorrectAnswersCount);
        for (int i = 0; i < incorrectAnswersCount; ++i)
        {
            incorrectAnswers[i] = UnityEditor.EditorGUILayout.TextArea(incorrectAnswers[i]);
        }
    }

    
}
