using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class fetchColor : MonoBehaviour
{

    private bool fetchFlag = false;
    private Tween circleAnimation;
    private circlePainter painter;
    [SerializeField] 
    private float maxRadius;
    [SerializeField] 
    private float duration;

    private void Start()
    {
        painter = GetComponent<circlePainter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            changeAnimation(true, circleAnimation);
        }
        if (Input.GetMouseButtonUp(0))
        {
            changeAnimation(false, circleAnimation);
        }
    }

    void changeAnimation(bool isKeyPushed, Tween circleAnimation)
    {
        circleAnimation.Kill();
        if (isKeyPushed)
        {
            circleAnimation = DOTween.To(() => painter.scale, x => painter.scale = x, maxRadius, duration);
            Time.timeScale = 0.1f;
        }
        else{
            circleAnimation = DOTween.To(() => painter.scale, x => painter.scale = x, 0f, duration * 10);
            Time.timeScale = 1f;
        }
    }
}
