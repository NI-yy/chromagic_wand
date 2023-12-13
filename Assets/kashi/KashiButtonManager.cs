using KoitanLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KashiButtonManager : MonoBehaviour
{
    [SerializeField]
    KashiBaseButton selectedButton_;
    [SerializeField]
    List<KashiBaseButton> Buttons_;
    // Start is called before the first frame update
    void Start()
    {
        selectedButton_.isSelected_ = true;
        selectedButton_.transform.DOScale(1.2f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (KoitanLib.KoitanInput.GetDown(ButtonCode.A))
        {
            selectedButton_.OnPush();
        }
        
        else if (KoitanLib.KoitanInput.GetUp(ButtonCode.Up) && selectedButton_.upperButton_ != null)
        {
            selectedButton_.isSelected_ = false;
            selectedButton_.transform.DOScale(1f, 0.1f);
            selectedButton_ = selectedButton_.upperButton_;
            selectedButton_.isSelected_ = true;
            selectedButton_.transform.DOScale(1.2f, 0.1f);
            Debug.Log("Up");
        }
        else if (KoitanLib.KoitanInput.GetUp(ButtonCode.Down) && selectedButton_.lowerButton_ != null)
        {
            selectedButton_.isSelected_ = false;
            selectedButton_.transform.DOScale(1f, 0.1f);
            selectedButton_ = selectedButton_.lowerButton_;
            selectedButton_.transform.DOScale(1.2f, 0.1f);
            selectedButton_.isSelected_ = true;
            Debug.Log("Down");
        }
    }
}
