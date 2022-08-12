using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageVoice : MonoBehaviour
{
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void CallDyingAudioCoroutine()
    {
        StartCoroutine(DelayWestDyingAudio());
    }

    IEnumerator DelayWestDyingAudio()
    {
        yield return new WaitForSeconds(15.0f);

        _audioSource.Play();
    }

}
