using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FillDiary : MonoBehaviour
{
    public List<int> MarksList;
    public List<GameObject> Marks;
    public GameObject MarkPrefab;
    public Transform Parent;
    public RectTransform DiaryLong;
    bool Filled = false;
    
    public void Update()
    {
        if (Test.MarkList != null && Filled ==false)
        {
            Fill();
            Filled = true;
        }
    }
    public void Fill()
    {
        int a = 0;
        
        foreach (int i in Test.MarkList)
        {
            CreateMark(i , a);                 
            a++;
        }
        DiaryLong.transform.Translate (new Vector3(0 , (a / 2 + a % 2) * 560 * -0.38f) , 0 );
        DiaryLong.sizeDelta = new Vector2(670,  (a / 2 + a % 2) * 560);
    }
    private void CreateMark(int MarkCost , int MarkID )
    {
        var child = Instantiate(MarkPrefab, Parent.transform.position, Quaternion.identity);
        child.GetComponent<MarkUsage>().Mark = MarkCost;
        child.GetComponent<MarkUsage>().MarkId = MarkID;
        child.transform.SetParent(Parent);
        child.transform.localScale = new Vector3(1, 1, 1);
    }
    public void AddMark(int Mark)
    {
        Test.MarkList.Add(Mark);
        Clear();
        Fill();
    }
    public void Clear()
    {
        
        foreach(Transform child in Parent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
