using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    // プレハブの参照のための変数
    public GameObject prefab;
    [SerializeField] int maxHP;

    void Start()
    {
        for (int i = 0; i < maxHP; i++)
        {
            // プレハブをインスタンス化
            GameObject instance = Instantiate(prefab, transform.position, Quaternion.identity);
            // インスタンス化したオブジェクトをこのオブジェクトの子として設定
            instance.transform.SetParent(transform);
        }
    }
}
