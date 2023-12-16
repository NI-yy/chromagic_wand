using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class KashiBaseButton : MonoBehaviour
{
    public KashiBaseButton upperButton_;
    public KashiBaseButton lowerButton_;
    [HideInInspector]
    public bool isSelected_;

    void Awake()
    {
        isSelected_ = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    abstract public void OnPush();
}
