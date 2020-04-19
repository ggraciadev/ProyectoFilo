using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using WiruLib;

public class Question
{
    public enum QuestionTypes { Test, MultipleChoice, Order, FillGaps, End};

    QuestionTypes questionType;
    WiruLocalizeText title;
    WiruLocalizeText[] answers;
    int[] correctAnswers;

    public Question()
    {

    }

    public void SetQuestionType(QuestionTypes qt)
    {
        questionType = qt;
    }

    public void SetQuestion(string key)
    {
        title.SetKey(key);



    }
}
