using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MarkUsage : MonoBehaviour
{
    public Text MarkText;
    public int Mark;
    public int MarkId;
    
    private void Start()
    {
        MarkText.text = Mark.ToString();
        Debug.Log("Check");
    }
    public void ClaimMark()
    {
        Test.Gems += Mark;

        Test.MarkList.RemoveAt(MarkId);        
        FillDiary fillDiary = transform.GetComponentInParent<FillDiary>();
        fillDiary.Clear();
        fillDiary.Fill();

    }

  
}
