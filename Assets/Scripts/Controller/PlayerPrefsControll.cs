using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsControll : MonoBehaviour {

    public static void IsGameStarForTheFirstTime()
    {
        //moi cai app thi cai dat gia tri ban dau la 0
        if (!PlayerPrefs.HasKey("IsGameStarForTheFirstTime"))
        {
            setSound(1);
            PlayerPrefs.SetInt(Util.mapEasy, 0);
            PlayerPrefs.SetInt(Util.mapNormal, 0);
            PlayerPrefs.SetInt(Util.mapHard, 0);
            PlayerPrefs.SetInt("IsGameStarForTheFirstTime", 0); //set lai la false
        }
    }

    public static void _SetHighScore(string namemap,int score)
    {
        PlayerPrefs.SetInt(namemap, score);
    }

    public static int _GetHighScore(string namemap)
    {
        return PlayerPrefs.GetInt(namemap);
    }

    public static void _SetScoreDie(string namemap,int score)
    {
        PlayerPrefs.SetInt(namemap+"scoredie", score);
    }
    public static int _GetScoreDie(string namemap)
    {
        return PlayerPrefs.GetInt(namemap+"scoredie");
    }
    public static void setSound(int vl)
    {
        PlayerPrefs.SetInt("sound", vl);
    }
    public static bool getSound()
    {
        if (PlayerPrefs.GetInt("sound") > 0)
            return true;
        return false;
    }
    //set pipe level - độ khó
    public static void setLevelPipe(int lv)
    {
        PlayerPrefs.SetInt("levelpipe", lv);
    }
    public static int getLevelPipe()
    {
        return PlayerPrefs.GetInt("levelpipe");
    }
}
