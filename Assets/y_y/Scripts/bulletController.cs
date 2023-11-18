using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 10f;
    string color;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    public void SetBulletColor(string wand_color)
    {
        Debug.Log(("SetBulletColor wandcolor: " + wand_color, this.gameObject));
        color = wand_color;
        Debug.Log(("SetBulletColor color: " + color, this.gameObject));
    }

    public string GetBulletColor()
    {
        Debug.Log(("GetBulletColor" + color, this.gameObject));
        return color;
    }
}
