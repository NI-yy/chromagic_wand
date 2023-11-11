using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonOn : MonoBehaviour
{
    
    [SerializeField]
    Button focusButton_;
    // Start is called before the first frame update
    void Start()
    {
        focusButton_ = focusButton_.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        EventSystem.current.SetSelectedGameObject(null);
        focusButton_.Select();
    }
}
