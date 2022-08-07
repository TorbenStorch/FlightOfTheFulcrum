using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fulcrum.ExtensionMethods;

//public class EventState_3 : MonoBehaviour
//{
//    [SerializeField] private EventManager eventManager;
//    [SerializeField] private int nextStateNumber;
//    [SerializeField] GameObject[] deactivateTogglableGameObjects;
//    [SerializeField] GameObject[] activateTogglableGameObjects;
//    public bool medicineInCorrectPos { set; get; }

//    public void ToggleShades()
//    {
//        deactivateTogglableGameObjects.ToggleGameObjectArray(false);
//        activateTogglableGameObjects.ToggleGameObjectArray(true);
//    }

//    public void NextStateEvent()
//    {
//        eventManager.CallEvent(nextStateNumber);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (medicineInCorrectPos)
//        {
//            NextStateEvent();
//        }
//    }
//}
public class EventState_3 : EventState
{
    public bool medicineInCorrectPos { set; get; } //can be changed outside of this sctipt (for ex. in unity events)
    void Update()
    {
        if (medicineInCorrectPos)
        {
            NextStateEvent();
        }
    }
}