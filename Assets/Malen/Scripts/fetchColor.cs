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
    [Header("最大半径")]
    [SerializeField] float radiusMax;
    [Header("画面全体を覆う半径")]
    [SerializeField] float radiusCoverScreenAll;
    [Header("拡縮にかかる時間(s)")]
    [SerializeField] float duration;
    //[SerializeField] PostProcessLayer PPL;
    [SerializeField] PostProcessVolume PPV;
    [SerializeField] CinemachineVirtualCamera cinemachine;
    [SerializeField] SubCamera subCamera;
    [SerializeField] GameObject soundManager;

    

    private void Start()
    {
        painter = GetComponent<CirclePainter>();
    }

    // Update is called once per frame
    void Update()
    {
        //R�{�^���ŐF�z�����[�h�ɂȂ�悤�ɕύX(yy)
        if (Input.GetMouseButtonDown(1) || KoitanInput.GetDown(ButtonCode.RB))
        {
            if (painter.scale == 0f)
                painter.scale = radiusCoverScreenAll;

            ChangeAnimation(true);
            // PPL.enabled = true;
            // by kashi
            PPV.enabled = true;

            //add sound by yy
            soundManager.GetComponent<SoundManager>().PlaypaletteSe();
        }
        if (Input.GetMouseButtonUp(1) || KoitanInput.GetUp(ButtonCode.RB))
        {
            ChangeAnimation(false);
            Time.timeScale = 1f;
        }
        subCamera.scale = painter.scale;
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
