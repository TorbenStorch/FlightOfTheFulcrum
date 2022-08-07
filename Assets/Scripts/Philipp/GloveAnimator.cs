using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class GloveAnimator : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] InputActionReference right_gripInput;

    private const string anim_param_GRIP = "Grip";

    void Update()
    {
        if (right_gripInput.action.ReadValue<float>() > 0)
        {
            var gripInputVal = right_gripInput.action.ReadValue<float>();
            anim.SetFloat(anim_param_GRIP, gripInputVal);
        }
    }
}
