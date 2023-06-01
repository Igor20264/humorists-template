using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Url : MonoBehaviour
{
    public void EnterSite(string URL)
    {
        Application.OpenURL(URL);
    }
}
