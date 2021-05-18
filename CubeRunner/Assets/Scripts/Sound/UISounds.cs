using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{

    [SerializeField] private AudioClip audioButtonClick;
    [SerializeField] private AudioClip audioBuyCube;
    [SerializeField] private AudioClip audioCoinPickUp;
    [SerializeField] private AudioClip audioSwtichCube;
    [SerializeField] private AudioClip audioJump;
    [SerializeField] private AudioClip audioFall;
    [SerializeField] private AudioClip audioDestroy;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateVolume();
    }

    public void UpdateVolume()
    {
        audioSource.volume = PlayerPrefs.GetFloat("EffectVolume", 1);
    }

    public void OnButtonUIClick()
    {
        audioSource.clip = audioButtonClick;
        audioSource.Play();
    }

    public void OnBuyCube()
    {
        audioSource.clip = audioBuyCube;
        audioSource.Play();
    }

    public void OnCoinPickUp()
    {
        audioSource.clip = audioCoinPickUp;
        audioSource.Play();
    }

    public void OnSwtichCube()
    {
        audioSource.clip = audioSwtichCube;
        audioSource.Play();
    }

    public void OnJump()
    {
        audioSource.clip = audioJump;
        audioSource.Play();
    }

    public void OnFall()
    {
        audioSource.clip = audioFall;
        audioSource.Play();
    }

    public void OnDestroyObject()
    {
        audioSource.clip = audioDestroy;
        audioSource.Play();
    }
}
