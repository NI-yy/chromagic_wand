using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public GameObject bird_player;
    public GameObject twoPlayerManager;
    string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            bird_player.SetActive(true);
            twoPlayerManager.GetComponent<TwoPlayerManager>().Birdenabled = true;
            Destroy(this.gameObject);
        }
    }
}
