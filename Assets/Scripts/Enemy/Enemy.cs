using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool bShouldFollowSinPath = true, bShouldFollowRandPath = false;
    public float fSpeed = 1.5f;
    [SerializeField] float fRandMax = 1.5f;
    [SerializeField] float fLifeTime = 17.5f;
    [SerializeField] float fRand;
    [SerializeField] GameObject ExplosionEffect;
    public int iInverseInt = 1;

    public int iScore = 10;

    float fTime;
    public void StartDeath()
    {
        Invoke(nameof(HandleDeath), fLifeTime);
        fRand = Mathf.Round(Random.Range(-(fRandMax), fRandMax));
    }


    // Update is called once per frame
    void Update()
    {
        if (bShouldFollowSinPath)
        {
            transform.position = new Vector3(
                (iInverseInt) * Mathf.Sin(Time.time * (Mathf.Abs(fRand) + 1) * fSpeed) + fRand,
                transform.position.y - (fSpeed * Time.deltaTime)
                );

        }
        else if (bShouldFollowRandPath)
        {
            transform.position = new Vector3(
                 fRand * (fSpeed * Time.deltaTime),
                transform.position.y - (fSpeed * Time.deltaTime)
                );
        }


    }

    public void HandleDeath()
    {
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);

        CancelInvoke(nameof(HandleDeath));

        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && other.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(25);

            gameObject.SetActive(false);


        }

    }


}
