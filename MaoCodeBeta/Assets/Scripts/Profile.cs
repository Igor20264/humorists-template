    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.IO;
    using UnityEngine.UI;
public  class Profile : MonoBehaviour
{
    public Text[] InfoText;

    public static bool Logged;
    public static int Points;
    public static int Gems;
    public static int Helpers;
    static string path;
    static string json;
    public static  ProfileStatistics profileStatistics;
    public static List<int> MarkList;

    private void Awake()
    {   
        path = Path.Combine( Application.persistentDataPath +  "/Statistics.json");
        
        json = File.ReadAllText(path);
        profileStatistics = JsonUtility.FromJson<ProfileStatistics>(json);
        Helpers = profileStatistics.helpers;
        Points = profileStatistics.pts;
        Gems = profileStatistics.gems;
        MarkList = profileStatistics.mark;
        Logged = profileStatistics.logged;
     
    }
    private void Update() {
        InfoText[0].text = path;
        InfoText[1].text = Points.ToString();
        InfoText[2].text = Gems.ToString();
        ReWrite();
    }
    public  static void ReWrite()
    {
        profileStatistics.pts = Points;
        profileStatistics.gems = Gems;
        profileStatistics.mark = MarkList;
        string updatedJsonString = JsonUtility.ToJson(profileStatistics);
        File.WriteAllText(path, updatedJsonString);
    }
    
     }
 public class ProfileStatistics
    {
        public bool logged;
        public string name;
        public string surname;
        public int helpers;
        public int pts;
        public int gems;
        public List<int> mark;
    }
