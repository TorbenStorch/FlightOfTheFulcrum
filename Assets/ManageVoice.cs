using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageVoice : MonoBehaviour
{
    [SerializeField] AudioClip[] VoiceLines;
    private int index = 0;
    private AudioSource auSource;

    void Start()
    {
        auSource = GetComponent<AudioSource>();
    }

    public void PlayNextVoice()
    {
        if (index <= VoiceLines.Length)
        {
            auSource.clip = VoiceLines[index];
            auSource.Play();
            index += 1;
        }
        else
        {
            Debug.Log("All Voice lines have been played!");
        }
    }
}
