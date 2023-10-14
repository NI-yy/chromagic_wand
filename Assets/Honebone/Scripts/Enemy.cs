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
        [Header("UŒ‚—\’›‚ÌˆÊ’u")] public Vector2 attackSignalOffset=new Vector2(0,1);
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



    /// <summary>dir‚ÍƒvƒŒƒCƒ„[‚ğ‘ÎÛ‚Æ‚µ‚È‚¢‚Ì‚İg—p</summary>
    public void StartFireProjectile(EnemyProjectorData data, Vector3 dir)
    {
        StartCoroutine(Fire(data,dir));
    }
    IEnumerator Fire(EnemyProjectorData data, Vector3 dir)
    {
        var wait = new WaitForSeconds(data.fireRate);
        //if (status.SE_GengeratePjtor != null) { soundManager.PlayAudio(status.SE_GengeratePjtor); }
        //if (data.fireRate == 0 && status.SE_Fire != null) { soundManager.PlayAudio(status.SE_Fire); }
        if(data.targetPlayer) { dir = (PlayerTF.position - transform.position).normalized; }   

        for (int i = 0; i < data.fireRounds; i++)
        {
            if (data.targetPlayer && data.refreshPlayerPos) { dir = (PlayerTF.position - transform.position).normalized; }
            FireProjectile(data,dir);
            //if (status.fireRate != 0 && status.SE_Fire != null) { soundManager.PlayAudio(status.SE_Fire); }
            if (data.fireRate > 0) { yield return wait; }
        }
    }
    public void FireProjectile(EnemyProjectorData data,Vector3 dir)
    {
        Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, dir);
        float delta = data.spread / -2f; ;
        for (int i = 0; i < data.pellets; i++)
        {
            float spread = 0f;
            if (data.spread > 0 && !data.equidistant) { spread = Random.Range(data.spread / -2f, data.spread / 2f); }//ŠgU‚ÌŒˆ’è
            if (data.equidistant)//“™ŠÔŠu‚É”­Ë‚·‚é‚È‚ç
            {
                spread = delta;
                delta += data.spread / (data.pellets - 1);
            }
            if (data.fireRandomly) { spread = Random.Range(-180f, 180f); }//ƒ‰ƒ“ƒ_ƒ€‚É”ò‚Î‚·‚È‚ç

            var pjtl = Instantiate(data.projectile, transform.position, quaternion);//pjtl‚Ì¶¬
            pjtl.GetComponent<EnemyProjectile>().Init(data, player);
            pjtl.transform.Rotate(new Vector3(0, 0, 1), spread);//ŠgU•ª‰ñ“]‚³‚¹‚é
        } 
        

    }


    public Vector3 GetPlayerPos() { return PlayerTF.position; }
    public Vector3 GetPlayerDir() { return (PlayerTF.position - transform.position).normalized; }
    public Vector2 GetPlayerDir_Horizontal() { return new Vector2(GetPlayerDir().x, 0).normalized; }
    public float GetPlayerDistance() { return (PlayerTF.position - transform.position).magnitude; }
}
