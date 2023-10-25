using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskManager : MonoBehaviour
{
    [SerializeField] bool isRedReleased;
    [SerializeField] bool isGreenReleased;
    [SerializeField] bool isBlueReleased;
    [SerializeField] bool isOrangeReleased;
    [SerializeField] bool isYellowReleased;
    [SerializeField] bool isPurpleReleased;

    [SerializeField] GameObject coloredImage;
    [SerializeField] Texture coloredTexture;

    [SerializeField] Shader grayScaleShader;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void SetColorTileActive()
    {
        
    }
}
