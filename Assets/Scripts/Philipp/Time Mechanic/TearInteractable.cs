/*-------------------------------------------------------
Creator: Philipp Petry
Project: Fulcrum
Last change: 07-05-2022
Topic: Sets a grabbed condition in an interactable script
Last Change: Direct Code the selectEnter/Exit into the script, to not have to setup the unity event. 
---------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TearInteractable : XRGrabInteractable
{
    public bool isGrabbed = false;

    protected override void Awake()
    {
        base.Awake();
    }

    private void grabToggle()
    {
        isGrabbed = !isGrabbed;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        grabToggle();
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        grabToggle();
        base.OnSelectExited(args);
    }

}
