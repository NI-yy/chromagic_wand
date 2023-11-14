using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scrtwpns.Mixbox;
using KoitanLib;
using UnityEngine.UI;
using KoitanLib;

public class BirdColorController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 移動速度
    private Texture2D tex = null;
    [SerializeField]
    Color color;
    [SerializeField]
    Color color_2;
    [SerializeField]
    Color color_mix;
    
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
        layerNum_init = this.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        Debug.Log(layerNum_init);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalKeyRaw = KoitanInput.GetStick(StickCode.LeftStick).x;
        float verticalKeyRaw = KoitanInput.GetStick(StickCode.LeftStick).y;

        // オブジェクトを移動させる
        Vector3 movement = new Vector3(horizontalKeyRaw, verticalKeyRaw, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        if (Input.GetMouseButtonDown(0) || (KoitanInput.GetDown(ButtonCode.B)))
        {
            //レイヤーを一番にしないとなぜか色を取ってこれない
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -100;
            Debug.Log(this.gameObject.GetComponent<SpriteRenderer>().sortingOrder);
            StartCoroutine(ColorPicker());
            
        }

        //if (Input.GetMouseButtonDown(1))
        //{
        //    color_mix = Mixbox.Lerp(color, color_2, 0.5f);
        //    Debug.Log(color_mix);
        //    Color.RGBToHSV(color_mix, out h, out s, out v);
        //    Debug.Log((h, s, v));
        //    SpriteRenderer spriterenderer = GetComponent<SpriteRenderer>();
        //    spriterenderer.color = color_mix;
        //    UI_ColorOrb.GetComponent<Image>().color = color_mix;
        //}
    }

    IEnumerator ColorPicker()
    {
        tex = new Texture2D(1, 1, TextureFormat.RGB24, false);
        yield return new WaitForEndOfFrame();

        Vector3 pos = transform.position;
        pos = pos + new Vector3(0.51f, 0.25f, 0f);
        // ワールド座標をスクリーン座標に変換します。
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(pos);

        Vector2 pos_2d = new Vector2(screenPosition.x, screenPosition.y);
        tex.ReadPixels(new Rect(pos_2d.x, pos_2d.y, 1, 1), 0, 0); //写真撮ってtexに保存

        //if (flag)
        //{
        //    color = tex.GetPixel(0, 0);
        //    flag = !flag;
        //}
        //else
        //{
        //    color_2 = tex.GetPixel(0, 0);
        //    flag = !flag;
        //}
        //Debug.Log((pos_2d.x + 0.51f, pos_2d.y + 0.25f));

        color = tex.GetPixel(0, 0);
        SpriteRenderer spriterenderer = GetComponent<SpriteRenderer>();
        spriterenderer.color = color;
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = layerNum_init;
    }
}
