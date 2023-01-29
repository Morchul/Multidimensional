using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip shootSound;

    [SerializeField]
    private AudioClip destroyedSound;

    [SerializeField]
    private AudioSource oneShotAudioSource;

    [SerializeField]
    private AudioSource scanAudioSource;

    [SerializeField]
    private AudioSource moveAudioSource;

    public void PlayShootSound()
    {
        oneShotAudioSource.PlayOneShot(shootSound);
    }

    public void PlayDestroyedSound()
    {
        oneShotAudioSource.PlayOneShot(destroyedSound);
    }

    public void StartPlayScanSound()
    {
        scanAudioSource.Play();
    }

    public void StopPlayScanSound()
    {
        scanAudioSource.Stop();
    }

    public void StartPlayMoveSound()
    {
        if(!moveAudioSource.isPlaying)
            moveAudioSource.Play();
    }

    public void StopPlayMoveSound()
    {
        if(moveAudioSource.isPlaying)
            moveAudioSource.Stop();
    }

    public void StopAllSounds()
    {
        StopPlayMoveSound();
        StopPlayScanSound();
    }
}
