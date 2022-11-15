using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreUpdater : MonoBehaviour
{
    [SerializeField] TextMeshPro TextToUpdate;
    void Start()
    {
        int iScore = SaveSytem.LoadPlayerData().iHighScore;
        if (iScore > 0)
            TextToUpdate.text = $"HIGHSCORE:{iScore}";
        else
            TextToUpdate.text = "";
    }

}
