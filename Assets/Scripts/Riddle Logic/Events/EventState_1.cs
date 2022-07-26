using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fulcrum.ExtensionMethods;

public class EventState_1 : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private int nextStateNumber;
    [SerializeField] GameObject[] deactivateTogglableGameObjects;

    public void ToggleShades()
    {
        deactivateTogglableGameObjects.ToggleGameObjectArray(false);
    }

    public void NextStateEvent()
    {
        eventManager.CallEvent(nextStateNumber);
    }
}
