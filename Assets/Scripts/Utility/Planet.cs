using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] float fSpeed = 0.5f;

    float fRand = 0;
    bool bIsPlayerOver = false;
    GameObject obj;

    void Start()
    {


    }
    public void SetRand(float fNewRand) => fRand = fNewRand;
    void Update()
    {
        transform.position = new Vector3(
               fRand,
               transform.position.y - (fSpeed * Time.deltaTime)
               );

        if (bIsPlayerOver && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.LoadIntoPlanet();

        }


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bIsPlayerOver = true;
            if (HoverShowScript.Instance)
                HoverShowScript.Instance.Show();
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bIsPlayerOver = false;
            if (HoverShowScript.Instance)
                HoverShowScript.Instance.Hide();
        }



    }
}
