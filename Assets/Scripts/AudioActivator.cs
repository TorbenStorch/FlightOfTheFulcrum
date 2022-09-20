using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioActivator : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] GameObject triggerObject;
    [SerializeField] float delayTimeInSec;

    public UnityEvent triggerDelay;
    public bool audioListening { set; get; }
    private bool hasPlayed = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == triggerObject.name && audioListening && !hasPlayed)
        {
            audio.Play();
            hasPlayed = true;
            Invoke("TriggerEnterDelay", delayTimeInSec);
            Debug.Log("Introduction Audio Played");
        }
    }

    private void TriggerEnterDelay()
    {
        triggerDelay.Invoke();
    }
}
