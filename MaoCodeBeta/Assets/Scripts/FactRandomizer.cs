using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class FactRandomizer : MonoBehaviour
{
    
    public string[] Facts;

    
    public string Fact(int min , int max)
    {
        return Facts[Random.Range(min , max)];
    }
}   

