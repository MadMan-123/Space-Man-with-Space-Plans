using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandler : MonoBehaviour
{
    [SerializeField] GameObject Projectile;
    [SerializeField] Transform FiringOrigin;
    [SerializeField] int iFiringMouseButton = 0;
    public float fDamage = 20;
    [SerializeField] float fFireRate = .5f;
    [SerializeField] bool bIsAI = true;
    public Vector2 Dir;
    public bool _bCanFire = true;



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

        if (Spawned.TryGetComponent<Bullet>(out Bullet bullet))
        {
            bullet.fDamage = fDamage;
            if (bIsAI)
            {
                bullet.Dir = Dir;
            }
            else
                bullet.Dir = new Vector2(FiringOrigin.transform.up.x, FiringOrigin.transform.up.y);
        }

        yield return new WaitForSeconds(fFireRate);
        _bCanFire = true;
    }
}
