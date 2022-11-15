using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GunHandler gunHandler;
    [SerializeField] float fSpeed = 2.5f;
    [SerializeField] float fDistanceToStop = 5f;
    [SerializeField] GameObject DeathAnimation;
    public int iScore = 10;

    private Transform Target;
    private Vector2 Dir;
    float fSQRDistance = 0f;
    float fRot;
    void Start()
    {
        //set the target to player
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (!Target) return;
        fSQRDistance = Vector2.SqrMagnitude(Target.position - transform.position);
        Dir = (Target.position - transform.position).normalized;


        if (fSQRDistance > fDistanceToStop * fDistanceToStop)
        {
            //move 
            transform.position += new Vector3(Dir.x, Dir.y, 0) * fSpeed * Time.deltaTime;
        }
        else if (fSQRDistance <= fDistanceToStop * fDistanceToStop)
        {
            //stop and shoot
            gunHandler.Dir = Dir;


            if (gunHandler._bCanFire)
                StartCoroutine((gunHandler.Fire()));

        }

        //rotate to player
        fRot = Mathf.Atan2((Target.position.y - transform.position.y),
        (Target.position.x - transform.position.x));
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, (fRot * Mathf.Rad2Deg) - 90), Time.time);
    }

    public void HandleDeath()
    {
        //play death animation
        GameObject obj = Instantiate(DeathAnimation, transform.position, transform.rotation);

        //set active false
        gameObject.SetActive(false);
    }

}
