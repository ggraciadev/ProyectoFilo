using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using WiruLib;

[System.Serializable]
public class Question
{
    public enum QuestionTypes { Test, MultipleChoice, Order, FillGaps, End};

    [SerializeField] string path;

    [SerializeField] QuestionTypes questionType;
    public QuestionTypes QuestionType { get { return questionType; } }

    [SerializeField] string key;
    public string Key { get { return key; } }

    [SerializeField] List<string> answersKeys;
    public string[] AnswersKeys { get { return answersKeys.ToArray(); } }
    [SerializeField] List<int> correctAnswers;
    ButtonManager[] buttons;
    List<int> selectedAnswers;

    bool checkedQuestion;


    public Question()
    {
        selectedAnswers = new List<int>();
        answersKeys = new List<string>();
        correctAnswers = new List<int>();
    }

    public void Init()
    {
        checkedQuestion = false;
        selectedAnswers.Clear();
        buttons = GameObject.FindObjectsOfType<ButtonManager>();
        for(int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].ID = i;
            buttons[i].SetQuestion(this);
        }
        DiselectAllButtons();
    }

    public bool CheckAnswers()
    {
        int corrects = 0;

        switch(questionType)
        {
            case QuestionTypes.Test:
                if(buttons[selectedAnswers[0]].text.GetKey() == answersKeys[correctAnswers[0]])
                {
                    corrects++;
                    buttons[selectedAnswers[0]].ChangeState(ButtonManager.ButtonState.Correct);
                }
                else
                {
                    buttons[selectedAnswers[0]].ChangeState(ButtonManager.ButtonState.Incorrect);
                }
                break;
            case QuestionTypes.MultipleChoice:
                for(int i = 0; i < selectedAnswers.Count; ++i)
                {
                    bool correct = false;
                    for(int j = 0; j < correctAnswers.Count && !correct; ++j)
                    {
                        if(buttons[selectedAnswers[i]].text.GetKey() == answersKeys[correctAnswers[j]])
                        {
                            correct = true;
                            corrects++;
                            buttons[selectedAnswers[i]].ChangeState(ButtonManager.ButtonState.Correct);
                        }
                    }
                    if(!correct)
                    {
                        buttons[selectedAnswers[i]].ChangeState(ButtonManager.ButtonState.Incorrect);
                    }
                }
                break;
            case QuestionTypes.Order:
                break;
            case QuestionTypes.FillGaps:
                break;
        }
        
        return corrects == correctAnswers.Count;
    }

    public bool CheckCorrect()
    {
        if(CheckAnswers())
        {
            checkedQuestion = true;
            return true;
        }
        return false;
    }

    public void DiselectButton(ButtonManager bm)
    {
        if (checkedQuestion) return;
        int temp = -1; 
        bm.ChangeState(ButtonManager.ButtonState.Normal);

        for(int i = 0; i < selectedAnswers.Count; ++i)
        {
            if(temp != -1)
            {
                buttons[selectedAnswers[i]].SetOrder(i - 1);
            }
            else if(buttons[selectedAnswers[i]] == bm)
            {
                temp = i;
            }

        }
        if(temp != -1)
            selectedAnswers.RemoveAt(temp);
    }

    public void DiselectAllButtons()
    {
        for (int i = 0; i < buttons.Length; ++i)
        {
            DiselectButton(buttons[i]);
        }
    }

    public void SelectButton(ButtonManager bm)
    {
        if (checkedQuestion) return;
        switch (questionType)
        {
            case Question.QuestionTypes.Test:
                DiselectAllButtons();
                if (selectedAnswers.Count == 1) selectedAnswers[0] = bm.ID;
                else selectedAnswers.Add(bm.ID);
                break;
            case Question.QuestionTypes.MultipleChoice:
                selectedAnswers.Add(bm.ID);
                break;
            case QuestionTypes.Order:
                bm.SetOrder(selectedAnswers.Count);
                selectedAnswers.Add(bm.ID);
                break;
        }
        bm.ChangeState(ButtonManager.ButtonState.Selected);
        string temp = "Respuestas seleccionadas: ";
        for (int i = 0; i < selectedAnswers.Count; ++i)
        {
            temp += selectedAnswers[i] + " | ";
        }
        Debug.Log(temp);
    }

    public void LoadQuestion(string _path)
    {
        path = _path;
        answersKeys.Clear();
        correctAnswers.Clear();

        TextAsset file = Resources.Load<TextAsset>("Questions/" + path);
        string[] lines = file.text.Split('\n');
        key = lines[1];
        questionType = StringToQuestionType(lines[4]);
        int count = 0;
        int i = 7;
        while(lines[i] != "")
        {
            answersKeys.Add(lines[i]);
            correctAnswers.Add(count);
            ++i;
            ++count;
        }
        i += 2;

        while (lines[i] != "")
        {
            answersKeys.Add(lines[i]);
            ++i;
        }
    }

    public void SetQuestionType(QuestionTypes qt)
    {
        questionType = qt;
    }



    public static QuestionTypes StringToQuestionType(string s)
    {
        QuestionTypes ret = QuestionTypes.End;

        switch(s)
        {
            case "Test":
                ret = QuestionTypes.Test;
                break;
            case "MultipleChoice":
                ret = QuestionTypes.MultipleChoice;
                break;
            case "Order":
                ret = QuestionTypes.Order;
                break;
            case "FillGaps":
                ret = QuestionTypes.FillGaps;
                break;
            default:
                break;
        }

        return ret;
    }
}
