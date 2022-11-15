using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{
    public float fDamage = 10f;
    [SerializeField] private float fSpeed = 50f;
    [SerializeField] private float fTimeTillDestroy = 5f;
    [SerializeField] bool bShouldHurtPlayer = false;

    [SerializeField] float fXAxisRandMax = 0;

    private Rigidbody2D _rb;

    public Vector2 Dir;

    // Start is called before the first frame update
    void Start()
    {
        fXAxisRandMax = Random.Range(-fXAxisRandMax, fXAxisRandMax);
        Dir = new Vector2(fXAxisRandMax, Dir.y);
        //play sound effect here 
        if (TryGetComponent<AudioSource>(out AudioSource source))
        {
            source.Play();
        }

        Fire();

    }

    void Fire()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0.5f;

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
