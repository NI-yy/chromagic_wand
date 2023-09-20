using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slime : Enemy
{
    [SerializeField]
    float jumpIntervalTime;
    [SerializeField]
    float jumpHeight;
    [SerializeField,Header("0-90")]
    float jumpAngel;

    [SerializeField]
    bool doesAttack;
    [SerializeField]
    int attackInterval;
    [SerializeField]
    float attackDelayTime;
    [SerializeField]
    float attackHeight;
    [SerializeField, Header("0-90")]
    float attackAngel;
    [SerializeField]
    EnemyProjectorData projectorData;

    bool isOnGround;
    bool interval = true;
    bool jumped;
    bool jumping;
    bool attack;
    int jumpCount;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        StartCoroutine(JumpInterval());
    }
    private void FixedUpdate()
    {
        if (jumping)
        {
            int i = 1;
            if (GetPlayerDir().x < 0) { i = -1; }
            if (attack) { rb.AddForce(new Vector2(Mathf.Cos(attackAngel * Mathf.Deg2Rad) * i, 0) * attackHeight, ForceMode2D.Force); }
            else { rb.AddForce(new Vector2(Mathf.Cos(jumpAngel * Mathf.Deg2Rad) * i, 0) * jumpHeight, ForceMode2D.Force); }
        }
    }
    // Update is called once per frame
    void Update()
    {
        isOnGround = groundCheck.IsOnGround();

        if (!interval && isOnGround)
        {
            interval = true;
            jumpCount++;
            if (doesAttack && jumpCount == attackInterval)
            {
                jumpCount = 0;
                attack = true;
                StartCoroutine(Attack());
            }
            else { StartCoroutine(Jump()); }
        }
        if (jumped && isOnGround)//’…’n
        {
            jumping = false;
            jumped = false;
            if (attack)
            {
                attack = false;
                anim.SetBool("Attack", false);
                StartFireProjectile(projectorData, new Vector3(0, 1));
            }
            else
            {
                anim.SetBool("Jump",false);
            }
            StartCoroutine(JumpInterval());
        }

        SetSpriteFlip();
    }
   
    IEnumerator Jump()
    {
        //int i = 1;
        //if (GetPlayerDir().x < 0) { i = -1; }
        //rb.AddForce(new Vector2(Mathf.Cos(jumpAngel * Mathf.Deg2Rad) * i, Mathf.Sin(jumpAngel * Mathf.Deg2Rad)) * jumpHeight, ForceMode2D.Impulse);
        rb.AddForce(new Vector2(0, Mathf.Sin(jumpAngel * Mathf.Deg2Rad)) * jumpHeight, ForceMode2D.Impulse);
        anim.SetBool("Jump", true);
        jumping = true;
        yield return new WaitForSeconds(0.2f);
        jumped = true;
    }
    IEnumerator JumpInterval()
    {
        yield return new WaitForSeconds(jumpIntervalTime);
        interval = false;
    }

    IEnumerator Attack()
    {
        Signal();
        yield return new WaitForSeconds(attackDelayTime);
        //int i = 1;
        //if (GetPlayerDir().x < 0) { i = -1; }
        //rb.AddForce(new Vector2(Mathf.Cos(attackAngel * Mathf.Deg2Rad) * i, Mathf.Sin(attackAngel * Mathf.Deg2Rad)) * attackHeight, ForceMode2D.Impulse);
        rb.AddForce(new Vector2(0, Mathf.Sin(attackAngel * Mathf.Deg2Rad)) * attackHeight, ForceMode2D.Impulse);
        anim.SetBool("Attack", true);
        jumping = true;
        yield return new WaitForSeconds(0.2f);
        jumped = true;
    }
}
