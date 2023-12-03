using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField, TextArea(3, 10)]
    string info;
    [SerializeField]
    float displayRange;
    [SerializeField]
    float deleteRange;
    [SerializeField]
    Image guide;
    [SerializeField]
    GameObject textPanel;
    [SerializeField]
    Text text;

    Player player;
    Transform PlayerTF;
    Vector3 origin;
    Vector3 direction;
    float dist;

    bool displaying;
    bool guiding;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        PlayerTF = player.GetComponent<Transform>();
        text.text = info;
    }

    // Update is called once per frame
    void Update()
    {
        origin = transform.position;//originÇ…é©êgÇÃç¿ïWÇë„ì¸
        Vector3 playerPos = PlayerTF.position;
        playerPos.y += 7.5f;
        direction = (playerPos - origin).normalized;
        Debug.DrawRay(origin, direction * deleteRange, Color.gray);
        Debug.DrawRay(origin, direction * displayRange, Color.green);

        dist = (playerPos - origin).magnitude;


        if (dist <= displayRange)
        {
            if (!displaying)
            {
                displaying = true; if (!textPanel.activeSelf) { SetGuide(true); }
            }
            if (Input.GetKeyDown(KeyCode.Alpha1) || KoitanInput.Get(ButtonCode.Y))
            {
                if (!textPanel.activeSelf)
                {
                    SetGuide(false);
                    textPanel.SetActive(true);
                }
            }

        }
        else if (displaying)
        {
            displaying = false;
            SetGuide(false);
        }

        if (textPanel.activeSelf && dist >= deleteRange)
        {
            textPanel.SetActive(false);
        }
    }
    void SetGuide(bool set)
    {
        if (set && !guiding)
        {
            guiding = true;
            guide.GetComponent<Animator>().SetTrigger("enable");
        }
        if(!set&&guiding)
        {
            guiding = false;
            guide.GetComponent<Animator>().SetTrigger("unable");
        }
    }
}
