using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingGimicController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float initialForce;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Player>().beAbleToDoubleJump = true;
            Destroy(this.gameObject);
        }
    }
}
