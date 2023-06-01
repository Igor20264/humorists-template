using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class TicTacToe : MonoBehaviour
{
    [SerializeField] Color[] SymbolColor;  
    public EnemyAi enemyAi;
    public GameTabNavigation gameTabNavigation;
    public Sprite[] Symbol;
    public Image[] Square; 
    public Image[] RoundIndicator;
    public GameObject WinTab; 
    public int[] Field = { 0, 0, 0, 0, 0, 0, 0, 0, 0 }; 
    bool EnemyAction = false;
    int Result;
    int[] Rounds = {0 , 0 };
    public void StartGame()
    {
        Clear();
    }
    public void PlayerCross(int SquareId)
    {
        PlaceSymbol(SquareId, 0, 2 , 1); 
        Field[SquareId] = 2; 
        Square[SquareId].GetComponent<Button>().enabled = false; 
        var FreeSquares = SquaresCheck();
        Result = Check();

        if (FreeSquares > 1 && Result == 0)
        {
            EnemyTurn();
        }
        if (FreeSquares == 0 && Result == 0)
        {
            Clear();
        }
        Result = Check();
        if ( Result == 1)
        {
            Indicator(0);
           
        }
        if (Result == 2)
        {
            Indicator(1);
           
        }
        
    }

    public void HelperCross()
    {
        if (Test.Helpers != 0)
        {
            Test.Helpers -= 1;
            int a = enemyAi.GetBestMove(Field, 2);
            Field[a] = 2;
            PlaceSymbol(a, 0, 2, 1);
            var FreeSquares = SquaresCheck();
            Result = Check();
            if (FreeSquares > 1 && Result == 0)
            {
                EnemyTurn();
            }
            if (FreeSquares == 0 && Result == 0)
            {
                Clear();
            }
            Result = Check();
            if (Result == 1)
            {
                Indicator(0);

            }
            if (Result == 2)
            {
                Indicator(1);

            }
        }
    }
    void EnemyTurn()
    {
        bool Access = false; 
        while (Access == false)  
        {
            int EnemySquare;
            if (EnemyAction == false)
            {
                EnemySquare = Random.Range(0, 9);          
            }
            else
            {
                int a = Random.Range(0,100);
                if (a <=50)
                {
                    EnemySquare = enemyAi.GetBestMove(Field, 1); 
                }
                else 
                {
                    EnemySquare = Random.Range(0, 9);
                }
            }
            if (Field[EnemySquare] == 0) 
            {
                
                Field[EnemySquare] = 1;
                PlaceSymbol(EnemySquare, 2, 4 , 1);
                Square[EnemySquare].GetComponent<Button>().enabled = false; 
                Access = true; 
                EnemyAction = true;
            }
        }
       
        if (Result >= 1)
        {
            WinTab.SetActive(true);
        }
    }
    int SquaresCheck() 
    {
        byte FreeSquares = 0; 
        foreach (int i in Field) 
        {
            if (i == 0)
            {
                FreeSquares += 1; 
            }            
        }
        return FreeSquares;
    }
     void Clear()  
    {     
        for (byte i = 0; i < 9;) 
        {
            Field[i] = 0; 
            Square[i].GetComponent<Button>().enabled = true; 
            PlaceSymbol(i , 4 , 4 , 0);
            i += 1;
        }
    }
    void PlaceSymbol(int SquareId ,int Max ,int Min , int Color )
    {
        Square[SquareId].color = SymbolColor[Color];
        Square[SquareId].sprite = Symbol[Random.Range(Max,Min)];
    }                           
     bool CheckWin ( int i)
    {
        if (Field[i] != 0)
        {
            if ((i == 0 || i % 3 == 0) && (Field[i] == Field[i + 1] && Field[i] == Field[i + 2]))
            {
                return true;
            }
            else if ((i >= 0 && i <= 2) && (Field[i] == Field[i + 3] && Field[i] == Field[i + 6]))
            {
                return true;
            }           
            else if (i == 4)
            {
                if (Field[i] == Field[i - 2] && Field[i] == Field[i + 2])
                {
                    return true;
                }
                else if (Field[i] == Field[i - 4] && Field[i] == Field[i + 4])
                {
                    return true;
                }
            }
        }
        return false;
    }       
      int Check()
    {
        foreach (int i in new int[]  { 0, 1, 2, 3, 4, 6 })
        {
            var result = CheckWin(i);
            if (result == true )
            {
                return Field[i];
            }
        }
        return 0;
    }

    void Indicator(int Player)
    {
        Rounds[Player] += 1;
        for (int i = 0; i < Rounds[Player]; i++)
        {
            RoundIndicator[i + Player * 3].color = new Vector4(255, 255, 255, 255);
        }
        if(Rounds[1] == 3)
        {
           StartCoroutine( gameTabNavigation.EndScreen("Ты выйграл"));
            Test.Points += 100;
        }
        if(Rounds[0] == 3)
        {
            StartCoroutine(gameTabNavigation.EndScreen("Ты проиграл"));
            Test.Points += 25;
        }
        if (Rounds[Player] != 3)
        {
            Clear();
        }
    }  
}

    


    




