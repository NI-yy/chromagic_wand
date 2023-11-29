using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackWoodScript : MonoBehaviour
{
    string wandTag = "wand";
    private string bulletTag = "bullet";
    private string bulletColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag(wandTag))
        //{
        //    Destroy(this.gameObject);
        //}

        if (collision.gameObject.CompareTag(bulletTag))
        {
            bulletColor = collision.gameObject.GetComponent<bulletController>().GetBulletColor();
            Debug.Log(bulletColor);
            if (bulletColor.Equals("Orange"))
            {
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }

            Destroy(collision.gameObject);
        }
    }

}
