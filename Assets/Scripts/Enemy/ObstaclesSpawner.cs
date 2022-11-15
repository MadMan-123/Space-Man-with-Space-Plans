using System.Collections;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [Header("Object template")]
    public GameObject ObjectTemplate;
    [Header("Positions")]
    [SerializeField] Transform OriginSpawnPos;
    [Header("Data")]
    public float fSeccondsToWait = 2f;
    [SerializeField] float fScaleIncrease = 0.5f;
    [Range(-1, 1)][SerializeField] int iInverseInt = 1;

    public int iAmmountToPool = 5;
    public GameObject[] Pool;
    //private members

    void Awake()
    {
        PopulatePool();
    }

    void Start()
    {
        foreach (var g in Pool)
        {
            if (g.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.iInverseInt = iInverseInt;
            }
        }
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
            Pool[i] = Instantiate(ObjectTemplate, transform);
            Pool[i].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < Pool.Length; i++)
        {
            if (Pool[i].activeInHierarchy == false)
            {

                fScaleIncrease += fScaleIncrease / 75;
                //restart health

                Pool[i].transform.position = transform.position;

                if (Pool[i].TryGetComponent<Health>(out Health health))
                {
                    health.fMaxHealth += fScaleIncrease;

                    health.ResetHealth();
                }
                if (Pool[i].TryGetComponent<Enemy>(out Enemy enemy))
                {
                    //handle timeout
                    enemy.StartDeath();

                    //scale
                    enemy.fSpeed += fScaleIncrease / 20;

                    //increase player
                    GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

                    foreach (var Object in objs)
                    {
                        if (Object.TryGetComponent<ProjectileHandler>(out ProjectileHandler ProjectileHan))
                        {
                            ProjectileHan.fDamage += fScaleIncrease;
                        }
                    }

                }
                if (Pool[i].TryGetComponent<Planet>(out Planet planet))
                {
                    planet.SetRand(Random.Range(-2, 2));
                }
                Pool[i].SetActive(true);
                return;
            }
        }
    }


}
