using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Wolf : Enemy
{ 

    [SerializeField]
    float engageRange;
    [SerializeField]
    float disengageRange;
    [SerializeField]
    float attackRange;

    [SerializeField]
    float jumpRange;
    [SerializeField]
    float jumpHeight;
    [SerializeField, Header("0-90")]
    float jumpAngel;

    [SerializeField]
    float attackIntervalTime;
    [SerializeField]
    float attackDelayTime;
    [SerializeField]
    float attackHeight;
    [SerializeField, Header("0-90")]
    float attackAngel;

    Vector3 origin;
    Vector3 direction;

    bool isOnGround;
    bool interval;
    /// <summary>0:ジャンプしてない 1:ジャンプ開始 2:着地判定開始</summary>
    int jumpState;
    /// <summary>0:攻撃していない 1:攻撃動作 2:攻撃開始 3:着地判定開始</summary>
    int attackState;
    bool engaged;

    RaycastHit2D groundHit;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    private void FixedUpdate()
    {
        if (engaged && attackState == 0 && jumpState == 0 && isOnGround)//移動
        {
            transform.Translate(GetPlayerDir_Horizontal() * enemyStatus.moveSpeed * 0.01f);
        }

        if (jumpState>0)//ジャンプ中の横移動
        {
            int i = 1;
            if (GetPlayerDir().x < 0) { i = -1; }
            rb.AddForce(new Vector2(Mathf.Cos(jumpAngel * Mathf.Deg2Rad) * i, 0) * jumpHeight, ForceMode2D.Force);
        }

        if (groundHit.CheckRaycastHit("Ground") && isOnGround && jumpState == 0 && attackState == 0)//ジャンプ
        {
            jumpState = 1;
            StartCoroutine(Jump());
        }
    }
    // Update is called once per frame
    void Update()
    {
        isOnGround = groundCheck.IsOnGround();

        origin = transform.position;
        direction = GetPlayerDir();
        RaycastHit2D hit2D = Physics2D.Raycast(origin, direction, engageRange);
        if (engaged) { Debug.DrawRay(origin, direction * disengageRange, Color.blue); }
        else { Debug.DrawRay(origin, direction * engageRange, Color.yellow); }
        Debug.DrawRay(origin, direction * attackRange, Color.red);

        groundHit = Physics2D.Raycast(origin, GetPlayerDir_Horizontal(), jumpRange);
        Debug.DrawRay(origin, GetPlayerDir_Horizontal() * jumpRange, Color.gray);

        if (hit2D.CheckRaycastHit("Player"))
        {
            if (!engaged)//追尾開始
            {
                engaged = true;
            }
            if (!interval && GetPlayerDistance() <= attackRange && isOnGround)//攻撃
            {
                interval = true;
                attackState = 1;
                StartCoroutine(Attack());
            }
        }
        if (engaged && GetPlayerDistance() > disengageRange) { engaged = false; }//追尾終了

        if (attackState == 3 && isOnGround)//攻撃後着地
        {
            attackState = 0;
            rb.velocity = Vector2.zero;
            //anim.SetBool("Attack", false);
            StartCoroutine(AttackInterval());
        }
        if (jumpState == 2 && isOnGround)//ジャンプ後着地
        {
            jumpState = 0;
            rb.velocity = Vector2.zero;
        }

        SetSpriteFlip();
    }
   

    
    IEnumerator AttackInterval()
    {
        yield return new WaitForSeconds(attackIntervalTime);
        interval = false;
    }

    IEnumerator Attack()
    {
        Signal();
        yield return new WaitForSeconds(attackDelayTime);
        int i = 1;
        if (GetPlayerDir().x < 0) { i = -1; }
        rb.AddForce(new Vector2(Mathf.Cos(attackAngel * Mathf.Deg2Rad) * i, Mathf.Sin(attackAngel * Mathf.Deg2Rad)) * attackHeight, ForceMode2D.Impulse);
        //rb.AddForce(new Vector2(0, Mathf.Sin(attackAngel * Mathf.Deg2Rad)) * attackHeight, ForceMode2D.Impulse);
        //anim.SetBool("Attack", true);
        attackState = 2;
        yield return new WaitForSeconds(0.2f);
        attackState = 3;
    }
    IEnumerator Jump()
    {
        rb.AddForce(new Vector2(0, Mathf.Sin(jumpAngel * Mathf.Deg2Rad)) * jumpHeight, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        jumpState = 2;
    }
}
