using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiruLib;

public class QuestionManager : MonoBehaviour
{
    const int ASIGNATURA = 0;
    const int TEMA = 1;
    const int PREGUNTA = 2;

    [SerializeField] WiruLocalizeText questionText;
    [SerializeField] WiruLocalizeText[] answersText;
    [SerializeField] List<int> selectedAnswers;

    [SerializeField] Question question;
    [SerializeField] string[] allPaths;
    [SerializeField] string asignatura = "";
    [SerializeField] string tema = "";
    [SerializeField] List<string> currentPaths;

    [SerializeField] GameObject tituloApartado;
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject currentPanel;

    bool checkedAnswer;

    int currentQuestion;

    // Start is called before the first frame update
    void Start()
    {
        asignatura = MainManager.Instance.asignatura;
        tema = MainManager.Instance.tema;
        selectedAnswers = new List<int>();

        SetPaths();
        ShuffleQuestions();
        LoadCurrentQuestion();
    }

    void SetPaths()
    {
        currentPaths = new List<string>();
        TextAsset temp = Resources.Load("Questions/List") as TextAsset;
        allPaths = temp.text.Split('\n');
        
        if(asignatura != "*") {
            foreach (string s in allPaths) {
                string[] info = s.Split('/');
                if (asignatura == info[ASIGNATURA] && (tema == "*" || tema == info[TEMA]))
                {
                    currentPaths.Add(s);
                }
            }
        }
        else
        {
            currentPaths.AddRange(allPaths);
        }
    }

    void GenerateGUI()
    {
        if (tema != "*")
        {
            tituloApartado.GetComponent<WiruLocalizeText>().SetKey(tema);
        }

        for (int i = 0; i < mainPanel.transform.childCount; ++i)
        {
            if (i == (int)question.QuestionType)
            {
                currentPanel = mainPanel.transform.GetChild(i).gameObject;
                currentPanel.SetActive(true);
            }
            else
            {
                mainPanel.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        switch (question.QuestionType)
        {
            case Question.QuestionTypes.Test:
                TestGUI();
                break;
            case Question.QuestionTypes.MultipleChoice:
                TestGUI();
                break;
            case Question.QuestionTypes.Order:
                break;
            case Question.QuestionTypes.FillGaps:
                break;
        }
    }

    void TestGUI()
    {
        currentPanel.transform.Find("SuperiorPanel").gameObject.GetComponentInChildren<WiruLocalizeText>().SetKey(question.Key);
        Transform parent = currentPanel.transform.Find("Panel");
        int currentLenght = parent.childCount;
        int lengthToGo = question.AnswersKeys.Length;

        if (currentLenght < lengthToGo)
        {
            for (int i = currentLenght; i < lengthToGo; ++i)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>("InstPrefabs/AnswerButton"), parent);
            }
        }
        else if (currentLenght > lengthToGo)
        {
            for (int i = currentLenght; i >= lengthToGo; --i)
            {
                Destroy(parent.GetChild(i - 1));
            }
        }
        string[] tempAnswers = ShuffleAnswers(question.AnswersKeys);
        for (int i = 0; i < lengthToGo; ++i)
        {
            parent.GetChild(i).GetComponentInChildren<WiruLocalizeText>().SetKey(tempAnswers[i]);
        }
    }

    void LoadCurrentQuestion()
    {
        checkedAnswer = false;
        selectedAnswers.Clear();
        question.LoadQuestion(currentPaths[currentQuestion]);
        GenerateGUI();
        question.Init();
    }

    void NextQuestion()
    {
        if (++currentQuestion == currentPaths.Count)
        {
            //Finish Exam
        }
        else
        {
            LoadCurrentQuestion();
        }
    }

    void SwapQuestions(int x, int y)
    {
        string temp = currentPaths[x];
        currentPaths[x] = currentPaths[y];
        currentPaths[y] = temp;
    }

    string[] ShuffleAnswers(string[]s)
    {
        for(int i = 0; i < s.Length; ++i)
        {
            int rand = Random.Range(i, s.Length);
            string temp = s[i];
            s[i] = s[rand];
            s[rand] = temp;
        }
        return s;
    }

    void ShuffleQuestions()
    {
        for(int i = 0; i < currentPaths.Count; ++i)
        {
            SwapQuestions(i, Random.Range(i, currentPaths.Count));
        }
    }
    

    public void CheckAnswer()
    {
        bool correct = question.CheckCorrect();
        if (checkedAnswer)
        {
            NextQuestion();
        }
        else
        {
            if (correct)
            {
                Debug.Log("Respuesta Correcta");
                checkedAnswer = true;
            }
            else Debug.Log("Respuesta Incorracta");
        }
        
    }
}
