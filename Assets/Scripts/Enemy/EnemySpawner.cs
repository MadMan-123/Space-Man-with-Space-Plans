using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] float fSeccondsToWait = 5f;
    [SerializeField] float fDistanceFromPlayer = 10f;
    [SerializeField] Transform Center;
    public int iAmmountToPool = 10;
    public GameObject[] Pool;
    //private members

    void Awake()
    {
        PopulatePool();
    }

    void Start()
    {

        StartCoroutine(nameof(SpawnObjects));
    }


    public IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(fSeccondsToWait);
            EnableObjectInPool();

        }
    }

    void PopulatePool()
    {
        Pool = new GameObject[iAmmountToPool];

        for (int i = 0; i < Pool.Length; i++)
        {
            Pool[i] = Instantiate(EnemyPrefab, transform);
            Pool[i].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < Pool.Length; i++)
        {
            if (Pool[i].activeInHierarchy == false)
            {
                //restart health

                float fRot = Random.Range(0, 360);


                if (Center)
                {
                    //spawn pos
                    Vector2 NewSpawnPos = new Vector2(
                        fDistanceFromPlayer * Mathf.Sin(fRot * Mathf.Deg2Rad) + Center.position.x,
                        fDistanceFromPlayer * Mathf.Cos(fRot * Mathf.Deg2Rad) + Center.position.y
                        );
                    Pool[i].transform.position = NewSpawnPos;
                    if (Pool[i].TryGetComponent<Health>(out Health health))
                    {
                        health.ResetHealth();
                    }

                    Pool[i].SetActive(true);
                }






                return;
            }
        }
    }

}
