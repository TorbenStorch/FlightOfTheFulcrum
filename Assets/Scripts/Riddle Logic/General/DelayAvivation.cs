using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAvivation : MonoBehaviour
{
	[SerializeField] AudioSource audioSource;
	[SerializeField] float delayFloat;
	
	private void OnEnable()
	{
		StartCoroutine(StartDelayEvent(delayFloat));
	}
	IEnumerator StartDelayEvent(float delay)
	{
		yield return new WaitForSeconds(delay);
		audioSource.Play();
	}

}

