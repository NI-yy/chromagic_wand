using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCamera : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] CirclePainter circlePainter;

    public float scale;
    public float cameraScale;

    private Vector2 circleOffSet;
    private CinemachineVirtualCamera virtualCamera;

    private void Start() {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {

        circleOffSet = circlePainter.circleOffSet;
        transform.localPosition = player.localPosition + new Vector3(circleOffSet.x, circleOffSet.y, -20f);
    }
}
