using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderSettings : MonoBehaviour
{
    bool status = false;
    public Animator animator;
    public GameObject Tab;
   public void SettingsOut()
    {

        animator.SetBool("Active", !status);
        Tab.SetActive(!Tab.activeSelf);
        status = !status;
    }
}
