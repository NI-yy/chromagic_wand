using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Header("エフェクトがついた床を判定するか")] public bool checkPlatformGround = true;

    private string groundTag = "Ground";
    private string surinukeTag = "Surinuke";
    private string platformTag = "PlatformGround";
    private List<string> tagList = new List<string>();

    public bool isOnGround = false;
    private bool isGroundEnter, isGroundStay, isGroundExit;

    // Start is called before the first frame update
    void Start()
    {
        tagList.Add(groundTag);
        tagList.Add(surinukeTag);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsOnGround()
    {
        if (isGroundExit)
        {
            isOnGround = false;
        }
        else if (isGroundEnter || isGroundStay)
        {
            isOnGround = true;
        }
        

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;

        return isOnGround;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (tagList.Contains(collision.tag))
        {
            isGroundEnter = true;
        }
        else if (checkPlatformGround && collision.tag == platformTag)
        {
            isGroundEnter = true;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (tagList.Contains(collision.tag))
        {
            isGroundStay = true;
        }
        else if (checkPlatformGround && collision.tag == platformTag)
        {
            isGroundStay = true;
        }
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        if (tagList.Contains(collision.tag))
        {
            isGroundExit = true;
        }
        else if (checkPlatformGround && collision.tag == platformTag)
        {
            isGroundExit = true;
        }
    }
}
