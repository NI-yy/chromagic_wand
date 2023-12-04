using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scrtwpns.Mixbox;
using KoitanLib;
using UnityEngine.UI;
using KoitanLib;
using UnityEngine.UIElements;

public class BirdParticleColorController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 移動速度
    private Texture2D tex = null;
    [SerializeField]
    Color color;
    [SerializeField]
    Color color_2;
    [SerializeField]
    Color color_mix;
    [SerializeField] Material birdMaterial;
    [SerializeField] ParticleSystem birdParticleFire;
    [SerializeField] GameObject twoPlayerManager;
    [SerializeField] GameObject player;
    

    Camera mainCamera;
    //bool flag = true;
    float h, s, v;

    //レイヤーが対象オブジェクトよりも下じゃないと色を取ってこれない現象が起こっているのでそのための対処.
    int layerNum_init;
    int layerNum_minus = -100;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        SetBirdParticleColor(Color.white);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalKeyRaw;

        if (player.GetComponent<Player>().lookRight)
        {
            horizontalKeyRaw = KoitanInput.GetStick(StickCode.LeftStick).x;
        }
        else
        {
            horizontalKeyRaw =  - KoitanInput.GetStick(StickCode.LeftStick).x;
        }
        
        float verticalKeyRaw = KoitanInput.GetStick(StickCode.LeftStick).y;

        // オブジェクトを移動させる
        Vector3 movement = new Vector3(horizontalKeyRaw, verticalKeyRaw, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        if (Input.GetMouseButtonDown(0) || (KoitanInput.GetDown(ButtonCode.B)))
        {
            StartCoroutine(ColorPicker());
        }

    }

    IEnumerator ColorPicker()
    {
        tex = new Texture2D(1, 1, TextureFormat.RGB24, false);
        yield return new WaitForEndOfFrame();

        Vector3 pos = transform.position;
        if (player.GetComponent<Player>().lookRight)
        {
            pos = pos + new Vector3(2.5f, 2.46f, 0f);
        }
        else
        {
            pos = pos + new Vector3(-2.5f, 2.46f, 0f);
        }
        
        // ワールド座標をスクリーン座標に変換します。
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(pos);

        Vector2 pos_2d = new Vector2(screenPosition.x, screenPosition.y);
        tex.ReadPixels(new Rect(pos_2d.x, pos_2d.y, 1, 1), 0, 0); //写真撮ってtexに保存

        color = tex.GetPixel(0, 0);
        birdMaterial.color = color;

        var main = birdParticleFire.main;
        main.startColor = new ParticleSystem.MinMaxGradient(color);
        birdParticleFire.Clear();
        birdParticleFire.Play();

        twoPlayerManager.GetComponent<TwoPlayerManager>().SetBirdColor(color);
    }

    public void SetBirdParticleColor(Color color)
    {
        birdMaterial.color = color;

        var main = birdParticleFire.main;
        main.startColor = new ParticleSystem.MinMaxGradient(color);
        birdParticleFire.Clear();
        birdParticleFire.Play();
    }

    
}
