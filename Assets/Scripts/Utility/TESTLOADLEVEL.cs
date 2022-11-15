using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTLOADLEVEL : MonoBehaviour
{
    public void Load()
    {
        GameManager.Instance.LoadIntoScrollingShooter();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
