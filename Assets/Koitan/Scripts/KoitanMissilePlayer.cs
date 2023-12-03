using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoitanMissilePlayer : MonoBehaviour
{
    [SerializeField]
    KoitanMissile missilePrefab;
    [SerializeField]
    Transform target;
    [SerializeField]
    float speed = 1f;
    [SerializeField]
    float period = 2f;
    [SerializeField]
    AnimationCurve curve;
    [SerializeField]
    float lineLiveTime = 1f;
    [SerializeField]
    int stepCount = 2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = mousePos;

        if (Input.GetMouseButton(0))
        {
            var m = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            m.Initialize(target, period, Random.onUnitSphere * speed, curve, lineLiveTime, stepCount);
        }
    }
}
