using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCamera : MonoBehaviour
{

    [SerializeField] Transform player;

    void Update()
    {  
        transform.localPosition = player.localPosition + new Vector3(0, 0, -20f);
    }
}
