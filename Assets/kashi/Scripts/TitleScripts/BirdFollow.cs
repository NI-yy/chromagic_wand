using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BirdFollow : MonoBehaviour
{
    [SerializeField]
    KashiButtonManager kashiButtonManager_;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var worldPos = Camera.main.ScreenToWorldPoint(kashiButtonManager_.GetSelectedButton().transform.position);
        transform.DOMove(worldPos + new Vector3(-3f, 0f, 10f), 0.1f);
    }
}