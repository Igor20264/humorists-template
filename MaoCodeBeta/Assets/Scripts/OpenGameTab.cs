using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OpenGameTab : MonoBehaviour
{
    public GameObject[] Game;
    public Transform Parent;
   public void StartGame(int GameId)
    {   
        var child = Instantiate(Game[GameId], Parent.transform.position , Quaternion.identity);
        child.transform.SetParent(Parent);
        child.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
