using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassFloorController : MonoBehaviour
{
    private string bulletTag = "bullet";
    private string bulletColor;
    [SerializeField] Sprite grassFloor_2;
    // Start is called before the first frame update

    SpriteRenderer renderer;
    BoxCollider2D boxcollider2D;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        boxcollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(bulletTag))
        {
            Debug.Log("Trigger");
            bulletColor = collision.gameObject.GetComponent<bulletController>().GetBulletColor();
            Debug.Log(bulletColor);
            if (bulletColor.Equals("Green"))
            {
                renderer.sprite = grassFloor_2;
                boxcollider2D.isTrigger = false;
            }
        }
    }
}
