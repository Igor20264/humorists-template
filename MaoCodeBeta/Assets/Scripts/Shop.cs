using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    public GameObject ShopTab;
    public Text[] Indicator;
    private void Update()
    {
        Indicator[0].text = Test.Gems.ToString();
        Indicator[1].text = Test.Helpers.ToString();
    }
    public void BuyHelper()
    {
        if (Test.Gems >= 5)
        {
            Test.Gems -= 5;
            Test.Helpers += 1;
        }
       
    }

   public void Finish()
    {
        GetComponentInParent<BarCodeScanner>().TabOpened = false;
        Destroy(ShopTab);
    }
}
