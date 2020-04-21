using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestionGenerator : UnityEditor.EditorWindow
{
    string[] ALL_TYPES = { "Test", "MultipleChoice", "Order", "FillGaps" };
    int selected;
    string questionKey = "Key de la Pregunta";
    string tema = "Tema";
    string asignatura = "Asignatura";
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

        window.asignatura = "Asignatura";
        window.tema = "Tema";
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

        asignatura = UnityEditor.EditorGUILayout.TextArea(asignatura);
        tema = UnityEditor.EditorGUILayout.TextArea(tema);

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
                OnGUI_Order();
                break;
            case (int)Question.QuestionTypes.FillGaps:
                break;
        }

        if (GUILayout.Button("Añadir Pregunta!"))
        {
            AddToQuestionList();
            AddEntryToCSV();
            CreateQuestionFile();
            Debug.Log(questionKey + " | " + questionTitleSpa + " | " + correctAnswers[0] + " Creada correctamente!");
        }
    }

    void OnGUI_Order()
    {
        correctAnswersCount = UnityEditor.EditorGUILayout.IntField("Respuestas en orden correcto", correctAnswersCount);
        for (int i = 0; i < correctAnswersCount; ++i)
        {
            correctAnswers[i] = UnityEditor.EditorGUILayout.TextArea(correctAnswers[i]);
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

    void AddToQuestionList()
    {
        System.IO.StreamWriter file = new StreamWriter("Assets/Resources/Questions/List.txt", true);
        file.Write(asignatura + "/" + tema + "/" + questionKey + "\n");
        file.Close();
    }

    void CreateQuestionFile()
    {
        if(!Directory.Exists("Assets/Resources/Questions/" + asignatura + "/" + tema))
            System.IO.Directory.CreateDirectory("Assets/Resources/Questions/" + asignatura + "/" + tema);

        StreamWriter file = File.CreateText("Assets/Resources/Questions/" + asignatura + "/" + tema + "/" + questionKey + ".txt");
        file.Write("-- Key de la pregunta --\n");
        file.Write(asignatura + "_" + tema + "_" + questionKey + "\n");
        file.Write("\n-- Tipo de pregunta --\n");
        file.Write(ALL_TYPES[selected] + "\n");
        file.Write("\n-- Respuestas Correctas en orden --\n");
        for (int i = 0; i < correctAnswersCount; ++i)
        {
            file.Write(asignatura + "_" + tema + "_" + questionKey + "_Correct" + i + "\n");
        }
        file.Write("\n-- Respuestas Incorrectas en orden --\n");
        for (int i = 0; i < incorrectAnswersCount; ++i)
        {
            file.Write(asignatura + "_" + tema + "_" + questionKey + "_Incorrect" + i + "\n");
        }

        file.Close();
    }

    void AddEntryToCSV()
    {
        System.IO.StreamWriter file = new StreamWriter("Assets/Resources/Localization/Localization.csv", true);
        file.Write("\n,,,");
        file.Write("\n" + asignatura + "_" + tema + "_" + questionKey + ",," + questionTitleSpa + ",");
        for(int i = 0; i < correctAnswersCount; ++i)
        {
            file.Write("\n" + asignatura + "_" + tema + "_" + questionKey + "Correct" + i  + ",," + correctAnswers[i] + ",");
        }
        for (int i = 0; i < incorrectAnswersCount; ++i)
        {
            file.Write("\n" + asignatura + "_" + tema + "_" + questionKey + "Incorrect" + i + ",," + incorrectAnswers[i] + ",");
        }
        file.Close();
    }
}
