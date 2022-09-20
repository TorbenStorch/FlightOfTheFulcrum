using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerHeightResetter : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [Header("Set Y value to 0.8 for correct player height (Higher values mean smaller player height)")]
    [SerializeField] Vector3 resetPosition;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ResetPlayer", 0.5f);
    }

    private void ResetPlayer()
    {
        //characterController.center = new Vector3(0.0f, 1.0f, 0.0f);
        characterController.center = resetPosition;
    }
}
