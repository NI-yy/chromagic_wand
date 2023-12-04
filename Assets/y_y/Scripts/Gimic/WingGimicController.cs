using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingGimicController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float initialForce;

    private void Start()
    {
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * initialForce, ForceMode2D.Impulse);
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
