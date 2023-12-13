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
    [SerializeField] AudioClip walk_se;
    [SerializeField] AudioClip item_get_se;

    private AudioSource audioSource;
    private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
    }

    public void PlayJumpSe()
    {
        audioSource.PlayOneShot(jump_se);
    }

    public void PlaypaletteSe()
    {
        audioSource.PlayOneShot(palette_se);
    }

    public void PlayRockSe()
    {
        audioSource.PlayOneShot(rock_se);
    }

    public void PlayFireSe()
    {
        audioSource.PlayOneShot(fire_se);
    }

    public void PlayThunderSe()
    {
        audioSource.PlayOneShot(thunder_se);
    }

    public void PlayWaterSe()
    {
        audioSource.PlayOneShot(water_se);
    }

    public void PlayWindSe()
    {
        audioSource.PlayOneShot(wind_se);
    }

    public void StartPlayingWalkSE()
    {
        Debug.Log(("PlaySE", this.gameObject));
        if (!isPlaying)
        {
            Debug.Log(("StartCoroutine", this.gameObject));
            isPlaying = true;
            StartCoroutine(PlaySoundEffect());
        }
    }

    // å¯â âπÇÃçƒê∂Çí‚é~Ç∑ÇÈ
    public void StopPlayingWalkSE()
    {
        Debug.Log("StopSE");
        isPlaying = false;
    }

    IEnumerator PlaySoundEffect()
    {
        while (isPlaying) // ñ≥å¿ÉãÅ[Év
        {
            audioSource.PlayOneShot(walk_se);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void PlayItemGetSe()
    {
        audioSource.PlayOneShot(item_get_se);
    }
}
