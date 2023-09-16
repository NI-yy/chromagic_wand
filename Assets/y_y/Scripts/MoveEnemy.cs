using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    private Animator animator;
    //　目的地
    private Vector3 destination;
    //　歩くスピード
    [SerializeField]
    private float walkSpeed = 1.0f;
    //　速度
    private Vector3 velocity;
    //　移動方向
    private Vector3 direction;
    //　到着フラグ
    private bool arrived;
    //　スタート位置
    private Vector3 startPosition;

    private float time;
    private float timeTh = 3.0f;
    Rigidbody rb;


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        var randDestination = Random.Range(1, 9);
        startPosition = transform.position;
        destination = startPosition + new Vector3(randDestination, 0f, 0f);
        arrived = false;
        time = 0f;
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!arrived)
        {
            animator.SetBool("walk", true);
            animator.SetBool("onGround", false);
            direction = (destination - transform.position).normalized;
            transform.localScale = new Vector3(-direction.x * 3, 3f, 3f);
            //transform.Translate(direction * walkSpeed * Time.deltaTime);
            rb.AddForce(direction * walkSpeed);


            if (Mathf.Abs(transform.position.x - destination.x) < 0.05f)
            {
                arrived = true;
            }
        }
        else
        {
            animator.SetBool("walk", false);
            animator.SetBool("onGround", true);

            time += Time.deltaTime;
            if (time > timeTh)
            {
                var randDestination = Random.insideUnitCircle * 8;
                destination = startPosition + new Vector3(randDestination.x, 0f, 0f);
                arrived = false;
                startPosition = transform.position;
                time = 0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if(collision.gameObject.tag == "bullet")
        {
            Destroy(this.gameObject);
        }
    }
}
