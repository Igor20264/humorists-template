using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTabNavigation : MonoBehaviour
{
    public GameObject[] Tabs;
    public Text[] FarewellText;
    public Image[] Enemy;
    public Sprite[] EnemySprite;


    string Fact;
    int EnemyId;

    void Awake()
    {
        EnemyId = Random.Range(0, 2);
        Enemy[0].sprite = EnemySprite[EnemyId];  
    }
    public void StartGame()
    {
        Tabs[1].SetActive(true);
        Enemy[1].sprite = Enemy[0].sprite;      
        Tabs[0].SetActive(false);
        if (EnemyId == 0)
        {
            Fact = GetComponentInParent<FactRandomizer>().Fact(0, 24);
        }
        if (EnemyId == 1)
        {
            Fact = GetComponentInParent<FactRandomizer>().Fact(23, 45);
        }
    }
    public IEnumerator EndScreen(string Farewall  )
    {
        yield return new WaitForSeconds(1);
        Tabs[1].SetActive(false);
        Tabs[2].SetActive(true);
        FarewellText[0].text = Farewall;
        FarewellText[1].text = Fact;
    }
    public void Finish()
    {
        transform.GetComponentInParent<BarCodeScanner>().TabOpened = false;
        Destroy(Tabs[3]);
    }
}
