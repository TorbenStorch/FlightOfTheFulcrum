/*-------------------------------------------------------
Creator: Torben Storch
Project: Fulcrum
Last change: 07-06-2022
Topic: Trigger Area that starts an UnityEvent.
---------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerColliderEvent : MonoBehaviour
{
    [SerializeField] private bool delay;
    [SerializeField] private float delayTimeInSec;

    [SerializeField] private GameObject target;
    public UnityEvent inTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            if (!delay)
            {
                inTrigger.Invoke();
            }
            else
            {
                Invoke("TriggerEnterDelay", delayTimeInSec);
            }
        }
    }

    private void TriggerEnterDelay()
    {
        inTrigger.Invoke();
    }
}
