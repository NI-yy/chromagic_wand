using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    EnemyProjectorData projectorData;
    Transform tf;
    Player player;
    Transform playerTF;
    Vector3 playerPos;
    Vector3 playerPosDiff;
    float projectileSpeed;
    float followPlayerSpeed;

    bool disabled;
    public void Init(EnemyProjectorData data, Player p)
    {
        projectorData = data;
        player = p;
        playerTF=player.GetComponent<Transform>();
        playerPos = playerTF.position;

        projectileSpeed = Random.Range(projectorData.projectileSpeed_min, projectorData.projectileSpeed_max);
        followPlayerSpeed=data.followPlayerSpeed;

        tf = transform;


        StartCoroutine(CountDown());
    }
    void FixedUpdate()
    {
        if (followPlayerSpeed > 0)//追尾弾
        {
            if (projectorData.followCurrentPlayer)//現在のプレイヤーの位置を追尾する場合は、プレイヤーの位置を常に更新
            {
                playerPos = playerTF.position;
            }
            playerPosDiff = (playerPos - tf.position);
            Vector2 dis = playerPosDiff;
            if (dis.magnitude < 0.5f ) { followPlayerSpeed = 0; }//プレイヤーの位置に到着したら追尾停止

            float rot = (Mathf.Atan2(playerPosDiff.y, playerPosDiff.x) * Mathf.Rad2Deg) - tf.localEulerAngles.z - 90;
            if (rot < -180) { rot += 360; }
            tf.Rotate(0, 0, Mathf.Clamp(rot, followPlayerSpeed * -0.5f, followPlayerSpeed * 0.5f));
        }

        tf.Translate(Vector3.up * projectileSpeed / 33f);
    }

    void DestroyPJTL(bool expired)
    {
        disabled = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        AtTheEnd(expired);


        Destroy(gameObject, 1f);
    }
    /// <summary>expired:時間切れによる破壊か</summary>
    public virtual void AtTheEnd(bool expired) { }//消滅時誘発

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(projectorData.projectileDuration);
        if (!disabled) { DestroyPJTL(true); }
    }
}
