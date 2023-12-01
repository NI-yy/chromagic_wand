using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWaterController : MonoBehaviour
{
    public bool isRight;
    [SerializeField] GameObject SplashCollider;
    [SerializeField] GameObject bulletColldier;
    [SerializeField] float speed;
    [SerializeField] float lifeTime;

    private void Start()
    {
        
    }

    public void ActiveBulletCollider()
    {
        bulletColldier.SetActive(true);
        if (isRight)
        {
            bulletColldier.GetComponent<Rigidbody2D>().AddForce(new Vector3(speed, 0f, 0f), ForceMode2D.Impulse);
        }
        else
        {
            bulletColldier.GetComponent<Rigidbody2D>().AddForce(new Vector3(-speed, 0f, 0f), ForceMode2D.Impulse);
        }

        Destroy(bulletColldier, lifeTime);
        Destroy(SplashCollider, lifeTime);
        Destroy(SplashCollider, lifeTime + 0.1f);
    }
}
