using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip jump_se;
    [SerializeField] AudioClip palette_se;
    [SerializeField] AudioClip fire_se;
    [SerializeField] AudioClip rock_se;
    [SerializeField] AudioClip thunder_se;
    [SerializeField] AudioClip water_se;
    [SerializeField] AudioClip wind_se;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJumpSe()
    {
        audioSource.PlayOneShot(jump_se);
    }

    public void PlayRockSe()
    {
        audioSource.PlayOneShot(rock_se);
    }
}
