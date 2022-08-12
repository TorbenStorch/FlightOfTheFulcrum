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

    //public void PlayNextVoice()
    //{
    //    if (index < VoiceLines.Length && !auSource.isPlaying)
    //    {
    //        auSource.clip = VoiceLines[index];
    //        auSource.Play();
    //        index += 1;
    //        if (index == VoiceLines.Length - 2) StartCoroutine(lastAudio());
    //    }
    //    else
    //    {
    //        Debug.Log("All Voice lines have been played!");
    //    }
    //}

   IEnumerator lastAudio(AudioSource audioS)
	{
        bool b = true;
        while (b)
		{
            if (!auSource.isPlaying /*&& index == VoiceLines.Length - 1*/ && !audioS.isPlaying)
            {
                auSource.Stop();

                GameObject.FindGameObjectWithTag("SoulSuck").transform.gameObject.SetActive(false);

                auSource.clip = VoiceLines[VoiceLines.Length - 1];
                auSource.Play();
                b = false;
            }
            yield return null;
        }
	}

    public void PlayVoiceAudio(AudioSource audioS)
	{
        //auSource.Stop();
        //auSource.clip = VoiceLines[i];
        //auSource.Play();

        /*if(i == VoiceLines.Length - 2) */StartCoroutine(lastAudio(audioS));
    }


}
