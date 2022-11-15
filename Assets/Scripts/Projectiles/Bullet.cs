using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float fDamage = 10f;
    [SerializeField] private float fSpeed = 100f;
    [SerializeField] private float fTimeTillDestroy = 5f;
    [SerializeField] bool bShouldHurtPlayer = false;

    private Rigidbody2D _rb;

    public Vector2 Dir;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        //play sound effect here 
        Fire();

    }

    void Fire()
    {
        _rb.AddForce(Dir * fSpeed);

        Invoke(nameof(HandleDestroy), fTimeTillDestroy);
    }

    private void HandleDestroy()
    {
        CancelInvoke(nameof(HandleDestroy));
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((!bShouldHurtPlayer && other.CompareTag("Player"))) return;

        else if (other.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(fDamage);
        }


        HandleDestroy();

    }
}
