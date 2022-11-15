using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string sName;
    public int iHighScore;

    public PlayerData(ScoreManager scoreManager, string sCurrentName)
    {
        sName = sCurrentName;
        iHighScore = scoreManager.GetScore();
    }

}
