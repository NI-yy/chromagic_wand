using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsController : MonoBehaviour
{
    [SerializeField] GameObject burnEffect;
    [SerializeField] GameObject crashEffect;
    [SerializeField] GameObject ThornImage;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("OrangeAttack"))
        {
            burnEffect.SetActive(true);
            StartCoroutine(ActivateSecondObjectAfterDelay(1f));
        }
    }

    IEnumerator ActivateSecondObjectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        burnEffect.SetActive(false);
        crashEffect.SetActive(true);
        ThornImage.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
