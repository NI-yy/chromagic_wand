using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SpitPlant : Enemy
{
    [SerializeField]
    float range = 10;
    [SerializeField]
    float attackDelayTime;
    [SerializeField]
    float attackIntervalTime;
    [SerializeField]
    EnemyProjectorData projectorData;
    Vector3 origin;
    Vector3 direction;

    bool interval;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        origin = transform.position;
        direction = GetPlayerDir();
    }

    // Update is called once per frame
    void Update()
    {
        origin = transform.position;
        direction = GetPlayerDir();
        RaycastHit2D hit2D = Physics2D.Raycast(origin, direction,range);
        Debug.DrawRay(origin, direction * range, Color.red);
        if (!interval && hit2D.collider!=null)
        {
            if (hit2D.collider.CompareTag("Player"))
            {
                interval = true;
                StartCoroutine(Attack());
            }
        }

        SetSpriteFlip();
    }

    IEnumerator Attack()
    {
        Signal();
        yield return new WaitForSeconds(attackDelayTime);
        StartFireProjectile(projectorData, new Vector3());
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(attackIntervalTime);
        interval = false;
    }
}
