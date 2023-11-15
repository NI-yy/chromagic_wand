using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected GameObject attackSignal;
    public GameObject projector;



    [System.Serializable]
    public class EnemyStatus
    {
        public int maxHP = 1;
        public float moveSpeed;
        [Header("攻撃予兆の位置")] public Vector2 attackSignalOffset=new Vector2(0,3);
    }
    [SerializeField]
    protected EnemyStatus enemyStatus;

    protected Animator anim;
    protected SpriteRenderer sprite;
    protected Rigidbody2D rb;

    protected Player player;
    protected Transform PlayerTF;

    [SerializeField]
    protected GroundCheck groundCheck;

    // Start is called before the first frame update
    public void Init()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        player = FindObjectOfType<Player>();
        PlayerTF = player.GetComponent<Transform>();

    }

    public void HitAttack()
    {
        Destroy(this.gameObject);//test
    }


    public void Signal()
    {
        Vector3 signalPos = transform.position + enemyStatus.attackSignalOffset.ToVector3();
        Instantiate(attackSignal,signalPos,Quaternion.identity);
    }
    public void SetSpriteFlip()
    {
        if (sprite.flipX && GetPlayerDir().x < 0) { sprite.flipX = false; }
        if (!sprite.flipX && GetPlayerDir().x > 0) { sprite.flipX = true; }
    }



    /// <summary>dirはプレイヤーを対象としない時のみ使用</summary>
    public void StartFireProjectile(EnemyProjectorData data, Vector3 dir)
    {
        var p = Instantiate(projector, transform.position, Quaternion.identity);
        p.GetComponent<EnemyProjector>().Init(data, dir, GetPlayerPos(), player);
    }



    public Vector3 GetPlayerPos()
    {
        Vector3 pos = PlayerTF.position;
        pos.y += 7.5f;//応急処置
        return pos;
    }
    public Vector3 GetPlayerDir() { return (GetPlayerPos() - transform.position).normalized; }
    public Vector2 GetPlayerDir_Horizontal() { return new Vector2(GetPlayerDir().x, 0).normalized; }
    public float GetPlayerDistance() { return (GetPlayerPos() - transform.position).magnitude; }
}
