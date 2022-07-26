using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fulcrum.ExtensionMethods;

public class EventState_2 : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private int nextStateNumber;
    [SerializeField] GameObject[] deactivateTogglableGameObjects;
    [SerializeField] GameObject[] activateTogglableGameObjects;
    public bool letterInCorrectPos { set; get; }

    [SerializeField] private TearTimeMeasurement letterTearTimeMeasurement;

    private void Start()
    {
        if(letterTearTimeMeasurement == null)
        {
            Debug.Log("You forgot to connect it you dingus!");
        }
    }

    public void ToggleShades()
    {
        deactivateTogglableGameObjects.ToggleGameObjectArray(false);
        activateTogglableGameObjects.ToggleGameObjectArray(true);
    }

    public void NextStateEvent()
    {
        eventManager.CallEvent(nextStateNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if (letterInCorrectPos && letterTearTimeMeasurement.tearCorrect)
        {
            NextStateEvent();
        }
    }
}
