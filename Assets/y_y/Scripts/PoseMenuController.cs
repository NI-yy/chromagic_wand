using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoitanLib;
using Unity.VisualScripting;

public class PoseMenuController : MonoBehaviour
{
    [SerializeField] GameObject PoseMenu;
    private bool flag = true;

    private void Update()
    {
        if (KoitanInput.GetDown(ButtonCode.Start))
        {
            if (flag)
            {
                PoseMenu.SetActive(true);
                Time.timeScale = 0;
                flag = false;
            }
            else
            {
                PoseMenu.SetActive(false);
                Time.timeScale = 1;
                flag = true;
            }
            

        }
    }
}
