using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SpitPlant : Enemy
{
    [SerializeField]
    float range = 10;//検知範囲
    [SerializeField]
    float attackDelayTime;
    [SerializeField]
    float attackIntervalTime;
    [SerializeField]
    EnemyProjectorData projectorData;
    Vector3 origin;
    Vector3 direction;

    bool interval;
    void Start()
    {
        Init();
        origin = transform.position;
        direction = GetPlayerDir();
    }

    void Update()
    {
        origin = transform.position;//originに自身の座標を代入
        direction = GetPlayerDir();//directionに自身からプレイヤーに向かう単位ベクトルを代入
        RaycastHit2D hit2D = Physics2D.Raycast(origin, direction, range);//Raycastして検知したObjectをhit2Dに代入
        Debug.DrawRay(origin, direction * range, Color.red);//Rayと同じ始点、方向、長さの赤い線を1フレーム描画

        //攻撃後一定時間が経っている かつ Raycstでタグが"Player"であるObjectを検知した
        if (!interval && hit2D.CheckRaycastHit("Player"))
        {
            interval = true;
            StartCoroutine(Attack());
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
