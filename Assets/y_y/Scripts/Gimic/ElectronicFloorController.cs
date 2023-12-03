using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicFloorController : MonoBehaviour
{
    public float minY = 0f;
    public float maxY = 5f;
    public float speed = 1f;

    private bool flag = false;
    private string bulletTag = "bullet";
    private string bulletColor;

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            float y = Mathf.PingPong(Time.time * speed, maxY - minY) + minY;

            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(bulletTag))
        {
            Debug.Log("Trigger");
            bulletColor = collision.gameObject.GetComponent<bulletController>().GetBulletColor();
            Debug.Log(bulletColor);
            if (bulletColor.Equals("Yellow"))
            {
                flag = true;
            }

            Destroy(collision.gameObject);
        }
    }
}
