using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageVoice : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] GameObject door;
	[SerializeField] Quaternion doorNew;
	[SerializeField] float steps;

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

        yield return new WaitForSeconds(9f);
        StartCoroutine(DoorMove());
       
    }

    IEnumerator DoorMove()
	{
        float f = 0f;
        while (f < 1)
		{
            f += steps;
            door.transform.rotation = Quaternion.Lerp(door.transform.rotation, doorNew, f);
            yield return null;
        }
        door.transform.rotation = doorNew;
        yield return null;
    }

}
