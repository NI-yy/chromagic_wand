using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;
using KoitanLib;

public class FetchColor : MonoBehaviour
{

    private bool fetchFlag = false;
    private float size;
    private Tween circleAnimation;
    private CirclePainter painter;
    [Header("色吸いモードの最大半径")]
    [SerializeField] float radiusMax;
    [Header("メインカメラ全体を覆う半径")]
    [SerializeField] float radiusCoverScreenAll;
    [Header("色吸いモード移行にかかる時間(s)")]
    [SerializeField] float duration;
    //[SerializeField] PostProcessLayer PPL;
    [SerializeField] PostProcessVolume PPV;
    [SerializeField] CinemachineVirtualCamera cinemachine;

    private void Start()
    {
        painter = GetComponent<CirclePainter>();
        size = 2 * cinemachine.m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        //Rボタンで色吸いモードになるように変更(yy)
        if (Input.GetMouseButtonDown(0) || KoitanInput.GetDown(ButtonCode.RB))
        {
            if (painter.scale == 0f)
                painter.scale = radiusCoverScreenAll;
            ChangeAnimation(true);
            // PPL.enabled = true;
            // by kashi
            PPV.enabled = true;
        }
        if (Input.GetMouseButtonUp(0) || KoitanInput.GetUp(ButtonCode.RB))
        {
            ChangeAnimation(false);
            Time.timeScale = 1f;
        }
    }

    void ChangeAnimation(bool isKeyPushed)
    {
        circleAnimation.Kill();
        Debug.Log(circleAnimation);
        if (isKeyPushed)
        {
            circleAnimation = DOTween.To(() => painter.scale, x => painter.scale = x, radiusMax, duration);
            Time.timeScale = 0.1f;
        }
        else{
            circleAnimation = DOTween.To(() => painter.scale, x => painter.scale = x, radiusCoverScreenAll, duration * 10).OnComplete(OnCompleteExtension);            
        }
    }

    void OnCompleteExtension()
    {
        painter.scale = 0f;
        // PPL.enabled = false;
        // by kashi
        PPV.enabled = false;
    }
}
