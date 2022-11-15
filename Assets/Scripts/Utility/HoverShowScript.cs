using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverShowScript : MonoBehaviour
{
    public static HoverShowScript Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

        }
        Hide();
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);
}
