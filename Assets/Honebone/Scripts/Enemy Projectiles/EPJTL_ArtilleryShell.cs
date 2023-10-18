using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPJTL_ArtilleryShell : EnemyProjectile
{
    [SerializeField]
    EnemyProjectorData projectorData;
    [SerializeField]
    GameObject projector;
    public override void AtTheEnd(bool expired)
    {
        var p = Instantiate(projector, transform.position, Quaternion.identity);
        p.GetComponent<EnemyProjector>().Init(projectorData, new Vector3(), playerPos, player);
    }
}
