using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackWoodScript : MonoBehaviour
{
    [SerializeField] GameObject WallImage;
    [SerializeField] GameObject CrackParticle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("OrangeAttack"))
        {
            WallImage.SetActive(false);
            CrackParticle.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
