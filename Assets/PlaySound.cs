using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource audio;

    private void OnCollisionEnter(Collision collision)
    {
        audio.Play();
    }
}
