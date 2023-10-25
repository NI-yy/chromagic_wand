using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spider : Enemy
{
    LineRenderer line;
    [SerializeField]
    float ceilingHitRange;
    [SerializeField]
    float moveTime_min;
    [SerializeField]
    float moveTime_max;

    [SerializeField]
    float groundHitRange;

    [SerializeField]
    int attackInterval;
    [SerializeField]
    float attackRange;
    [SerializeField]
    float searchDuration;

    [SerializeField]
    float attackDelayTime;
    [SerializeField]
    float attackIntervalTime;
    [SerializeField]
    EnemyProjectorData projectorData;

    RaycastHit2D groundHit;
    RaycastHit2D ceilingHit;
    RaycastHit2D playerHit;

    Vector3 origin;

    Vector3 ceilingPos;
    Vector3 moveVector;

    bool moveDelay;
    bool searchPlayer;
    bool attacking;

    int moveCount;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        line = GetComponent<LineRenderer>();

        if (50.Dice()) { moveVector = Vector3.up; }
        else { moveVector = Vector3.down; }

        StartCoroutine("Move");
    }
    private void FixedUpdate()
    {
        if (!attacking&&!moveDelay)
        {
            transform.Translate(moveVector * enemyStatus.moveSpeed * 0.01f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        origin = transform.position;


        ceilingHit = Physics2D.Raycast(transform.position, Vector2.up, ceilingHitRange);
        if (ceilingHit.CheckRaycastHit("Ground"))
        {
            ceilingPos = ceilingHit.point;
        }

        groundHit = Physics2D.Raycast(origin, moveVector, groundHitRange);
        //Debug.DrawRay(origin, moveVector * groundHitRange, Color.red);
        if (groundHit.CheckRaycastHit("Ground")&&!moveDelay)
        {
            StopCoroutine("Move");
            moveDelay = true;
            EndMove();
        }

        playerHit = Physics2D.Raycast(origin, GetPlayerDir(), attackRange);
        Debug.DrawRay(origin, GetPlayerDir() * attackRange, Color.red);
        if (playerHit.CheckRaycastHit("Player") && searchPlayer)
        {
            searchPlayer = false;
            StopCoroutine("SearchPlayer");
            StartCoroutine(Attack());
        }

        line.SetPosition(0, transform.position);//ü•`‰æ
        line.SetPosition(1, ceilingPos);

        SetSpriteFlip();
    }

    void EndMove()
    {
        moveCount++;
        if (moveVector == Vector3.up) { moveVector = Vector3.down; }
        else { moveVector = Vector3.up; }
        if (moveCount >= attackInterval)
        {
            searchPlayer = true;
            moveCount = 0;
            StartCoroutine("SearchPlayer");
        }
        else
        {
            StartCoroutine("Move");
        }
    }

    IEnumerator Move()
    {
        //yield return new WaitForSeconds(0.1f);
        moveDelay = false;
        yield return new WaitForSeconds(Random.Range(moveTime_min, moveTime_max));
        moveDelay = true;
        EndMove();
    }
    IEnumerator SearchPlayer()
    {
        yield return new WaitForSeconds(searchDuration);
        searchPlayer = false;
        StartCoroutine("Move");
    }
    IEnumerator Attack()
    {
        Signal();
        yield return new WaitForSeconds(attackDelayTime);
        StartFireProjectile(projectorData, new Vector3());
        yield return new WaitForSeconds(attackIntervalTime);
        StartCoroutine("Move");
    }
}
