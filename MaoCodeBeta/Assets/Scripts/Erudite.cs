using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime;

public class Erudite : MonoBehaviour
{
    public GameTabNavigation gameTabNavigation;
    [SerializeField] Color[] ButtonColor; 
    public Text[] TaskText;
    public Image[] RoundIndicator;
    public Image[] AnswerButtons;
    int RightAnswer;
    bool[] AnswersEmpty = {false , false ,false ,false };
    int[] AllAnswers ;
    int[] Rounds = {0,0 };
    private void Awake()
    {
        StartCoroutine(FillForm(0));
        StartCoroutine(EnemyTurn());
    }
        IEnumerator FillForm(float Delay)
        {
            yield return new WaitForSeconds(Delay);
            ClearAnswers();
             StopAllCoroutines();
             StartCoroutine(EnemyTurn());
             (string Task, int Answer, int[] Answers) = GenerateTask();
            TaskText[0].text = Task;
            RightAnswer = Answer;
            AllAnswers = Answers;
            for (int i = 1; i < 5; i++)
            {
                TaskText[i].text = Answers[i - 1].ToString();
            }
    }
    public void Answer(int id)
    {
        if (AllAnswers[id] == RightAnswer)
        {

            Indicator(1);
            StartCoroutine(FillForm(1));
            FillAnswer(id, true, 1);
        }
        else
        {
            Indicator(0);   
            StartCoroutine(FillForm(1));
            FillAnswer(id, true, 1);

        }
    }
    public void HelperAnswer()
    {
        if (Test.Helpers >= 1)
        {
            Test.Helpers -= 1;
            for (int i = 0; i < 4; i++)
            {
                if (AllAnswers[i] == RightAnswer)
                {
                    Indicator(1);
                    StartCoroutine(FillForm(1));
                    FillAnswer(i, true, 1);
                }
            }
        }
    }
    
    public (string,int,int[]) GenerateTask()
    {
        int Result = 0;
        int[] Number = { 0, 0, 0 };
        for (int i = 0; i < 3; i++)
        {
            Number[i] =  Random.Range(0,100);
            Debug.Log(Number[i]);
        }      
        string Task = (Number[0].ToString() + "+" + Number[1].ToString() + "-" + Number[2].ToString() );
        Result = (Number[0] + Number[1] - Number[2]);

        int[] GenerateAnswers()
        {
            int[] Answers = { 0, 0, 0, 0 };
            Answers[Random.Range(0, 4)] = Result;
            for ( int a = 0; a <=3; a++)
            {
                if (Answers[a] != Result)
                {                                       
                    Answers[a]= Result + Random.Range(-11, 11);      
                }
            }
            Debug.Log(Task);
            return Answers;
        }
        return (Task , Result , GenerateAnswers()) ;       
    }
    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(5);
        int i = 0;
        bool access = false;
        while (access == false)
        {
            i = Random.Range(0, 4);
            if (AnswersEmpty[i] == false)
            {
                FillAnswer(i,true , 1);
                StartCoroutine(EnemyTurn());
                access = true;
            }
            if(AllAnswers[i] == RightAnswer)
            {
                Indicator(0);
                StartCoroutine(FillForm(1));
            }           
        }      
    }
    public void ClearAnswers() { 
        for (int i = 0; i < 4; i++)
        {
            FillAnswer(i, false , 0);
        }
    }
    public void FillAnswer(int AnswerId , bool Fill , int ColorId )
    {
        AnswersEmpty[AnswerId] = Fill;
        AnswerButtons[AnswerId].color = ButtonColor[ColorId];
        AnswerButtons[AnswerId].GetComponent<Button>().enabled = !Fill;
    }
    void Indicator(int Player)
    {
        Rounds[Player] += 1;
        for (int i = 0; i < Rounds[Player]; i++)
        {
            RoundIndicator[i + Player * 3].color = new Vector4(255, 255, 255, 255);
        }
        if (Rounds[1] == 3)
        {
           StartCoroutine(gameTabNavigation.EndScreen("Ты выйграл"));
            Test.Points += 100;
        }
        if (Rounds[0] == 3)
        {
            StartCoroutine(gameTabNavigation.EndScreen("Ты проиграл"));
            Test.Points += 25;
        }
    }
}
