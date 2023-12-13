using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CirclePainter : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Camera subCamera;

    [SerializeField]
    CinemachineVirtualCamera virtualCamera;

    [Header("サークル表示位置のオフセット")]
    public Vector2 circleOffSet = new Vector2(0, 0);

    public float scale;
    public float cameraScale;
    private Vector3 originePos;

    private void Start() {
        originePos = transform.localPosition;
    }

    void Update() {
        cameraScale = virtualCamera.m_Lens.OrthographicSize;
        subCamera.orthographicSize = cameraScale / 20 * scale;
        transform.localScale = new Vector3(scale, scale, scale);

        Transform playerPosition = player.transform;
        transform.localPosition = playerPosition.localPosition + new Vector3(circleOffSet.x, circleOffSet.y, 0);
        //subCamera.transform.localPosition = playerPosition.localPosition + new Vector3(circleOffSet.x * cameraScale / 20, circleOffSet.y * cameraScale / 20, -20f);
    }
}
