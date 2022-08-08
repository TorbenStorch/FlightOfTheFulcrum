/*-------------------------------------------------------
Creator: Torben Storch
Project: Fulcrum
Last change: 07-06-2022
Topic: Condition for Medicine & what happens once fullfilled (UnityEvent)
---------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventState_ConditionMedicine : EventState
{
 //   public UnityEvent medicineCorrect;
    public bool medicineInCorrectPos { set; get; } //can be changed outside of this sctipt (for ex. in unity events)

    void Update()
    {
        if (medicineInCorrectPos)
        {
            NextStateEvent();
        }
    }
}
