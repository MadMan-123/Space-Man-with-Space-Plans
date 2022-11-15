using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float fTimeToDestroy = .75f;
    void Start()
    {

        Explode();
        Invoke(nameof(DestroyThis), fTimeToDestroy);

    }

    void Explode()
    {
        if (TryGetComponent<AudioSource>(out AudioSource source))
        {
            source.Play();
        }

    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }

}
