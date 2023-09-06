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
    void Start()
    {
        color1 = new Color(0.0f, 0.129f, 0.522f); // blue
        color2 = new Color(0.988f, 0.827f, 0.0f); // yellow
        t = 0.5f;                                 // mixing ratio

        color_mix = Mixbox.Lerp(color1, color2, t);

        Debug.Log(color_mix);
    }

    private void Update()
    {
        color_mix = Mixbox.Lerp(color1, color2, t);
    }
}
