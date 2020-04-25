using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public enum ButtonState { Normal, Selected, Correct, Incorrect };
    public WiruLib.WiruLocalizeText text;
    Image graphicButton;

    [Header("Colores del botón")]
    [SerializeField] Color normalColor;
    [SerializeField] Color selectedColor;
    [SerializeField] Color correctColor;
    [SerializeField] Color incorrectColor;

    ButtonState currentState;
    Question question;
    int order;
    int id;

    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    public void SetQuestion(Question q) { question = q; }

    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponentInChildren<WiruLib.WiruLocalizeText>();
        graphicButton = GetComponent<Image>();
    }
    public void SetOrder(int value)
    {
        order = value;
    }

    public void Select()
    {
        if (currentState != ButtonState.Normal)
        {
            question.DiselectButton(this);
        }
        else
        {
            question.SelectButton(this);
        }
    }

    public void ChangeState(ButtonState state)
    {
        currentState = state;
        switch (state)
        {
            case ButtonState.Normal:
                graphicButton.color = normalColor;
                break;
            case ButtonState.Selected:
                graphicButton.color = selectedColor;
                break;
            case ButtonState.Correct:
                graphicButton.color = correctColor;
                break;
            case ButtonState.Incorrect:
                graphicButton.color = incorrectColor;
                break;
        }
    }

    public void PrintMessage(string msg)
    {
        Debug.Log(msg);
    }
}
