using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsController : MonoBehaviour
{
    private string bulletTag = "bullet";
    private string bulletColor;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(bulletTag))
        {
            bulletColor = collision.gameObject.GetComponent<bulletController>().GetBulletColor();
            Debug.Log(bulletColor);
            if (bulletColor.Equals("Red"))
            {
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }

            Destroy(collision.gameObject);
        }
    }
}
