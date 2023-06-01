using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public TicTacToe ticTacToe;
    private byte[] EnemyField = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int LastSquare;
    bool FirstTurn = true;
    public  int GetBestMove(int[] board, int player)
    {
        int opponent = (player == 1) ? 2 : 1;

        bool IsWinner(int[] currBoard, int currPlayer)
        {
            int[][] winningPatterns = new int[][] {
                new int[] { 0, 1, 2 }, new int[] { 3, 4, 5 }, new int[] { 6, 7, 8 },  // Горизонтальные
                new int[] { 0, 3, 6 }, new int[] { 1, 4, 7 }, new int[] { 2, 5, 8 },  // Вертикальные
                new int[] { 0, 4, 8 }, new int[] { 2, 4, 6 }   // Диагонали
            };

            foreach (int[] pattern in winningPatterns)
            {
                if (currBoard[pattern[0]] == currPlayer && currBoard[pattern[1]] == currPlayer && currBoard[pattern[2]] == currPlayer)
                    return true;
            }

            return false;
        }

        int Minimax(int[] currBoard, int depth, bool isMaximizing)
        {
            if (IsWinner(currBoard, player))
                return 1;
            else if (IsWinner(currBoard, opponent))
                return -1;
            else if (!HasEmptyCell(currBoard)) // Нет пустых клеток
                return 0;

            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < currBoard.Length; i++)
                {
                    if (currBoard[i] == 0)
                    {
                        currBoard[i] = player;
                        int score = Minimax(currBoard, depth + 1, false);
                        currBoard[i] = 0;
                        bestScore = Mathf.Max(score, bestScore);
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < currBoard.Length; i++)
                {
                    if (currBoard[i] == 0)
                    {
                        currBoard[i] = opponent;
                        int score = Minimax(currBoard, depth + 1, true);
                        currBoard[i] = 0;
                        bestScore = Mathf.Min(score, bestScore);
                    }
                }
                return bestScore;
            }
        }

        bool HasEmptyCell(int[] currBoard)
        {
            for (int i = 0; i < currBoard.Length; i++)
            {
                if (currBoard[i] == 0)
                    return true;
            }
            return false;
        }

        int bestMove = -1;
        int bestScore = int.MinValue;
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == 0)
            {
                board[i] = player;
                int score = Minimax(board, 0, false);
                board[i] = 0;
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = i;
                }
            }
        }
        return bestMove;
    }

}
