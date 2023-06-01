using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    public Text[] InfoText;
    string Json;

    public static string Name = " ";
    public static int Helpers = 0;
    public static int Points = 0;
    public static int Gems = 0;
    public static List<int> MarkList = new List<int> {5};
    public TestClass testClass;
    void Start()
    {
        if (PlayerPrefs.HasKey("Statistics"))
        {
            Debug.Log("Data exist");
            Json = PlayerPrefs.GetString("Statistics");
            testClass = JsonUtility.FromJson<TestClass>(Json);
            Helpers = testClass.helpers;
            Name = testClass.name;
            Points = testClass.pts;
            Gems = testClass.gems;
            MarkList = testClass.mark;
        }
        else
        {
            Debug.Log("Data doesn't exist");
            testClass = new TestClass();
            testClass.name = "Тестовый пользователь ";
            testClass.pts = 0;
            testClass.gems = 0;
            testClass.helpers = 0;
            testClass.mark = new List<int> { 5 };
            Json = JsonUtility.ToJson(testClass);
            PlayerPrefs.SetString("Statistics", Json);
            Helpers = testClass.helpers;
            Name = testClass.name;
            Points = testClass.pts;
            Gems = testClass.gems;
            MarkList = testClass.mark;
        }
       
    }
    void Update()
    {
        InfoText[0].text = Name;
        InfoText[1].text = Points.ToString();
        InfoText[2].text = Gems.ToString();

        testClass.helpers = Helpers;
        testClass.name = Name;
        testClass.pts = Points;
        testClass.gems = Gems;
        testClass.mark = MarkList;
        Json = JsonUtility.ToJson(testClass);
        PlayerPrefs.SetString("Statistics", Json);
    }
    [SerializeField] public class TestClass{

        public string name;
        public int helpers;
        public int pts;
        public int gems;
        public List<int> mark;


}
}
