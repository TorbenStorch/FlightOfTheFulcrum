/*-------------------------------------------------------
Creator: Torben Storch
Project: Fulcrum
Last change: 07-08-2022
Topic: Condition for Letter & what happens once fullfilled (UnityEvent)
---------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventState_ConditionLetter : EventState
{
	//public UnityEvent letterCorrect;

	public bool letterInCorrectPos { set; get; } //can be changed outside of this sctipt (for ex. in unity events)
	[SerializeField] private TearTimeMeasurement letterTearTimeMeasurement;

	void Update()
	{
		if (letterTearTimeMeasurement == null)
			Debug.LogError("Letter Tear Missing");

		if (letterInCorrectPos && letterTearTimeMeasurement.tearCorrect)
			NextStateEvent();
	}
}
