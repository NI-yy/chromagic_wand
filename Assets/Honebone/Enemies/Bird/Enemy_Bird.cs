using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bird : Enemy
{
     
    [Header("x:ˆÚ“®•ûŒü 0:right 1:up 2: left 3:down\ny:ˆÚ“®ŽžŠÔ")]
    [SerializeField]
    Vector2[] moveList;

    int currentMove;
    Vector3 moveVector;

    void Start()
    {
        Init();
        StartMove();
    }
    private void FixedUpdate()
    {
        transform.Translate(moveVector * enemyStatus.moveSpeed * 0.01f);

    }
    void StartMove()
    {
        switch (moveList[currentMove].x)
        {
            case 0:
                moveVector = new Vector3(1, 0, 0);
                anim.SetTrigger("Horizontal");
                sprite.flipX = true;
                break;
            case 1:
                moveVector = new Vector3(0, 1, 0);
                anim.SetTrigger("Vertical");
                break;
            case 2:
                moveVector = new Vector3(-1, 0, 0);
                anim.SetTrigger("Horizontal");
                sprite.flipX = false;
                break;
            case 3:
                moveVector = new Vector3(0, -1, 0);
                anim.SetTrigger("Vertical");
                break;
        }
        StartCoroutine(Move(moveList[currentMove].y));
    }
    
    IEnumerator Move(float moveTime)
    {
        yield return new WaitForSeconds(moveTime);
        currentMove++;
        if (currentMove >= moveList.Length) { currentMove = 0; }
        StartMove();
    }
   
}
