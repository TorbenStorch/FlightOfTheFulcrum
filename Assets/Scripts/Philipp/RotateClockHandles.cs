using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateClockHandles : MonoBehaviour
{
    [SerializeField] GameObject hourHandle;
    [SerializeField] GameObject minuteHandle;

    [SerializeField] TimeScrolling scrollingScript;

    [SerializeField] Vector3 rot;
    [SerializeField] float rotationSpeed;

    void Update()
    {
        if (scrollingScript._rayCastScript._rayCastHit == true)
            rotationSpeed = scrollingScript._controllerRotZ;
        
        else
            rotationSpeed = 0f;
        

        hourHandle.transform.Rotate(rot * rotationSpeed * 2 * Time.deltaTime);
        minuteHandle.transform.Rotate(rot * rotationSpeed / 10 * Time.deltaTime);
    }
}
