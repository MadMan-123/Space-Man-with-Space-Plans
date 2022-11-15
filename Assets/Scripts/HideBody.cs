using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBody : MonoBehaviour
{
    [SerializeField] GameObject body;
    public void StartHideBody()
    {
        body.SetActive(false);
        if (TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController.enabled = false;
        }
    }


}
