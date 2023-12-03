using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMover : MonoBehaviour
{
    [SerializeField]
    Transform cameraTransform;
    [SerializeField]
    Vector2 offset;
    [SerializeField]
    float followRatio = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    private void LateUpdate()
    {
        UpdatePosition();
    }

    private void OnValidate()
    {
        UpdatePosition();
    }

    private void OnGUI()
    {
        UpdatePosition();
    }

    private void OnDrawGizmos()
    {
        UpdatePosition();

    }

    void UpdatePosition()
    {
        if (cameraTransform == null)
        {
            return;
        }
        Vector3 pos = transform.position;
        pos.x = offset.x;
        pos.y = offset.y;
        Vector3 movePos = cameraTransform.position * followRatio;
        movePos.z = 0f;
        transform.position = pos + movePos;
    }
}
