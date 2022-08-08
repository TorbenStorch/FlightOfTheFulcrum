/*-------------------------------------------------------
Creator: Torben Storch
Project: Fulcrum
Last change: 07-08-2022
Topic: Invoke an UnityEvent after a set delay.  
---------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayEvent : MonoBehaviour
{
	public UnityEvent eventAfterDelay;
	public void StartEventAfterDelay(float delayInSec) => StartCoroutine(StartDelayEvent(delayInSec)); //call this function & pass over the delay amount

	IEnumerator StartDelayEvent(float delay)
	{
		yield return new WaitForSeconds(delay);
		eventAfterDelay.Invoke();
	}		
}
