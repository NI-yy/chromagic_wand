using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitColliderController : MonoBehaviour
{
    string playerTag = "Player";
    [SerializeField] int  colliderNum;
    public CinemachineVirtualCamera virtualCamera_0;
    public CinemachineVirtualCamera virtualCamera_1;
    public CinemachineVirtualCamera virtualCamera_2;
    public CinemachineVirtualCamera virtualCamera_3;

    private bool flag_1 = false; //1‰ñ’Ê‰ß‚µ‚½‚©‚Ç‚¤‚©
    private bool flag_2 = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag(playerTag))
        //{
        //    SceneManager.LoadScene("BlackAndWhite_1_1");
        //}

        if (collision.gameObject.CompareTag(playerTag))
        {
            if (colliderNum == 1)
            {
                if (flag_1)
                {
                    virtualCamera_0.Priority = 1;
                    virtualCamera_1.Priority = 0;
                    flag_1 = false;
                }
                else
                {
                    virtualCamera_0.Priority = 0;
                    virtualCamera_1.Priority = 1;
                    flag_1 = true;
                }
            }
            else if (colliderNum == 2)
            {
                if (flag_2)
                {
                    virtualCamera_1.Priority = 1;
                    virtualCamera_2.Priority = 0;
                    flag_2 = false;
                }
                else
                {
                    virtualCamera_1.Priority = 0;
                    virtualCamera_2.Priority = 1;
                    flag_2 = true;
                }
            }
        }

        
    }
}
