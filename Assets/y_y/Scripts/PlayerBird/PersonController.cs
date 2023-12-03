using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;

public class PersonController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 移動速度
    public float jumpForce = 5.0f; // ジャンプの力
    private Rigidbody2D rb;
    float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = KoitanInput.GetStick(StickCode.LeftStick).x;
        float verticalInput = Input.GetAxis("Vertical");

        // オブジェクトを移動させる
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.Space)) // スペースキーが押されたら
        {
            Jump();
        }

    }

    void Jump()
    {
        // Rigidbody2Dに上向きの力を加えてジャンプする
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
