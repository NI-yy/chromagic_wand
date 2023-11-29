using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using KoitanLib;

public class TitleManager : MonoBehaviour
{
    public GameObject startButton_;
    public GameObject endButton_;

    // Start is called before the first frame update
    void Start()
    {
        SelectedStartMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectedStartMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton_);
    }

    public void SelectedEndMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(endButton_);
    }

    public void OnClickStartButton()
    {
        //ロードするシーンを変更(yy)
        //SceneManager.LoadScene("SampleScene");
        SceneManager.LoadScene("GameScene_yy");
    }

    public void OnClickEndButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
