using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CirclePainter : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Camera subCamera;

    [HideInInspector]
    public float scale;
    
    void Update()
    {
        Transform playerPosition = player.transform;
        transform.position = playerPosition.position;

        subCamera.orthographicSize = scale;
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
