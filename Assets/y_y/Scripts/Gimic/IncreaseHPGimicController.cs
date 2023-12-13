using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHPGimicController : MonoBehaviour
{
    [SerializeField] GameObject HP;
    [SerializeField] int increaseNum;
    [SerializeField] GameObject soundManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            HP.GetComponent<PlayerHP>().IncreaseHP(increaseNum);
            soundManager.GetComponent<SoundManager>().PlayItemGetSe();
            Destroy(this.gameObject);
        }
    }
}
