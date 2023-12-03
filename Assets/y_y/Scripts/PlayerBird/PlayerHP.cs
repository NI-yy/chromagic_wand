using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    // プレハブの参照のための変数
    public GameObject prefab;
    [SerializeField] int maxHP;
    List<GameObject> playerHPs = new List<GameObject>();
    int currentHPCount;

    void Start()
    {
        currentHPCount = maxHP;

        for (int i = 0; i < maxHP; i++)
        {
            // プレハブをインスタンス化
            GameObject instance = Instantiate(prefab, transform.position, Quaternion.identity);
            // インスタンス化したオブジェクトをこのオブジェクトの子として設定
            instance.transform.SetParent(transform);
            playerHPs.Add(instance);
        }
    }

    public void ReduceHP()
    {
        currentHPCount--;
        if(currentHPCount < 0)
        {
            Debug.Log("GameOver");
            SceneManager.LoadScene("Title");
        }
        else
        {
            Destroy(playerHPs[currentHPCount]);
        }
    }
}
