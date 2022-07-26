using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerColliderEvent : MonoBehaviour
{
    [SerializeField] private bool delay;

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
                Invoke("TriggerEnterDelay", 5f);
            }
        }
    }

    private void TriggerEnterDelay()
    {
        inTrigger.Invoke();
    }
}
