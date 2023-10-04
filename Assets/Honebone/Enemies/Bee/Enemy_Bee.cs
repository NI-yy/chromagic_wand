using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bee : Enemy
{
    [SerializeField]
    float engageRange;
    [SerializeField]
    float disengageRange;
    [SerializeField]
    float attackRange;

    [SerializeField]
    float attackDelayTime;
    [SerializeField]
    float attackIntervalTime;
    [SerializeField]
    EnemyProjectorData projectorData;

    [SerializeField]
    float minHeight;

    Vector3 origin;
    Vector3 direction;

    bool interval;
    bool inDelay;
    bool engaged;

    RaycastHit2D groundHit;
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
        RaycastHit2D hit2D = Physics2D.Raycast(origin, direction, engageRange);
        if (engaged) { Debug.DrawRay(origin, direction * disengageRange, Color.blue); }
        else { Debug.DrawRay(origin, direction * engageRange, Color.yellow); }       
        Debug.DrawRay(origin, direction * attackRange, Color.red);

        groundHit = Physics2D.Raycast(origin, Vector2.down, minHeight);
        Debug.DrawRay(origin, Vector2.down * minHeight, Color.gray);

        if (hit2D.CheckRaycastHit("Player"))
        {
            if (!engaged)//’Ç”öŠJŽn
            {
                engaged = true;
            }
            if (!interval && GetPlayerDistance() <= attackRange)//UŒ‚
            {
                interval = true;
                inDelay = true;
                StartCoroutine(Attack());
            }
        }
        if (engaged&& GetPlayerDistance() > disengageRange) { engaged = false; }//’Ç”öI—¹

        SetSpriteFlip();
    }
    private void FixedUpdate()
    {
        if (engaged&&!inDelay&& GetPlayerDistance() > attackRange)
        {
            transform.Translate(GetPlayerDir() * enemyStatus.moveSpeed * 0.01f);
        }

        if (groundHit.CheckRaycastHit("Ground")) { transform.Translate(Vector3.up * enemyStatus.moveSpeed * 0.01f); }
        //rb.velocity = Vector2.zero;
    }

    IEnumerator Attack()
    {
        Signal();
        yield return new WaitForSeconds(attackDelayTime);
        inDelay = false;
        StartFireProjectile(projectorData, new Vector3());
        yield return new WaitForSeconds(attackIntervalTime);
        interval = false;
    }

}
