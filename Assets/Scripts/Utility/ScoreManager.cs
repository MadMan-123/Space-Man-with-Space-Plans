using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    [SerializeField] private int iCurrentScore = 0;

    [SerializeField] TextMeshPro ScoreUIText;





    void Awake()
    {
        //singleton boilerplate 
        DontDestroyOnLoad(gameObject.transform.parent.gameObject);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }


        UpdateScore();
    }

    public void AddScore(int iScoreToAdd)
    {
        iCurrentScore += iScoreToAdd;

        //change the text
        UpdateScore();
    }

    public void UpdateTextOBJ()
    {
        Debug.Log("Updating TextOBJ");

        foreach (TextMeshPro obj in GameObject.FindObjectsOfType<TextMeshPro>())
        {
            if (obj.CompareTag("ScoreText") == true)
            {
                ScoreUIText = obj;
                Debug.Log($"{ScoreUIText.name} is the new text UI");
                break;
            }

        }


        UpdateScore();
    }

    public void UpdateScore()
    {
        ScoreUIText.text = $"SCORE:{iCurrentScore}";
    }

    public int GetScore() => iCurrentScore;

}