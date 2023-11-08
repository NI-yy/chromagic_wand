using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackWoodScript : MonoBehaviour
{
    string wandTag = "wand";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(wandTag))
        {
            Destroy(this.gameObject);
        }
    }
}
