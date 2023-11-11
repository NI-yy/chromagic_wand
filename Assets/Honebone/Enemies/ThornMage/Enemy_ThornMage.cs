using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ThornMage : Enemy
{
    [SerializeField]
    float engageRange;
    [SerializeField]
    float disengageRange;

    [SerializeField]
    float attackDelayTime;
    [SerializeField]
    float attackIntervalTime;

    [SerializeField]
    GameObject thorn;

    //[SerializeField]
    //float minHeight;

    Vector3 origin;
    Vector3 direction;

    bool interval;
    bool engaged;

    //RaycastHit2D groundHit;
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

        //groundHit = Physics2D.Raycast(origin, Vector2.down, minHeight);
        //Debug.DrawRay(origin, Vector2.down * minHeight, Color.gray);


        if (hit2D.CheckRaycastHit("Player"))
        {
            if (!engaged)//åêÌäJén
            {
                engaged = true;
            }
        }
        if (!interval && engaged)//çUåÇ
        {
            interval = true;
            StartCoroutine(Attack());
        }
        if (engaged && GetPlayerDistance() > disengageRange) { engaged = false; }//åêÌèIóπ



        SetSpriteFlip();
    }
    private void FixedUpdate()
    {
        //if (!attacking && groundHit.CheckRaycastHit("Ground")) { transform.Translate(Vector3.up * enemyStatus.moveSpeed * 0.01f); }
        rb.velocity = Vector2.zero;
    }

    IEnumerator Attack()
    {
        Vector3 attackPos = new Vector3();
        RaycastHit2D[] ground = Physics2D.RaycastAll(GetPlayerPos(), Vector2.down, 60);
        Debug.DrawRay(GetPlayerPos(), Vector2.down * 60, Color.gray, 1f);
        foreach (RaycastHit2D hit in ground)
        {
            if (hit.CheckRaycastHit("Ground"))
            {
                attackPos = hit.point;
                attackPos.y += 3;
                Instantiate(attackSignal, attackPos, Quaternion.identity);
                break;
            }
        }
        yield return new WaitForSeconds(attackDelayTime);
        var t = Instantiate(thorn, attackPos, Quaternion.identity);
        yield return new WaitForSeconds(attackIntervalTime);
        interval = false;
    }
}
