using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    
    public GameObject[] Categories;
    public Animator[] NavigationButtonAnimator;
    public Animator[] CategoryAnimator;
    public int[] Id = {0,0};

    private void Start()
    {
        NavigationButtonAnimator[0].SetBool("Active" , true);

    }
   
    public void ChangeCategory(int id)
    {
        Id[0] = Id[1];
        NavigationButtonAnimator[Id[0]].SetBool("Active", false);
        Categories[Id[0]].SetActive(false);
        Id[1] = id;
        NavigationButtonAnimator[Id[1]].SetBool("Active" , true);
        Categories[Id[1]].SetActive(true);
        CategoryAnimator[Id[1]].Play("CategoryOut");
    }
}
