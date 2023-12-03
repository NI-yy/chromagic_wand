using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoitanMissile : MonoBehaviour
{
    int cnt = 100;
    LineRenderer lineRenderer;
    Vector3[] poses;

    Vector3 velocity;
    Vector3 position;
    Transform target;
    float period;
    float periodStart;
    float time;
    AnimationCurve curve;

    bool isDead = false;
    float lineliveTime;
    int stepCount = 4;
    // Start is called before the first frame update
    void Start()
    {
        cnt = (int)(lineliveTime / Time.fixedDeltaTime) * stepCount;
        TryGetComponent(out lineRenderer);
        position = transform.position;
        poses = new Vector3[cnt];
        lineRenderer.positionCount = cnt;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            poses[i] = position;
        }
        lineRenderer.SetPositions(poses);
    }

    private void FixedUpdate()
    {
        float stepTime = Time.deltaTime / stepCount;
        for (int i = 0; i < stepCount; i++)
        {
            UpdatePos(stepTime);
            UpdateTrail();
        }
        transform.position = position;


    }

    /*
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        for (int i = cnt - 1; i >= 1; i--)
        {
            poses[i] = poses[i - 1];
        }
        poses[0] = mousePos;
        lineRenderer.SetPositions(poses);
    }
    */

    public void Initialize(Transform target, float period, Vector3 velocity, AnimationCurve curve, float lineLiveTime, int stepCount)
    {
        this.target = target;
        this.period = period;
        this.velocity = velocity;
        periodStart = period;
        this.curve = curve;
        time = 0f;
        this.lineliveTime = lineLiveTime;
        this.stepCount = stepCount <= 0 ? 1 : stepCount;
    }

    void UpdatePos(float t)
    {
        if (isDead) return;
        period = periodStart - curve.Evaluate(time / periodStart) * periodStart;
        //Debug.Log($"period = {period}");
        if (period <= 0.01f)
        {
            Destroy(gameObject, 2f);
            isDead = true;
            position = target.position;
            return;
        }
        var acceleration = Vector3.zero;
        var diff = target.position - position;
        acceleration += (diff - velocity * period) * 2f / (period * period);
        time += t;

        velocity += acceleration * t;
        position += velocity * t;
    }

    void UpdateTrail()
    {
        // トレイルの更新
        for (int i = cnt - 1; i >= 1; i--)
        {
            poses[i] = poses[i - 1];
        }
        poses[0] = position;
        lineRenderer.SetPositions(poses);
    }

    class TestIndexar
    {
        public Vector3[] poses;

        public Vector3 this[int i]
        {
            get { return poses[i]; }
        }
    }
}
