using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float fMaxHealth = 100f;
    [SerializeField] bool bIsPlayer = false;

    [SerializeField] DeathMenuScript deathMenuScript;
    [SerializeField] GameObject DeathAnimation;
    bool bShouldrun = true;

    private float fHealth = 0f;
    void Start()
    {
        //set the health to be max health
        fHealth = fMaxHealth;
    }

    public float GetHealth()
    {
        return fHealth;
    }
    public void TakeDamage(float fDamage)
    {
        fHealth -= fDamage;

        //play sound effect here 
        if (TryGetComponent<AudioSource>(out AudioSource source) && bIsPlayer)
        {
            source.Play();
        }


        if (fHealth <= 0)
        {
            HandleDeath();
        }



    }

    public void ResetHealth()
    {
        fHealth = fMaxHealth;
    }

    void HandleDeath()
    {
        if (TryGetComponent<Astroid>(out Astroid a))
        {
            ScoreManager.Instance.AddScore(a.iScore);

            a.HandleDeath();

            Destroy(a.gameObject);
            return;
        }
        //check if the health is an enemies
        else if (TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.HandleDeath();
            if (ScoreManager.Instance)
                ScoreManager.Instance.AddScore(enemy.iScore);

            return;
        }
        else if (TryGetComponent<EnemyAI>(out EnemyAI enemyAI))
        {
            enemyAI.HandleDeath();
            if (ScoreManager.Instance)
                ScoreManager.Instance.AddScore(enemyAI.iScore);

            return;
        }
        else if (bIsPlayer)
        {
            StartPlayerDeathProccess();
            if (GameManager.state == GameState.TopDownShooter && bShouldrun)
            {
                bShouldrun = false;
                //spawn Death Animation
                if (TryGetComponent<HideBody>(out HideBody hide))
                {
                    hide.StartHideBody();
                    GameObject obj = Instantiate(DeathAnimation, transform.position, transform.rotation);

                }


                Invoke(nameof(StartPlayerDeathProccess), 2f);
                Invoke(nameof(DestroyThis), 3f);

            }
        }


        if ((GameManager.state == GameState.ScrollingShooter))
        {
            Destroy(gameObject);
        }


    }

    void StartPlayerDeathProccess()
    {
        if (GameManager.Instance)
            GameManager.Instance.SavePlayerData();
        deathMenuScript.StartDeathMenu();
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }

}
