using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAudio : MonoBehaviour
{
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StartCoroutine(DelayWestAudio());
    }

    IEnumerator DelayWestAudio()
    {
        yield return new WaitForSeconds(1.5f);

        _audioSource.Play();
    }
}
