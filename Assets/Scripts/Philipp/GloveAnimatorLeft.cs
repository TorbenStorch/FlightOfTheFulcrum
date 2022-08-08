using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class GloveAnimatorLeft : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] InputActionReference left_gripInput;

    private const string anim_param_GRIP = "Grip";

    void Update()
    {
        if (left_gripInput.action.ReadValue<float>() > 0)
        {
            var gripInputVal = left_gripInput.action.ReadValue<float>();
            anim.SetFloat(anim_param_GRIP, gripInputVal);
        }
    }
}
