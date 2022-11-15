using System.Collections;
using UnityEngine;
public class AstroidSpawner : MonoBehaviour
{
    [Header("Object template")]
    public GameObject ObjectTemplate;
    [Header("Positions")]
    [SerializeField] Transform OriginSpawnPos;
    [Header("Data")]
    public float fSeccondsToWait = 10f;

    void Start()
    {
        StartCoroutine(nameof(SpawnInAstroids));
    }

    IEnumerator SpawnInAstroids()
    {
        while (true)
        {
            Instantiate(ObjectTemplate, OriginSpawnPos);
            yield return new WaitForSeconds(fSeccondsToWait);
        }
    }
}
