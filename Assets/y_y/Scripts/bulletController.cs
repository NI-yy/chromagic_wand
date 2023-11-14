using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 10f;
    private string color;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    public void SetBulletColor(string wand_color)
    {
        color = wand_color;
    }

    public string GetBulletColor()
    {
        return color;
    }
}
