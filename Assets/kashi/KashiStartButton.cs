using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class KashiStartButton : KashiBaseButton
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnPush()
    {
        //SceneManager.LoadScene("GameScene_3_yy");
        Debug.Log("Start");
    }
}
