/*-------------------------------------------------------
Creator: Torben Storch
Project: Fulcrum
Last change: 07-08-2022
Topic: Calling of NextState, Activate/Deactivate objects & State Information.
---------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fulcrum.ExtensionMethods;

public class EventState : MonoBehaviour
{
	[SerializeField] private EventManager _eventManager;
	[SerializeField] private int _nextStateNumber;
	[SerializeField] private GameObject[] _deactivateTogglableGameObjects;
	[SerializeField] private GameObject[] _activateTogglableGameObjects;

	public virtual void ToggleShades()
	{
		if (_deactivateTogglableGameObjects.Length != 0)
			_deactivateTogglableGameObjects.ToggleGameObjectArray(false);

		if (_activateTogglableGameObjects.Length != 0)
			_activateTogglableGameObjects.ToggleGameObjectArray(true);
	}

	public void NextStateEvent()
	{
		_eventManager.CallEvent(_nextStateNumber);
	}
}
