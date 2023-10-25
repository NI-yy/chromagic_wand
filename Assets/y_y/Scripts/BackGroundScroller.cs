using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    [SerializeField]
    GameObject Player;

    Vector3 pos_pre;
    Vector3 pos_current;
    Vector3 pos_diff;
    Vector3 newBackGroundPos = new Vector3(58f, 44f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        pos_pre = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pos_current= Player.transform.position;
        pos_diff = pos_current - pos_pre;
        newBackGroundPos = pos_current + new Vector3(42f, 0f, 0f);
        Vector3 pos = this.transform.position;
        pos = newBackGroundPos;
        this.transform.position = pos;
        pos_pre = pos_current;
    }
}
