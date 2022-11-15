using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIHandler : MonoBehaviour
{
    [SerializeField] Health _health;

    Image HealthBar;

    void Start()
    {
        TryGetComponent<Image>(out HealthBar);
    }
    // Update is called once per frame
    void Update()
    {
        if (_health)
        {
            HealthBar.fillAmount = (float)_health.GetHealth() / (float)_health.fMaxHealth;
        }
        else
        {
            HealthBar.fillAmount = 0;
        }
    }
}
