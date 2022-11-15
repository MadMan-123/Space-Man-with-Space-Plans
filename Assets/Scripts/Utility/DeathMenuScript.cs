using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathMenuScript : MonoBehaviour
{
    [SerializeField] TextMeshPro HighScore;
    [SerializeField] GameObject MainUI;
    bool bIsDead = false;

    void Update()
    {

        if (bIsDead && Input.GetKeyDown(KeyCode.Space) && GameManager.Instance)
        {
            GameManager.Instance.LoadIntoMainMenu();
        }


    }




    public void StartDeathMenu()
    {


        MainUI.SetActive(false);
        gameObject.SetActive(true);
        HighScore.text = $"CURRENT HIGHSCORE:{GetHighScore()}";
        bIsDead = true;


    }

    int GetHighScore()
    {
        if (SaveSytem.LoadPlayerData() != null)
            return SaveSytem.LoadPlayerData().iHighScore;

        return 0;
    }

}
