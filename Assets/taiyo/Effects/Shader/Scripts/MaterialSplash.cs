using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSplash : MonoBehaviour
{

    private Material _mat;
    private float bornTime;
    private float duration;
    float timeFromBorn;

    // Start is called before the first frame update
    void Start()
    {
        _mat = GetComponent<Renderer>().material;
        bornTime = Time.time;
        duration = GetComponent<ParticleSystem>().duration;
        timeFromBorn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeFromBorn += Time.deltaTime;

        if(timeFromBorn > duration)
        {
            timeFromBorn = 0f;
        }
        _mat.SetFloat("_TimeFromBorn", timeFromBorn);
    }
}
