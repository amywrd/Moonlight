using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    AudioSource[] sources;


	void Start () {
        sources = GetComponentsInChildren<AudioSource>();
	}

    public void PlaySquish()
    {
        sources[3].Play();
        //sources[4].Play();
    }

    public void PlayGunshot()
    {
        sources[0].Play();
    }

    public void PlayExclamation()
    {
        sources[4].Play();
    }

    public void PlayPlayerDead()
    {
        sources[5].Play();
    }

    public void PlayMoltovThrow()
    {
        sources[1].Play();
    }

    public void PlayPlayerHit()
    {
        sources[2].Play();
    }

    public void StopBgm()
    {
        sources[6].Stop();
    }

    public void PlayTheme()
    {
        sources[7].Play();
    }



    // Update is called once per frame
    void Update () {
	
	}
}
