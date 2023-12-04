using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentStageAreaManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] CinemachineVirtualCamera[] VirtualCameras;

    private Vector2 playerPos;
    private int layerMask;
    private int currentStageAreaNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = new Vector2(player.transform.localPosition.x, player.transform.localPosition.y);

        layerMask = LayerMask.GetMask("Ignore Raycast");
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = new Vector2(player.transform.localPosition.x, player.transform.localPosition.y);
        //Debug.Log(playerPos, this.gameObject);

        // 指定されたポイントにCollider2Dがあるかチェック（レイヤーマスクを使用）
        Collider2D hitCollider = Physics2D.OverlapPoint(playerPos, layerMask);

        if (hitCollider != null)
        {
            Debug.Log("Hit " + hitCollider.gameObject.name);
            if (hitCollider.gameObject.CompareTag("StageArea"))
            {
                int tempStageAreaNum = hitCollider.gameObject.GetComponent<StageAreaNum>().stageAreaNum;
                if (tempStageAreaNum != currentStageAreaNum)
                {
                    //if the player are in othe StageArea, virtualCamera's priority will be changed.
                    VirtualCameras[currentStageAreaNum].Priority = 0;
                    VirtualCameras[tempStageAreaNum].Priority = 1;

                    currentStageAreaNum = tempStageAreaNum;
                }
            }
        }
        else
        {
            Debug.Log("Null", this.gameObject);
        }
        
    }
}
