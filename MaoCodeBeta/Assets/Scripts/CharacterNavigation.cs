using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterNavigation : MonoBehaviour
{
    public Sprite[] CharacterSprite;
    public string[] CharacterName;
    public string[] Description;
    private int ChosenCharacter;
    private int[] Id = {0 , 0 };
    private int a;
    public Button ChooseButton;

    public Text CharacterNameText;
    public Text CharacterInformationText;
    public Image CharacterAvatarImage;
    public void ChooseCharacter()
    {
        PlayerPrefs.SetInt("Character" , a );
        Debug.Log(a);
    }
    public void ChangeCharacter()
    {       
        if (a == 1)
        {          
            CharacterNameText.text = CharacterName[0];
            CharacterInformationText.text = Description[0];
            CharacterAvatarImage.sprite = CharacterSprite[0];
            a = 0;          
        }
        else
        {
            CharacterNameText.text = CharacterName[1];
            CharacterInformationText.text = Description[1];
            CharacterAvatarImage.sprite = CharacterSprite[1];
            a = 1;
        }
       
    }
}
