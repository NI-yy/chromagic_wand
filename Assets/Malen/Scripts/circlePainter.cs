using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePainter : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Camera subCamera;

    [SerializeField]
    public float scale;
    
    void Update()
    {
        Transform playerPosition = player.transform;
        transform.position = playerPosition.position;

        subCamera.orthographicSize = scale;
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
