using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scrtwpns.Mixbox;

public class ColorTest : MonoBehaviour
{
    [SerializeField]
    Color color1;
    [SerializeField]
    Color color2;
    [SerializeField]
    Color color_mix;
    float t = 0.5f;
    float h;
    float s;
    float v;

    void Start()
    {
        color1 = new Color(0.0f, 0.129f, 0.522f); // blue
        color2 = new Color(0.988f, 0.827f, 0.0f); // yellow
        t = 0.5f;                                 // mixing ratio

        color_mix = Mixbox.Lerp(color1, color2, t);

        Debug.Log(color_mix);
        Color.RGBToHSV(color_mix,out h,out s,out v);
        Debug.Log((h, s, v));
    }

    private void Update()
    {
        color_mix = Mixbox.Lerp(color1, color2, t);
        //Debug.Log(color1);
        Color.RGBToHSV(color1, out h, out s, out v);
        h = Remap(h, 0, 1, 0, 360);
        s = Remap(s, 0, 1, 0, 100);
        v = Remap(v, 0, 1, 0, 100);
        Debug.Log((h, s, v));
    }

    // リマップを行う関数
    float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        // リマップを計算して返す
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }
}
