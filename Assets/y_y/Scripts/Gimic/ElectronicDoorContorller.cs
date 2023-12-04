using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicDoorContorller : MonoBehaviour
{
    public float maxY = 0f;
    public float speed;

    private bool flag = false;
    private string bulletTag = "bullet";
    private string bulletColor;

    [SerializeField] GameObject openEffect;

    private void Update()
    {
        if (flag)
        {
            Transform objectTransform = gameObject.GetComponent<Transform>(); // ゲームオブジェクトのTransformコンポーネントを取得
            Vector3 targetPosition = objectTransform.localPosition + new Vector3(0f, maxY, 0f); // 目的の位置の座標を指定

            objectTransform.localPosition = Vector3.Lerp(objectTransform.localPosition, targetPosition, speed * Time.deltaTime); // 目的の位置に移動
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag(bulletTag))
        //{
        //    Debug.Log("Trigger");
        //    bulletColor = collision.gameObject.GetComponent<bulletController>().GetBulletColor();
        //    Debug.Log(bulletColor);
        //    if (bulletColor.Equals("Yellow"))
        //    {
        //        flag = true;
        //    }

        //    Destroy(collision.gameObject);
        //}

        if (collision.gameObject.CompareTag("ElectricAttack"))
        {
            flag = true;
            openEffect.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(collision.gameObject);
        }
    }
}
