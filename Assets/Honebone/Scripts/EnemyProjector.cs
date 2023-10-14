using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjector : MonoBehaviour
{
    Vector3 playerPosition;
    Player player;

    public void Init(EnemyProjectorData data, Vector3 dir,Vector3 playerPos,Player p)
    {
        playerPosition = playerPos;
        player = p;
        StartCoroutine(Fire(data, dir));
    }
    IEnumerator Fire(EnemyProjectorData data, Vector3 dir)
    {
        var wait = new WaitForSeconds(data.fireRate);
        //if (status.SE_GengeratePjtor != null) { soundManager.PlayAudio(status.SE_GengeratePjtor); }
        //if (data.fireRate == 0 && status.SE_Fire != null) { soundManager.PlayAudio(status.SE_Fire); }
        if (data.targetPlayer) { dir = (playerPosition - transform.position).normalized; }

        for (int i = 0; i < data.fireRounds; i++)
        {
            if (data.targetPlayer && data.refreshPlayerPos) { dir = (playerPosition - transform.position).normalized; }
            FireProjectile(data, dir);
            //if (status.fireRate != 0 && status.SE_Fire != null) { soundManager.PlayAudio(status.SE_Fire); }
            if (data.fireRate > 0) { yield return wait; }
        }
    }
    public void FireProjectile(EnemyProjectorData data, Vector3 dir)
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

        Destroy(gameObject);
    }
}
