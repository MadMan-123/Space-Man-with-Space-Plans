using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Astroid : MonoBehaviour
{
    [SerializeField] GameObject SmallAstroid;
    [SerializeField] float fDamage = 25f;
    [SerializeField] bool bIsSmall = false;
    public int iScore { get; private set; } = 5;

    [SerializeField] float fSpeed = 10;
    [SerializeField] float fRandX, fRandY;

    private Rigidbody2D rb;

    void Start()
    {

        fRandX = ((Random.Range(-(4), 4)));
        fRandY = ((Random.Range(-(4), 4)));

        fSpeed = Random.Range(-40, 40);

        TryGetComponent<Rigidbody2D>(out rb);

        if (bIsSmall)
        {
            rb.AddForce(new Vector2(fRandX, fRandY) * fSpeed);
        }
        else
        {
            rb.AddForce(new Vector2(0, -1) * fSpeed);
            transform.position = new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y);
        }

        Destroy(gameObject, 20);

    }



    public void HandleDeath()
    {
        if (bIsSmall) return;

        for (int i = 0; i < 3; i++)
        {

            var obj = Instantiate(SmallAstroid, transform.position, Quaternion.identity);

        }
        Destroy(gameObject);

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && other.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(fDamage);

            HandleDeath();
            Destroy(gameObject);

        }


    }


};