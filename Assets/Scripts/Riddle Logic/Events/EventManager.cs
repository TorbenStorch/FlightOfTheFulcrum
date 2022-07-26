using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    //4 State Events
    public UnityEvent onState_1;
    public UnityEvent onState_2;
    public UnityEvent onState_3;
    public UnityEvent onState_4;

    void Start()
    {
        CallEvent(1);
    }

    public void CallEvent(int i)
    {
        switch (i)
        {
            case 1:
                onState_1.Invoke();
                break;

            case 2:
                onState_2.Invoke();
                break;

            case 3:
                onState_3.Invoke();
                break;

            case 4:
                onState_4.Invoke();
                break;

            default:
                break;
        }
    }
}
