using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    [SerializeField] GameObject Projectile;
    [SerializeField] Transform FiringOrigin;
    [SerializeField] int iFiringMouseButton = 0;
    public float fDamage = 20;
    [SerializeField] float fFireRate = .5f;
    [SerializeField] bool bIsAI = true;

    public Vector2 _Dir = Vector2.up;
    public bool _bCanFire { get; private set; } = true;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!bIsAI && (Input.GetMouseButton(iFiringMouseButton) || Input.GetKey(KeyCode.Space)) && _bCanFire)
        {
            StartCoroutine(Fire());
        }
    }

    public IEnumerator Fire()
    {
        _bCanFire = false;

        GameObject Spawned = Instantiate(Projectile, FiringOrigin.position, Quaternion.identity);

        if (Spawned.TryGetComponent<Projectile>(out Projectile projectile))
        {
            projectile.fDamage = fDamage;
            projectile.Dir = _Dir;
        }

        yield return new WaitForSeconds(fFireRate);
        _bCanFire = true;
    }
}
